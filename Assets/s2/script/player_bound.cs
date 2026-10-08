using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bound : MonoBehaviour
{
        // 1. 在 Inspector 中拖拽你的地板或边界对象
        public GameObject boundaryObject;

        private Transform playerTransform;
        private Bounds boundary;



        public void resetBoundary()
        { // 获取玩家自身的 Transform 组件
            playerTransform = GetComponent<Transform>();

            // 从边界对象获取 Collider 来计算边界
            if (boundaryObject != null)
            {
                Collider col = boundaryObject.GetComponent<Collider>();
                if (col != null)
                {
                    boundary = col.bounds;
                    Debug.Log("边界已设置: " + boundary);
                }
                else
                {
                    Debug.LogError("边界对象上没有 Collider 组件！", this);
                }
            }
            else
            {
                Debug.LogError("请在 Inspector 中为 PlayerBounds 脚本指定一个边界对象！", this);
            }

        }
        void Start()
        {
        resetBoundary();
        }

        // 2. 使用 LateUpdate 来修正位置
        // LateUpdate 会在所有 Update 函数执行完毕后被调用，确保我们修正的是最终位置
        void LateUpdate()
        {
            if (boundaryObject == null) return;

            // 获取玩家当前的位置
            Vector3 newPosition = playerTransform.position;

            // 使用 Mathf.Clamp 来限制 X 和 Z 轴的位置
            // Mathf.Clamp(value, min, max) 会返回一个被限制在 min 和 max 之间的值
            newPosition.x = Mathf.Clamp(newPosition.x, boundary.min.x, boundary.max.x);
            newPosition.z = Mathf.Clamp(newPosition.z, boundary.min.z, boundary.max.z);

            // 如果你也想限制 Y 轴（比如高度）
            // newPosition.y = Mathf.Clamp(newPosition.y, boundary.min.y, boundary.max.y);

            // 将修正后的位置赋值给玩家
            playerTransform.position = newPosition;
        }
}
