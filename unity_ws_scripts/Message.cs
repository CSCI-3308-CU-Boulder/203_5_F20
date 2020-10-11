using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using UnityEngine;

// [Serializable] //need this line for ToJson to work
// public class Parameters
// {
//   public string gameCode {get; set;}
//   public string userName {get; set;}
//
//   public Parameters(string g, string u){
//     gameCode = g;
//     userName = u;
//   }
// }

[Serializable] //need this line for ToJson to work
public class Message
{
  public int type;
  public string gameCode;
  public string userName;

  public Message(int t, string g, string u){
    type = t;
    gameCode = g;
    userName = u;
  }

  public string JsonConvert(){
    return JsonUtility.ToJson(this);
  }
}
