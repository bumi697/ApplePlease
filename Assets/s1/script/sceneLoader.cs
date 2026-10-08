using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public string nextSceneName = "inGame";

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);

    }

}
