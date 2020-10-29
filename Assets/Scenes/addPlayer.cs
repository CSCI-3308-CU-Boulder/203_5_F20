using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class addPlayer : MonoBehaviour
{
    public int amountOfPlayers = 0;
    bool haveID = false;
    Text m_Text;
    RectTransform m_RectTransform;
    public Text setID;

    public struct players{
        public string user;
        public int playerNum;
        public bool visable;
        public int playerScore;
        public GameObject gamObj;
    }
    public players[] Players;
    public void createPlayerUI(string usernameID)
    {
        Debug.Log("this is " + usernameID);
        GameObject playerNew = new GameObject("Player" + amountOfPlayers);
        playerNew.transform.SetParent(this.transform);
        Text myText = playerNew.AddComponent<Text>();
        myText.text = usernameID;

        playerNew.transform.position = new Vector3(870, 560 + (28 * -amountOfPlayers), 0);
        playerNew.GetComponent<Text>().color = Color.white;
        playerNew.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerNew.GetComponent<Text>().fontSize = 26;
        var rectTransform = playerNew.GetComponent<RectTransform>();

        players p;
        p.user = usernameID;
        p.playerNum = amountOfPlayers;
        p.visable = true;
        p.playerScore = 0;
        p.gamObj = playerNew;
        Debug.Log("this is " + p.playerScore);
        Players[amountOfPlayers] = p;
        amountOfPlayers++;
    }
    public void printPlayerUI()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            //TODO: add create obj print on to screen and take it out of createPlayerUI
        }
    }
}
    public void playerDis(string usernameID)
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].user == usernameID)
            {
                Players[i].gamObj.active = false;
                int k = i;
            }
            //TODO: need to shift the rest of the game objects when one disconnects
            //playerNew.transform.position = new Vector3(0, 28 * (i - k), 0);
        }
    }
}
