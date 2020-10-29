
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using UnityEngine;

[Serializable] //need this line for ToJson to work
public class Message
{
    [SerializeField] public int type;
    [SerializeField] public string gameCode;
    [SerializeField] public string username;
    [SerializeField] public int errNum;

    public Message(int t, string g, string u, int e)
    {
        type = t;
        gameCode = g;
        username = u;
        errNum = e;
    }

    public string JsonConvert()
    {
        return JsonUtility.ToJson(this);
    }
}