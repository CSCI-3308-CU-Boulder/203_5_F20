using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

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
    IList<players> arlist1 = new List<players>();
    public void createPlayerUI(string usernameID)
    {
        Debug.Log("this is " + usernameID);
        GameObject playerNew = new GameObject("Player" + amountOfPlayers);
        playerNew.transform.SetParent(this.transform);
        Text myText = playerNew.AddComponent<Text>();
        myText.text = usernameID;
        playerNew.transform.position = new Vector3(800, 560 + (40 * -amountOfPlayers), 0);
        playerNew.GetComponent<Text>().color = Color.white;
        playerNew.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerNew.GetComponent<Text>().fontSize = 38;
        RectTransform rt = (RectTransform)playerNew.transform;
        rt.sizeDelta = new Vector2(600f, 50f);
        var rectTransform = playerNew.GetComponent<RectTransform>();

        players p;
        p.user = usernameID;
        p.playerNum = amountOfPlayers;
        p.visable = true;
        p.playerScore = 0;
        p.gamObj = playerNew;
        //Debug.Log("this is " + p.playerScore);
        arlist1.Add(p);
        amountOfPlayers++;
    }
    public void updatePlayerUI()
    {
        for (int i = 0; i < arlist1.Count; i++)
        {
            arlist1[i].gamObj.transform.SetParent(this.transform);
            arlist1[i].gamObj.transform.position = new Vector3(800, 560 + (40 * -i), 0);
            var rectTransform = arlist1[i].gamObj.GetComponent<RectTransform>();
        }
    }

    public void playerDis(string usernameID)
    {
        for (int i = 0; i < arlist1.Count; i++)
        {
            if (arlist1[i].user == usernameID)
            {
                arlist1[i].gamObj.active = false;
                arlist1.RemoveAt(i);
                i--;
                amountOfPlayers--;
            }
            //TODO: need to shift the rest of the game objects when one disconnects
            //playerNew.transform.position = new Vector3(0, 28 * (i - k), 0);
        }
    }
}
