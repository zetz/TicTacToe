﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Utility;
using Akka.Configuration;
using Akka.Interfaced;
using Common.Logging;
using Domain.Interfaced;
using Akka.Interfaced.SlimSocket.Server;
using Akka.Interfaced.SlimSocket.Base;
using ProtoBuf.Meta;
using TypeAlias;
using System.Net.Sockets;

namespace GameServer
{
    public class ServiceMain
    {
        private List<Tuple<ActorSystem, List<IActorRef>>> _clusters = new List<Tuple<ActorSystem, List<IActorRef>>>();

        public void Run(string[] args)
        {
            // force interface assembly to be loaded before creating ProtobufSerializer

            var type = typeof(IUser);
            if (type == null)
                throw new InvalidProgramException("!");

            // connect to mongo-db

            try
            {
                var cstr = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
                MongoDbStorage.Instance = new MongoDbStorage(cstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in connecting mongo-db: " + e);
                return;
            }

            // run cluster nodes

            var commonConfig = ConfigurationFactory.ParseString(@"
                akka {
                  actor {
                    provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                    serializers {
                      proto = ""Akka.Interfaced.ProtobufSerializer.ProtobufSerializer, Akka.Interfaced.ProtobufSerializer""
                    }
                    serialization-bindings {
                      ""Akka.Interfaced.NotificationMessage, Akka.Interfaced"" = proto
                      ""Akka.Interfaced.RequestMessage, Akka.Interfaced"" = proto
                      ""Akka.Interfaced.ResponseMessage, Akka.Interfaced"" = proto
                    }
                  }
                  remote {
                    helios.tcp {
                      hostname = ""127.0.0.1""
                    }
                  }
                  cluster {
                    seed-nodes = [""akka.tcp://GameCluster@127.0.0.1:3001""]
                    auto-down-unreachable-after = 30s
                  }
                }");

            var standAlone = args.Length > 0 && args[0] == "standalone";
            if (standAlone)
            {
                LaunchClusterNode(commonConfig, 3001, 9001, "game-directory", "game-pair-maker",
                                                            "user-directory", "game", "user");
            }
            else
            {
                LaunchClusterNode(commonConfig, 3001, 0, "game-directory", "game-pair-maker");
                LaunchClusterNode(commonConfig, 3002, 0, "user-directory");
                LaunchClusterNode(commonConfig, 3011, 0, "game");
                LaunchClusterNode(commonConfig, 3012, 0, "game");
                LaunchClusterNode(commonConfig, 3021, 9001, "user");
                LaunchClusterNode(commonConfig, 3022, 9002, "user");
            }

            // wait for stop signal

            Console.WriteLine("Press enter key to exit.");
            Console.ReadLine();

            // TODO: Graceful Shutdown

            _clusters.Reverse();
            foreach (var cluster in _clusters)
            {
                // stop all root-actors in reverse

                var rootActors = cluster.Item2;
                rootActors.Reverse();
                foreach (var actor in rootActors)
                    actor.GracefulStop(TimeSpan.FromSeconds(30), new ShutdownMessage()).Wait();

                // stop system

                cluster.Item1.Shutdown();
            }
        }

        private void LaunchClusterNode(Config commonConfig, int port, int clientPort, params string[] roles)
        {
            var config = commonConfig
                .WithFallback("akka.remote.helios.tcp.port = " + port)
                .WithFallback("akka.cluster.roles = " + "[" + string.Join(",", roles) + "]");
            var system = ActorSystem.Create("GameCluster", config);
            var rootActors = InitClusterNode(system, clientPort, roles);
            _clusters.Add(Tuple.Create(system, rootActors));
        }

        private static List<IActorRef> InitClusterNode(ActorSystem system, int clientPort, params string[] roles)
        {
            DeadRequestProcessingActor.Install(system);

            var cluster = Cluster.Get(system);
            var context = new ClusterNodeContext { System = system };

            context.ClusterActorDiscovery = 
                system.ActorOf(Props.Create(() => new ClusterActorDiscovery(cluster)), "ClusterActorDiscovery");
            context.ClusterNodeContextUpdater =
                system.ActorOf(Props.Create(() => new ClusterNodeContextUpdater(context)), "ClusterNodeContextUpdater");

            var rootActors = new List<IActorRef>();
            foreach (var role in roles)
            {
                IActorRef rootActor = null;
                switch (role)
                {
                    case "game-directory":
                        rootActor = system.ActorOf(Props.Create<GameDirectoryActor>(context), nameof(IGameDirectory));
                        break;

                    case "game-pair-maker":
                        rootActor = system.ActorOf(Props.Create<GamePairMakerActor>(context), nameof(IGamePairMaker));
                        break;

                    case "user-directory":
                        rootActor = system.ActorOf(Props.Create<UserDirectoryActor>(context), nameof(IUserDirectory));
                        break;

                    case "game":
                        rootActor = system.ActorOf(Props.Create<GameDirectoryWorkerActor>(context), nameof(IGameDirectoryWorker));
                        break;

                    case "user":
                        var userSystem = new UserClusterSystem(context);
                        rootActor = userSystem.Start(clientPort);
                        break;

                    default:
                        throw new InvalidOperationException("Invalid role: " + role);
                }
                rootActors.Add(rootActor);
            }
            return rootActors;
        }

        private class UserClusterSystem
        {
            private ClusterNodeContext _clusterContext;
            private TcpConnectionSettings _tcpConnectionSettings;

            public UserClusterSystem(ClusterNodeContext clusterContext)
            {
                _clusterContext = clusterContext;
            }

            public IActorRef Start(int port)
            {
                var logger = LogManager.GetLogger("ClientGateway");

                _tcpConnectionSettings = new TcpConnectionSettings
                {
                    PacketSerializer = new PacketSerializer(
                        new PacketSerializerBase.Data(
                            new ProtoBufMessageSerializer(TypeModel.Create()),
                            new TypeAliasTable()))
                };

                var clientGateway = _clusterContext.System.ActorOf(Props.Create(() => new ClientGateway(logger, CreateSession)));
                clientGateway.Tell(new ClientGatewayMessage.Start(new IPEndPoint(IPAddress.Any, port)));
                return clientGateway;
            }

            private IActorRef CreateSession(IActorContext context, Socket socket)
            {
                var logger = LogManager.GetLogger($"Client({socket.RemoteEndPoint.ToString()})");
                return context.ActorOf(Props.Create(() => new ClientSession(
                    logger, socket, _tcpConnectionSettings, CreateInitialActor)));
            }

            private Tuple<IActorRef, Type>[] CreateInitialActor(IActorContext context, Socket socket)
            {
                return new[]
                {
                    Tuple.Create(context.ActorOf(Props.Create(() => new UserLoginActor(_clusterContext, context.Self, socket.RemoteEndPoint))),
                                 typeof(IUserLogin))
                };
            }
        };
    }
}
