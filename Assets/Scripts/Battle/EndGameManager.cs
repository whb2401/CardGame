using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    private BallController ballController;
    private UIManager uIManager;

    void Start()
    {
        ballController = FindObjectOfType<BallController>();
        uIManager = FindObjectOfType<UIManager>();
    }

    void Update()
    { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == GameSetting.TAGBRICKSQUARE || other.gameObject.tag == GameSetting.TAGBRICKTRIANGLE)
        {
            ballController.currentBallState = BallController.BallState.endGame;
            uIManager.EndGame();
        }
    }

    public void Retry()
    {
        GameManager.Instance.EnterLevel(-1);
        ballController.currentBallState = BallController.BallState.aim;
    }

    public void Quit()
    {
        Singleton<Me>.Instance.Save();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
