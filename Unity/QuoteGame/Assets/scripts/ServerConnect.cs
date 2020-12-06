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
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using UnityEditor;
using System.IO;

public class ServerConnect : MonoBehaviour
{
    private string address = "ws://3.130.99.109:80";
    private UnityClient client;
    public GameObject serverIDtxt;
    public Text setID;
    public addPlayer add;
    public GameObject score;
    public string holder = "Waiting For ID";
    private int correctAnswer = 0;
    private int playerNumber = 0;
    public struct players
    {
        public string user;
        public int playerNum;
        public int playerScore;
    }
    IList<players> playerlist = new List<players>();

    // Start is called before the first frame update
    void Start()
    {
        setID.text = holder;
        serverIDtxt.GetComponent<Text>().text = holder;
        client = new UnityClient(address);
        servConnect();
        RequestCode();
        Debug.Log("should be game code:" + holder);

    }
    public bool hasGameCode = false;
    public async void servConnect()
    {
        await client.Connect();
    }
    //this is called in every frame of the game
    void Update()
    {
        var q = client.inQueue;
        string message;

        //read from queue here
        int i = 0;
        while (q.TryPeek(out message))
        {
            q.TryDequeue(out message);
            //Debug.Log("Received: "+ i + message);

            Message m = new Message(-1, "", "", -1, -1, "",-1);
            m = JsonUtility.FromJson<Message>(message);
            //Debug.Log("Username: " + m.gameCode);
            //logic for creating new game lobby
            if (m.type == 2)
            {
                //Debug.Log("GameCode: " + m.gameCode);
                holder = m.gameCode;
                setID.text = m.gameCode;
                serverIDtxt.GetComponent<Text>().text = m.gameCode;
            }

            //logic for adding a player
            else if (m.type == 1)
            {
                //Debug.Log("message: " + message);
                add.createPlayerUI(m.userName);
                //Debug.Log("Username:" + m.userName);
                players k;
                k.user = m.userName;
                k.playerScore = 0;
                k.playerNum = playerNumber;
                playerlist.Add(k);
                playerNumber++;
            }

            else if (m.type == 3)
            {
                add.playerDis(m.userName);
                add.updatePlayerUI();
                playerNumber--;
                for (int k = 0; k < playerlist.Count; k++)
                {
                    if (playerlist[k].user == m.userName)
                    {
                        playerlist.RemoveAt(k);
                    }
                }
            }
            else if (m.type == 5)
            {
                checkRight(m.data, m.userName);
            }
            //error case
            else
            {
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
        Message m = new Message(2, "123", "conor", 3,-1,"",-1);
        string json_message = m.JsonConvert();
        //Debug.Log(json_message);
        client.SendMessage(json_message);
    }
    public void endQuestion()
    {
        Message m = new Message(6, "", "", -1, -1, "", -1);
        string json_message = m.JsonConvert();
        //Debug.Log(json_message);
        client.SendMessage(json_message);
    }
    public void newQuestion(int qNum, string qText)
    {
        Message m = new Message(5, "", "", -1,qNum,qText,-1 );
        string json_message = m.JsonConvert();
        //Debug.Log(json_message);
        client.SendMessage(json_message);
    }
    public void startGame()
    {
        Message m = new Message(4, "", "", -1, -1, "",-1);
        string json_message = m.JsonConvert();
        //Debug.Log(json_message);
        client.SendMessage(json_message);
    }
    public void endTheGame()
    {
        Message m = new Message(7, "", "", -1, -1, "", -1);
        string json_message = m.JsonConvert();
        //Debug.Log(json_message);
        client.SendMessage(json_message);
    }
    public void correctAnswers(int a)
    {
        correctAnswer = a;
    }
    public void checkRight(int answer, string username)
    {
        if (correctAnswer == (answer + 1))
        {
            Debug.Log(username + " was correct");
            int a = playerlist.Count;
            for (int i = 0; i < a; i++)
            {
                if (playerlist[i].user == username)
                {
                    players k;
                    k.user = playerlist[i].user;
                    k.playerScore = playerlist[i].playerScore + 1;
                    k.playerNum = playerlist[i].playerNum;
                    playerlist.Add(k);
                    playerlist.RemoveAt(i);
                    a = 0;
                }
            }
        }
        else {
            Debug.Log(username + " was incorrect");
        }
    }
    public void printScore()
    {
        for (int i = 0; i < playerlist.Count; i++)
        {
            Debug.Log(playerlist[i].user +" "+ playerlist[i].playerScore);
        }
    }
    public void scoreBoard()
    {
        score = GameObject.Find("GameEndOBJ");
        /*for (int k = 0; k < 8; k++)
        {
            for (int i = 1; i < playerlist.Count; i++)
            {
                if (playerlist[i--].playerScore < playerlist[i].playerScore)
                {
                    players p = playerlist[i--];
                    playerlist[i--] = playerlist[i];
                    playerlist[i] = p;
                }
            }
        }*/
        int[] array1 = new int[3];
        array1[0] = -1;
        array1[1] = -1;
        array1[2] = -1;
        players[] array2 = new players[3];
        array2[0] = playerlist[0];
        array1[0] = playerlist[0].playerScore;
        for (int i = 1; i < playerlist.Count; i++)
        {
            if (playerlist[i].playerScore > array1[0])
            {
                array2[0] = playerlist[i];
                array1[0] = playerlist[i].playerScore;
            }
        }
        for (int i = 1; i < playerlist.Count; i++)
        {
            if (playerlist[i].playerScore > array1[1] && playerlist[i].user != array2[0].user)
            {
                array2[1] = playerlist[i];
                array1[1] = playerlist[i].playerScore;
            }
        }
        for (int i = 1; i < playerlist.Count; i++)
        {
            if (playerlist[i].playerScore > array1[2] && playerlist[i].user != array2[0].user && playerlist[i].user != array2[1].user)
            {
                array2[2] = playerlist[i];
                array1[2] = playerlist[i].playerScore;
            }
        }
        for (int i = 0; i < 3; i++)
        {
             score.GetComponent<Score>().scoreUI((i + 1) + ". " + array2[i].user + " - " + array2[i].playerScore);
        }
    }
    public void createPlayerUI(string usernameID)
    {
        GameObject playerNew = new GameObject(usernameID);
        playerNew.transform.SetParent(this.transform);
        Text myText = playerNew.AddComponent<Text>();
        myText.text = usernameID;
        playerNew.transform.position = new Vector3(870, 560 + (28), 0);
        playerNew.GetComponent<Text>().color = Color.white;
        playerNew.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerNew.GetComponent<Text>().fontSize = 26;
        var rectTransform = playerNew.GetComponent<RectTransform>();
    }
}
