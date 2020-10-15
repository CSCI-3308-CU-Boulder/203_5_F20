
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ServerConnect : MonoBehaviour
{
    private string address = "ws://3.130.99.109:80";
    private UnityClient client;
    public GameObject serverIDtxt;
    public Text setID;
    public string holder = "Waiting For ID";
    public int amountOfPlayers = 0;
    bool haveID = false;

    // Start is called before the first frame update
    void Start()
    {
        setID.text = holder;
        serverIDtxt.GetComponent<Text>().text = holder;
        client = new UnityClient(address);
        await client.Connect();
        RequestCode();
        Debug.Log("should be game code:" + holder);

    }
    public bool hasGameCode = false;

    //this is called in every frame of the game
    void Update()
    {
        var q = client.inQueue;
        string message;

        //read from queue here
        while(q.TryPeek(out message)){
          q.TryDequeue(out message);
          Debug.Log("Received: " + message);

          Message m = new Message(-1, "", "", -1);
          Debug.Log(m.type);
          m = JsonUtility.FromJson<Message>(message);

          //logic for creating new game lobby
          if (m.type == 2){
            Debug.Log(m.gameCode);
          }

          //logic for adding a player
          else if (m.type == 1){
            Debug.Log(m.userName);
          }

          //error case
          else{
            Debug.Log("Error in type");
          }

          client.RestartThread(); //this restarts the thread that listens for incoming messages
        }
    }

    //this is used to send data to the server
    public void SendJSON(string json)
    {
        client.SendMessage(json);
    }

    //this is used to start a game and retrieve a unique game code
    public void RequestCode()
    {
        Message m = new Message(2, "123", "conor", 3);
        string json_message = m.JsonConvert();
        Debug.Log(json_message);
        client.SendMessage(json_message);
    }
}
