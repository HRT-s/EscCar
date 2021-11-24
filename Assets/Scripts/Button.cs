using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }
}
