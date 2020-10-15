using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addPlayer : MonoBehaviour
{
    public int amountOfPlayers = 1;
    public void createPlayerUI(string usernameID)
    {
        GameObject playerNew = new GameObject("Player" + amountOfPlayers);
        playerNew.transform.SetParent(this.transform);
        Text myText = playerNew.AddComponent<Text>();
        myText.text = usernameID;
        playerNew.transform.position = new Vector3(100, 100 * amountOfPlayers, 0);
        amountOfPlayers++;
    }
}
