using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    private static readonly object locker = new object();

    private static bool bAppQuitting;

    public static T Instance
    {
        get
        {
            if (bAppQuitting)
            {
                instance = null;
                return instance;
            }

            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogError("不应该存在多个单例！");
                        return instance;
                    }

                    if (instance == null)
                    {
                        var singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton)" + typeof(T);
                        singleton.hideFlags = HideFlags.None;
                        DontDestroyOnLoad(singleton);
                    }
                    else
                        DontDestroyOnLoad(instance.gameObject);
                }
                instance.hideFlags = HideFlags.None;
                return instance;
            }
        }
    }

    private void Awake()
    {
        bAppQuitting = false;
    }

    private void OnDestroy()
    {
        bAppQuitting = true;
    }

    protected virtual void OnStart()
    {
        GameManager.Instance.OnGameStatusChange += GameStatusChange;
        GameManager.Instance.OnBallAccelerateChange += BallAccelerateChange;
    }

    protected virtual void Destroy()
    {
        GameManager.Instance.OnGameStatusChange -= GameStatusChange;
        GameManager.Instance.OnBallAccelerateChange -= BallAccelerateChange;
    }

    protected virtual void GameStatusChange(bool isPause)
    {
        // Do Something
    }

    protected virtual void BallAccelerateChange(bool isAccelerate)
    {
        // Do Something
    }
}
