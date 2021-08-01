using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject objArrow;
    public Transform[] balls;

    public Rigidbody rgbBall;

    private Vector3 clickPosition;
    private float constantSpeed = 25f;

    void Start()
    { }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var ballVelocityX = -3 - transform.position.x;
            var ballVelocityZ = -6 - transform.position.z;
            rgbBall.velocity = constantSpeed * new Vector3(ballVelocityX, 0.6f, ballVelocityZ).normalized;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (objArrow == null) return;
            var startPos = objArrow.transform.position;

            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var movement = clickPosition - startPos;
            float theta = Mathf.Rad2Deg * Mathf.Atan(movement.x / movement.y);
            objArrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);

            // 单位化（得到长度为1的单位向量）
            var line = movement.normalized;
            print("normalized: " + line);
            // 得到距离
            float distance = movement.sqrMagnitude;
            float velocity = Mathf.Sqrt((movement.x * movement.x) + (movement.y * movement.y));
            print("distance: " + velocity);

            var hit = Physics2D.Raycast(startPos, movement);
            if (hit.collider != null)
            {
                Debug.DrawLine(startPos, hit.point);
            }
        }
    }
}
