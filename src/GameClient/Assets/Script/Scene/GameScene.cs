﻿using UnityEngine;
using System.Collections;
using System.Net;
using Akka.Interfaced;
using Domain.Interfaced;

public class GameScene : MonoBehaviour
{
    private UserRef _user;
    private string _userId;

    void Start()
    {
        ApplicationComponent.TryInit();

        G.Comm = new Communicator(G.Logger, this);
        G.Comm.ServerEndPoint = new IPEndPoint(IPAddress.Loopback, 9001);
        G.Comm.Start();

        StartCoroutine(ProcessLoginUser());
    }

    private IEnumerator ProcessLoginUser()
    {
        Debug.LogFormat("ProcessLoginUser");

        var userLogin = new UserLoginRef(new SlimActorRef { Id = 1 }, new SlimRequestWaiter { Communicator = G.Comm }, null);

        // Login

        var t1 = userLogin.Login("tester", "1234", G.Comm.IssueObserverId());
        yield return t1.WaitHandle;
        ShowResult(t1, "Login");
        if (t1.Exception != null)
            yield break;
        _user = new UserRef(new SlimActorRef { Id = t1.Result }, new SlimRequestWaiter { Communicator = G.Comm }, null);

        // Get UserId (just for test)

        var t2 = _user.GetId();
        yield return t2.WaitHandle;
        ShowResult(t2, "GetId");
        _userId = t2.Result;
    }

    void ShowResult(Task task, string name)
    {
        if (task.Status == TaskStatus.RanToCompletion)
            Debug.Log(string.Format("{0}: Done", name));
        else if (task.Status == TaskStatus.Faulted)
            Debug.Log(string.Format("{0}: Exception = {1}", name, task.Exception));
        else if (task.Status == TaskStatus.Canceled)
            Debug.Log(string.Format("{0}: Canceled", name));
        else
            Debug.Log(string.Format("{0}: Illegal Status = {1}", name, task.Status));
    }

    void ShowResult<TResult>(Task<TResult> task, string name)
    {
        if (task.Status == TaskStatus.RanToCompletion)
            Debug.Log(string.Format("{0}: Result = {1}", name, task.Result));
        else
            ShowResult((Task)task, name);
    }
}