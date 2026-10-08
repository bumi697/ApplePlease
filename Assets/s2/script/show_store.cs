using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class show_store : MonoBehaviour
{

    public int DAY = 1;

    public GameObject time_now;
    public GameObject store;
    public GameObject UI;
    public GameObject turial;
    public TextMeshProUGUI nextDayText;
    public TextMeshProUGUI DayText;


    public GameObject player;
    private GameObject[] apples;
    public GameObject appleGenerater;
    public GameObject winorlose;

    public bool turialShown = false;
    public bool storeShown = false;
    private int timeOut = 0;

    public string nextSceneName = "end";

    // Start is called before the first frame update
    void Start()
    {   
        turialShown = false;
        storeShown = false;

        turial.SetActive(true);
        UI.SetActive(false);
        store.SetActive(false);

        DAY = 1;
        nextDayText.text = "next Day";
        
        stopBegin();
    }

    // Update is called once per frame


     void stopBegin()
    {
            time_now.GetComponent<text_time>().enabled = false;
            player.GetComponent<character_getApple_trigger>().enabled = false;
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<ThirdPersonCarmera>().enabled = false;

            player.GetComponent<ThirdPersonMove>().ResetMove();
            player.GetComponent<ThirdPersonMove>().enabled = false;

            appleGenerater.GetComponent<generate_apple>().enabled = false;
    }
    void startBegin()
    {
            time_now.GetComponent<text_time>().enabled = true;
            player.GetComponent<character_getApple_trigger>().enabled = true;
            player.GetComponent<PlayerInput>().enabled = true;
            player.GetComponent<ThirdPersonCarmera>().enabled = true;

            player.GetComponent<ThirdPersonMove>().ResetMove();
            player.GetComponent<ThirdPersonMove>().enabled = true;
            appleGenerater.GetComponent<generate_apple>().enabled = true;

            turial.SetActive(false);
            UI.SetActive(true);
            turialShown = true;
    }


    void Update()
    { 
        if(Input.anyKey&&!turialShown)
        {
            startBegin();
        }
        if (!storeShown && time_now.GetComponent<text_time>().nowTime == timeOut)
        {
            Debug.Log(time_now.GetComponent<text_time>().nowTime+" "+ timeOut);

            store.SetActive(true);
            UI.SetActive(false);
            player.GetComponent<character_getApple_trigger>().enabled=false;
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<ThirdPersonCarmera>().enabled = false;
            
            player.GetComponent<ThirdPersonMove>().ResetMove();
            player.GetComponent<ThirdPersonMove>().enabled = false;
           

            appleGenerater.GetComponent<generate_apple>().enabled = false;
            apples = GameObject.FindGameObjectsWithTag("apple");
            foreach (GameObject apple in apples)
            {
                // 检查 apple 对象是否因为某些原因已经为 null
                if (apple != null)
                {
                    Destroy(apple);
                }
            }

            storeShown = true;

        }

    }

    public void ResetScene()
    {
        if(DAY<3)
        {  
            if(DAY==2)
            {
            nextDayText.text = "END";
            }
            store.SetActive(false);
            UI.SetActive(true);
            player.GetComponent<character_getApple_trigger>().enabled = true;
            player.GetComponent<PlayerInput>().enabled = true;
            player.GetComponent<ThirdPersonCarmera>().enabled = true;
            player.GetComponent<ThirdPersonMove>().enabled = true;

            appleGenerater.GetComponent<generate_apple>().enabled = true;
            player.GetComponent<character_getApple_trigger>().Score = 0;
            player.GetComponent< character_getApple_trigger>().text_apple.text= "Score : 000";
       
            time_now.GetComponent<text_time>().nowTime = time_now.GetComponent<text_time>().fullTime;
            time_now.GetComponent<text_time>().show_text();
             storeShown = false;
                DAY++;

          
        }
        else
        {
           GameData.lose=true;
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
        DayText.text = "DAY: " + DAY+"/3";
     }

}
