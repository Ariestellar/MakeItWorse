using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("CurrenScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrenScene"));
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
