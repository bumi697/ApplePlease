using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class winORlose : MonoBehaviour 
{
    public string nextSceneName = "end";
    public GameObject player;

    private bool isGameOver = false; // 添加一个标志位，防止重复加载场景

    void Update()
    {
        // 检查玩家是否存在，并且游戏还未结束
        if (player != null && !isGameOver)
        {
            // 检查玩家是否掉落
            if (player.transform.position.y < -50f)
            {
                Debug.Log("玩家掉落，游戏失败！");
                HandleLoseCondition();
            }

        }

        if(player.GetComponent<character_getApple_trigger>().getBigApple)
        {
            Debug.Log("玩家拿到了大苹果！");
            HandleWinCondition();
        }

    }

    private void HandleLoseCondition()
    {
        GameData.lose = true; // 正确地设置静态变量
        LoadEndScene();
    }


    public void HandleWinCondition()
    {
        GameData.Win = true; // 正确地设置静态变量
        LoadEndScene();
    }

    private void LoadEndScene()
    {
        isGameOver = true; // 设置标志位，防止Update中再次触发
        // 使用协程在下一帧加载场景，确保数据已被正确设置
        StartCoroutine(LoadSceneAfterFrame());
    }

    private IEnumerator LoadSceneAfterFrame()
    {
        yield return new WaitForEndOfFrame(); // 等待当前帧结束
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}