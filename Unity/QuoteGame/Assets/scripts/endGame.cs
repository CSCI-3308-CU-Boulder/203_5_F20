using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{
    public GameObject serverConnection;
    void Start()
    {
        serverConnection = GameObject.Find("ServerCode");
        serverConnection.GetComponent<ServerConnect>().scoreBoard();
        serverConnection.GetComponent<ServerConnect>().endTheGame();
        Debug.Log("HEY WE ARE SHOWING SCOREBOARD");
    }
}
