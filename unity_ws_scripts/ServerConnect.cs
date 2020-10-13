using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using UnityEngine;

public class ServerConnect : MonoBehaviour
{
    private string address = "ws://3.130.99.109:80";
    private UnityClient client;

    public void Awake(){
      client = new UnityClient(address);
    }

    public async void EstablishConnection(){
      await client.Connect();
    }

    public void SendJSON(string json){
      client.SendMessage(json);
    }

    public string Listen(){
      var q = client.inQueue;
      string message;

      //read from queue here
      while(q.TryPeek(out message)){
        q.TryDequeue(out message);
        Debug.Log("Received: " + message);
        return message;
      }
      return "error";
    }

    public void RequestCode(){
      Message m = new Message(2, "123", "conor", 3);
      string json_message = m.JsonConvert();
      Debug.Log(json_message);
      client.SendMessage(json_message);
    }

    public void GetCode(){
      string code = Listen();
      Message m;
      m = JsonUtility.FromJson<Message>(code);
      Debug.Log(m.gameCode);
    }
}
