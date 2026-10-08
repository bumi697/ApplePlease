using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class apple_gravity : MonoBehaviour
{
    float gravity = -9.8f;
    private Vector3 velocity = Vector3.zero;
    private bool grounded = false;

    public float hight_float = 3.5f;

    private Vector3 velocity_float = Vector3.zero;
    private float count_time;
    public float high_float= 2f;
    public float speed_float = 3f;

    public float rotationSpeed = 90f;


    public float dispper_time = 5.0f;

    public int score = 1;


    // Start is called before the first frame update
    void Start()
    {
        count_time = 0;
    }

    // Update is called once per frame
    void Update()
    {if(!grounded)
       { if (Physics.Raycast(transform.position, Vector3.down, hight_float))
        {
            // 碰到地面后，将垂直速度设为0，防止它无限下沉
            velocity.y = 0;
            grounded = true;
        }
        else
        {
            // 如果没有落地，则应用重力
            // 每帧都根据重力和时间来更新速度
            // v = v0 + a * t
            velocity.y += gravity * Time.deltaTime;
           
        }


            transform.Translate(velocity * Time.deltaTime, Space.World);
       }
    else{//漂浮旋转
            count_time += Time.deltaTime;
            count_time = count_time % ((float)(Math.PI * 2));//防止溢出
            velocity_float.y = ((float)Math.Cos(count_time * speed_float+0.5*Math.PI))* high_float;//位置移动速度,掉落先向下
            transform.Translate(velocity_float * Time.deltaTime, Space.World);
            transform.Rotate(Vector2.up, rotationSpeed*Time.deltaTime);

            //在5秒内删除
            Destroy(gameObject, dispper_time);
        }


    }
}
