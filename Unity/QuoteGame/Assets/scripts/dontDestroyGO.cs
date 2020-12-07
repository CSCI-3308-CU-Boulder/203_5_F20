using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyGO : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
