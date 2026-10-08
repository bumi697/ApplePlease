using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class generate_apple : MonoBehaviour
{
    public float random_time = 3.0f;
    public GameObject apple_red;
    public GameObject apple_yellow;
    public GameObject apple_green;
    public GameObject floor_plane;

    private float bound_x_max;
    private float bound_x_min;
    private float bound_z_max;
    private float bound_z_min;

    public bool isGenerating = true;
    private float delta_time = 0.0f;
    public bool redUP = false;


       public void resetBoundary()
    {
//1.获取物体上的 Collider 组件
        Collider objectCollider = floor_plane.GetComponent<Collider>();

        if (objectCollider != null)
        {
            // 2. 获取边界信息
            Bounds bounds = objectCollider.bounds;
            bound_x_max = bounds.center.x + bounds.size.x / 2;
            bound_x_min = bounds.center.x - bounds.size.x / 2;
            bound_z_max = bounds.center.z + bounds.size.z / 2;
            bound_z_min = bounds.center.z - bounds.size.z / 2;

            Debug.Log("碰撞体边界中心点: " + bounds.center);
            Debug.Log("碰撞体边界尺寸: " + bounds.size);

        }
        else
        {
            Debug.LogError("该物体上没有找到 Collider 组件！");
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        resetBoundary();
    }

    void randomTime()
    {

        // 2. 调用 Next() 方法生成随机数
        float randomNumber = Random.Range(0.0f, 3.0f);
        random_time = randomNumber;
        isGenerating = true;

    }

    void generateApple()
    {
        if(!redUP)
       { int choose = Random.Range(0, 3);
            switch (choose)
            {
                case 0:
                    {
                        Instantiate(apple_red, new Vector3(Random.Range(bound_x_min, bound_x_max), 35.0f, Random.Range(bound_z_min, bound_z_max)), Quaternion.identity);
                        break;
                    }
                case 1:
                    {
                        Instantiate(apple_yellow, new Vector3(Random.Range(bound_x_min, bound_x_max), 35.0f, Random.Range(bound_z_min, bound_z_max)), Quaternion.identity);
                        break;
                    }
                case 2:
                    {
                        Instantiate(apple_green, new Vector3(Random.Range(bound_x_min, bound_x_max), 35.0f, Random.Range(bound_z_min, bound_z_max)), Quaternion.identity);
                        break;
                    }
            }
        }
        else
        {int choose = Random.Range(0, 10);
         if(choose<=6)
            { Instantiate(apple_red, new Vector3(Random.Range(bound_x_min, bound_x_max), 35.0f, Random.Range(bound_z_min, bound_z_max)), Quaternion.identity);

            }
          else if (choose<=8)
                    {
                        Instantiate(apple_yellow, new Vector3(Random.Range(bound_x_min, bound_x_max), 35.0f, Random.Range(bound_z_min, bound_z_max)), Quaternion.identity);
         
                    }
          else
           {
                        Instantiate(apple_green, new Vector3(Random.Range(bound_x_min, bound_x_max), 35.0f, Random.Range(bound_z_min, bound_z_max)), Quaternion.identity);
      
           }
        }


       
        isGenerating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGenerating)
        {
            randomTime();
        }
        else
        {
            delta_time += Time.deltaTime;
            if(delta_time>= random_time)
            {
                delta_time = 0.0f;
                generateApple();
            }

        }
    }
}
