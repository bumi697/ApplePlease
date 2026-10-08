using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back : MonoBehaviour
{
    public string nextSceneName = "scene01";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);

        }
    }
}
