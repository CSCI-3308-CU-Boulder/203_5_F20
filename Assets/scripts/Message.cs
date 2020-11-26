
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
    [SerializeField] public string userName;
    [SerializeField] public int errNum;
    [SerializeField] public int q_num;
    [SerializeField] public string q_text;
    [SerializeField] public int data;
    public Message(int t, string g, string u, int e, int q,string s,int d)
    {
        type = t;
        gameCode = g;
        userName = u;
        errNum = e;
        q_num = q;
        q_text = s;
        data = d;


    }

    public string JsonConvert()
    {
        return JsonUtility.ToJson(this);
    }
}