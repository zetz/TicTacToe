﻿using System;
using System.Threading.Tasks;
using Akka.Interfaced;
using ProtoBuf;
using TypeAlias;

namespace Domain.Interfaced
{
    public interface IUserDirectory : IInterfacedActor
    {
        Task RegisterUser(string userId, IUser user);
        Task UnregisterUser(string userId);
        Task<IUser> GetUser(string userId);
    }
}