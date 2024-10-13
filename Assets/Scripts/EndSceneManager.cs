using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void ReturnToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
