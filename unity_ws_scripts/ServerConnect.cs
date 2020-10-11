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

    public void SayHello(){
      client.SendMessage("Hello World!");
    }

    public void HearHello(){
      var q = client.inQueue;
      string message;
      //read from queue here
      while(q.TryPeek(out message)){
        q.TryDequeue(out message);
        Debug.Log("Received: " + message);
      }
    }
}
