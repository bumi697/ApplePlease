using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class text_time : MonoBehaviour
{
    public int fullTime=60;//√Î
    public int nowTime;
    private float deltaTime = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        nowTime = fullTime;
        deltaTime = 0.0f;
        show_text();
    }


   public void show_text()
    {  
        if (nowTime % 60 < 10)
                {
                    gameObject.GetComponent<TextMeshProUGUI>().text = nowTime / 60 + ":0" + nowTime % 60;
                }
                else
                { gameObject.GetComponent<TextMeshProUGUI>().text = nowTime / 60 + ":" + nowTime % 60; }

    }

    // Update is called oncebper frame
    void Update()
    {
       if(nowTime>0)
        { 
            if (deltaTime>=1.0f)
            {
                nowTime--;
                deltaTime = 0.0f;

                show_text();
            }
            else
            {
             deltaTime += Time.deltaTime;
            }
       }
    
    }
}
