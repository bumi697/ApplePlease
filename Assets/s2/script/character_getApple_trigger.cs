using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class character_getApple_trigger : MonoBehaviour
{
    public TextMeshProUGUI text_apple;
    public GameObject audio_getApple;
    public string nextSceneName = "end";
    public GameObject winorlose;

    public bool getBigApple = false;

    public int Score=0;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("apple"))
        {
            GameObject collectedApple = other.gameObject;

            Score+= collectedApple.GetComponent<apple_gravity>().score;
            Debug.Log("Åöµ½ÁËÆ»¹û£¡");
            audio_getApple.GetComponent<AudioSource>().Play();
            if (Score>=100)
            {
             text_apple.text = "Score : "+Score;
            }
            else if (Score >=10)
            {
                text_apple.text = "Score : " +"0"+ Score;
            }
            else if (Score >=0)
            {
                text_apple.text = "Score : " + "00" + Score;
            }
            Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag("end"))
        {
            getBigApple = true;

        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }
}
