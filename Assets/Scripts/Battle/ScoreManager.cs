using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreNumber;
    public int score = 0;

    void Start()
    { }

    void Update()
    {
        scoreNumber.text = "" + score;
    }

    public void IncreaseScore()
    {
        score += 1;
    }
}
