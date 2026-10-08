using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasController : MonoBehaviour
{
    public GameObject canvasWin;
    public GameObject canvaslose;


    void Start()
    {if (GameData.Win)
         {   canvasWin.SetActive(true);
            canvaslose.SetActive(false);
        }
      if (GameData.lose)
        {
            canvasWin.SetActive(false);
            canvaslose.SetActive(true);
        }
        GameData.Reset();

    }

}
