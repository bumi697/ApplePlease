using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))//左键
        {//从摄像机到点击位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //存射线返回信息
            RaycastHit hitInfo;
            //得到返回信息
            if(Physics.Raycast(ray,out hitInfo))
            {   
                //获得返回物体
                GameObject gameObj = hitInfo.collider.gameObject;
                
                //画线
                Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
                //打印出被击中物体的名字
                Debug.Log("射线击中了: " + gameObj.name);
                
                //判断标签删除
                if(gameObj.tag=="bonus")
                {
                    Destroy(gameObj,1.0f);
                }
            }

        }
    }
}
