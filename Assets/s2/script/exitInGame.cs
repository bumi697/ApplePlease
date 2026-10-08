using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitInGame : MonoBehaviour
{
    public string nextSceneName = "scene01";

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);

    }
}
