using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//参考BV1dX2UY9EbC
//冲刺跳跃自己完成

public class ThirdPersonMove : MonoBehaviour
{
    
    public AudioClip[] alternateClips; // 存放要交替的音效
    public AudioSource audioSource;    // 播放音效的AudioSource
    public float minInterval = 0.3f;   // 最小播放间隔（防止音效重叠）
    public float maxInterval = 0.6f;   // 最大播放间隔

    private int currentClipIndex = 0;  // 当前音效索引
    private float timer = 0f;          // 间隔计时器

    public GameObject audio_jump;
    public GameObject audio_dash;
    public GameObject audio_move;

    private CharacterController _controller;
    private GameObject _mainCamera;

    public ParticleSystem jumpParticlePrefab;




    //速度
    //速度标量
    private float currentSpeed;
    //低速
    public float basicSpeed = 15.0f;
    //着陆
    private bool _isGround = true;
   
    //方向    
    //记录之前的速度方向（惯性）
    private Vector3 targetDir;
    float _targetRot = 0.0f;
    public float RotationSmoothTime = 0.1f;
    float _rotationVelocity;
    private void moveDir()
    {  
        if (_move != Vector2.zero)
        {
            // 计算输入方向，将其转换为世界空间
            Vector3 inputDir = new Vector3(_move.x, 0.0f, _move.y).normalized;
            // 计算目标旋转角度
            _targetRot = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg +
                         _mainCamera.transform.eulerAngles.y;
            //平滑计算
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRot,
            ref _rotationVelocity, RotationSmoothTime);

            // 旋转玩家
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            // 计算移动方向
            targetDir = Quaternion.Euler(0.0f, _targetRot, 0.0f) * Vector3.forward;

        }

    }
    //冲刺相关
    enum moveMode{
    _stop=0,
    _Normal=1,
    _isSpeedUp =2,
    _isSprinting =3,
    _isSpeedDown=4

    }
    private moveMode nowMove;

    public float sprintSpeed = 25.0f;
    private float _speedUp;//加速的加速度
    public float speedUpTime = 0.8f;//加速的时间

    private float nowSprintTime = 0.0f;
    public float fullSprintTime = 1.0f;//高速1s
    
    private float _speedDown ;//减速的加速度
    public float speedDownTime = 0.8f;//减速的时间
    void moveSpeedContorl()
    {  
        //停下来=没输入&没减速
        if (_move == Vector2.zero && nowMove <= moveMode._Normal)
        {
            nowMove = moveMode._stop;
        }
        //有输入&没加速
        else if (_move != Vector2.zero && nowMove <= moveMode._Normal)
        {
            nowMove = moveMode._Normal;
        }
        //冲刺阶段只能旋转。


        switch (nowMove)
        {
            case moveMode._stop:
                {
                    currentSpeed = 0.0f;
                    
                    break;
                }
            case moveMode._Normal:
                {
                    Debug.Log("Move");
                    currentSpeed = basicSpeed;
                  
                    break;
                    
             }
            case moveMode._isSpeedUp:
                {
                    currentSpeed = currentSpeed + _speedUp * Time.deltaTime;
                    if (currentSpeed >= sprintSpeed)
                    {
                        currentSpeed = sprintSpeed;
                        nowMove = moveMode._isSprinting;
                    }
                    break;
                }
            case moveMode._isSprinting:
             {
                nowSprintTime += Time.deltaTime;
                    if (nowSprintTime >= fullSprintTime)
                    {
                        nowSprintTime = 0.0f;
                        nowMove = moveMode._isSpeedDown;
                    }
                    break;
             }
            case moveMode._isSpeedDown:
             {  
                   currentSpeed= currentSpeed- _speedDown * Time.deltaTime;
                   if(currentSpeed <= basicSpeed)
                   {      
                        currentSpeed = basicSpeed;
                        nowMove = moveMode._Normal;
                   }
                    break;
             }
        }

      
    }

    //跳跃
    enum jumpMode
    {
        _Normal = 0,
        _isJumpUp = 1,
        _isFallDown = 2

    }
    private jumpMode nowJump;
    private Vector3 dirJumpUP = new Vector3(0, 1, 0);//向上跳跃
    public float JumpSpeed=10.0f;//开始跳跃的速度
    private float nowJumpSpeed = 0.0f;
    public float speedJumpTime = 0.8f;//跳跃的时间
    public float gravity = 9.8f;//可调

    void Jump()
    {   //调速=按了shift并且没有在高速or减速状态
        //着陆判断
            if (!_isGround)
            {
                nowJump = jumpMode._isFallDown;
                Debug.Log("NOTgound");
            }
            else
            {
                Debug.Log("gound");
            }

        switch (nowJump)
        {
            case jumpMode._Normal:
                {
                    nowJumpSpeed = -0.5f;
                    break;
                }
            case jumpMode._isJumpUp:
                {
                    nowJumpSpeed = JumpSpeed;
                    nowJump = jumpMode._isFallDown;
                    break;
                }
            case jumpMode._isFallDown:
                {
                    nowJumpSpeed = nowJumpSpeed - Time.deltaTime * gravity;
                    if(_isGround)
                    {
                        nowJump = jumpMode._Normal;
                    }
                   break;
                }


        }


    }


    public void ResetMove()
    {
        targetDir = new Vector3(0, 0, 0);
        //冲刺
        nowSprintTime = 0.0f;
        nowMove = moveMode._Normal;
        _speedUp = (sprintSpeed - basicSpeed) / speedUpTime;
        _speedDown = (sprintSpeed - basicSpeed) / speedDownTime;//0.8s内减速完成

        //跳跃
        nowJump = jumpMode._Normal;
        _isGround = gameObject.GetComponent<CharacterController>().isGrounded;

    }
    public void PlayAlternateSound()
    {
        // 播放当前索引的音效
        audioSource.PlayOneShot(alternateClips[currentClipIndex]);

        // 更新索引：自增后取模，实现循环
        currentClipIndex = (currentClipIndex + 1) % alternateClips.Length;
    }
    void Start()
    {
        ResetMove();
        //镜头
        if (_mainCamera==null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _controller = GetComponent<CharacterController>();



    // 校验参数
    if (alternateClips == null || alternateClips.Length == 0)
    {
        Debug.LogError("请在Inspector中添加至少一个交替音效！");
        enabled = false;
        return;
    }
    if (audioSource == null)
    {
        audioSource = GetComponent<AudioSource>();
    }

}
    void Update()
    {  
        //速度方向容器,每次清空
        Vector3 moveVelocity = new Vector3(0,0, 0);
        Vector3 jumpVelocity = new Vector3(0, 0, 0);

        _isGround = gameObject.GetComponent<CharacterController>().isGrounded;



      
        //移动=获取改变的方向dir
        moveDir();
        //冲刺OR普通移动的标量
        moveSpeedContorl();
        //跳跃，垂直dir
        Jump();

    

        // 使用计算出的当前速度进行移动
        moveVelocity += targetDir.normalized * (currentSpeed * Time.deltaTime);
        jumpVelocity += dirJumpUP * (nowJumpSpeed * Time.deltaTime);

        // 移动玩家
        _controller.Move(moveVelocity+jumpVelocity);

    // 示例：按间隔自动交替播放（可替换为你的触发条件，比如脚步触发）
       
        if(nowMove==moveMode._Normal)
        {
            timer += Time.deltaTime;
        if(timer >= UnityEngine.Random.Range(minInterval, maxInterval))
        {
            PlayAlternateSound();
            timer = 0f;
        }
        }
            
       





}

    Vector2 _move;
    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        Debug.Log("moving");
      
    }
    private void OnSprint(InputValue value)
    {
        //按下返回true//没在加速中
        if (nowMove== moveMode._Normal && value.isPressed&&_isGround)//空中不能加速
        { nowMove= moveMode._isSpeedUp;
            audio_dash.GetComponent<AudioSource>().Play();
        }
        Debug.Log("sprinting");
    }
    private void OnJump(InputValue value)
    {
        //按下返回true//没在加速中
        if (nowJump == jumpMode._Normal && value.isPressed)
        {
            nowJump = jumpMode._isJumpUp;
            audio_jump.GetComponent<AudioSource>().Play();

            if (jumpParticlePrefab != null)
            {
                // 确定粒子生成的位置（通常在角色脚下）
                Vector3 spawnPosition = transform.position;
                // 你可以稍微向下偏移一点，防止粒子在地面以下
                spawnPosition.y -= 1.5f;

                // 实例化粒子
                Instantiate(jumpParticlePrefab, spawnPosition, Quaternion.identity);
            }
        }

        Debug.Log("Jumping");
    }

}
