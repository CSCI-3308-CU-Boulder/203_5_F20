
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
        connect();
        RequestCode();
        Debug.Log("should be game code:" + holder);
        
    }
    public bool hasGameCode = false;
    void Update()
    {
        
        if (hasGameCode == false)
        {
            holder = GetCode();
            setID.text = holder;
            serverIDtxt.GetComponent<Text>().text = holder;
            hasGameCode = true;
        }
        //Listen();
    }
    // Update is called once per frame
    public async void connect()
    {
        await client.Connect();
    }

    public void SendJSON(string json)
    {
        client.SendMessage(json);
    }
    public string Listen()
    {
        var q = client.inQueue;
        string message;

        //read from queue here
        while (q.TryPeek(out message))
        {
            q.TryDequeue(out message);
            Debug.Log("Received: " + message);
            return message;
        }
        return "error";
    }
    public string GetCode()
    {
        string code = Listen();
        Message m;
        m = JsonUtility.FromJson<Message>(code);
        Debug.Log(m.gameCode);
        return m.gameCode;
    }
    public void RequestCode()
    {
        Message m = new Message(2, "123", "conor", 3);
        string json_message = m.JsonConvert();
        Debug.Log(json_message);
        client.SendMessage(json_message);
    }

    public void GetName()
    {
    }

}