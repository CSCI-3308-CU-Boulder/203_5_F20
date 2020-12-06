using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class Score : MonoBehaviour
{
    bool haveID = false;
    Text m_Text;
    RectTransform m_RectTransform;
    public Text setID;
    int amountOfPlayers = 0;

    public void scoreUI(string usernameID)
    {
        Debug.Log("this is " + usernameID);
        GameObject playerNew = new GameObject("Player" + amountOfPlayers);
        playerNew.transform.SetParent(this.transform);
        Text myText = playerNew.AddComponent<Text>();
        myText.text = usernameID;
        playerNew.transform.position = new Vector3(800, 560 + (40 * -amountOfPlayers), 0);
        playerNew.GetComponent<Text>().color = Color.white;
        playerNew.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerNew.GetComponent<Text>().fontSize = 28;
        RectTransform rt = (RectTransform)playerNew.transform;
        rt.sizeDelta = new Vector2(700f, 80f);
        var rectTransform = playerNew.GetComponent<RectTransform>();
        amountOfPlayers++;


        Debug.Log("HEY WE ARE SHOWING SCOREBOARD 2.0");
    }
}
