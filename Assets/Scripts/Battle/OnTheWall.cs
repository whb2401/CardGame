using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheWall : MonoBehaviour
{
    private Rect rect;

    void Start()
    {
        rect = GameManager.Instance.GetScreenRect();
    }

    void Update()
    { }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameSetting.TAGBALL))
        {
            GameManager.Instance.UpdateHitCount(true, 1);

            if (other.transform.position.y >= rect.height / 2)
            {
                Debug.Log("hit top.");
            }
            else
            {
                if (other.transform.position.x < 0f)
                {
                    Debug.Log("hit left.");
                }
                else if (other.transform.position.x < 0f)
                {
                    Debug.Log("hit right.");
                }
            }
        }
    }
}
