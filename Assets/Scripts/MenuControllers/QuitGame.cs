using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
