using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class addGameCode : MonoBehaviour
{
    public GameObject serverIDtxt;
    public Text setID;
    public string holder = "Waiting For ID";
    // Start is called before the first frame update
    void Start()
    {
        setID.text = holder;
        serverIDtxt.GetComponent<Text>().text = holder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
