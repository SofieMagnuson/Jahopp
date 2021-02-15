using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadinScenes : MonoBehaviour
{
    public bool isAtCheckpoint;
    
    void Update()
    {
        if (isAtCheckpoint = true);
        SceneManager.LoadScene("Level 2");
        Time.timeScale = 1f;
    }
}
