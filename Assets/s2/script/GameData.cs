using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // 声明你需要在场景间传递的变量
    public static bool Win = false;
    public static bool lose = false;
    // 你也可以添加方法来重置数据
    public static void Reset()
    {
        Win = false;
        lose = false;
    }


}

