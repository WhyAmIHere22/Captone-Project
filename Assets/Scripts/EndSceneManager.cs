using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void OnMainMenuClick()
    {
        SceneManager.LoadScene(0);
    }
}
