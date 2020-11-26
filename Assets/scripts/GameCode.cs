using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using System;

public class GameCode : MonoBehaviour
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
        holder = GameObject.Find("ServerCode").GetComponent<ServerConnect>().holder;
        setID.text = holder;
        serverIDtxt.GetComponent<Text>().text = holder;
    }
}
