using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{
    public string sceneName;
    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
