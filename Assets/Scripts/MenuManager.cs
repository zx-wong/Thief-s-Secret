using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void ButtonStart()
    {
         
        SceneManager.LoadScene(1);
    }

    public void ButtonTutorial()
    {
        SceneManager.LoadScene(3);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
