using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class store_work : MonoBehaviour
{
    public TextMeshProUGUI money_text;
    public GameObject player;
    public GameObject show_store;
    public GameObject floor;
    public GameObject apple_generator;

    private int score = 0;

    private bool moneyCount = false;
    private int money = 0;

    public UnityEngine.UI.Button Btn01;
    public TextMeshProUGUI text01;
    public int cost01 = 30;
    public UnityEngine.UI.Button Btn02;
    public TextMeshProUGUI text02;
    public int cost02 = 50;
    public UnityEngine.UI.Button Btn03;
    public TextMeshProUGUI text03;
    public int cost03 = 70;

    // Start is called before the first frame update
    void Start()
    {
        text01.text = cost01+  "R";
        text02.text = cost02 + "R";
        text03.text = cost03 + "R";

    }

    // Update is called once per frame
    void Update()
    {   
        if(show_store.GetComponent<show_store>().storeShown)
       { score = player.GetComponent<character_getApple_trigger>().Score;
        if(!moneyCount)
           { money += score *2;//累加
                moneyCount = true;
            }
         money_text.text = "Money: " + money;
        }
        else
        {
            moneyCount = false;
        }

    }

    public void BuyIten01()
    {
        if(money>=cost01)
        {
            money -= cost01;
            
            apple_generator.GetComponent<generate_apple>().redUP = true;
            
         
            Btn01.interactable = false;
        }
      

    }

    public void BuyIten02()
    {
        if (money >=cost02)
        {
            money -= cost02;
            player.GetComponent<ThirdPersonMove>().basicSpeed = 25; 
          
            Btn02.interactable = false;
        }
        
    }

    public void BuyIten03()
    {
        Debug.Log("clicked");

        if (money >=cost03)
        {
            money -= cost03;
            Vector3 currentScale = floor.transform.localScale;
            // 将其放大两倍
            Vector3 newScale = currentScale * 2.0f;
            // 将新的缩放值赋回给 floor
            floor.transform.localScale = newScale;   
            apple_generator.GetComponent<generate_apple>().resetBoundary();
            //player.GetComponent<player_bound>().resetBoundary();
            Btn03.interactable = false;
        }
       
    }

}
