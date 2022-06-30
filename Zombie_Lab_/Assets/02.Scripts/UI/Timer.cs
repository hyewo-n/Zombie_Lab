using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float LimitTime = 180.0f;
    public Text text_Timer;

    public Image timeOverScreen;
    public Image timeOver;

    // Update is called once per frame
    void Update() {

        if (LimitTime > 0.0f)
        {
            LimitTime -= Time.deltaTime;
            text_Timer.text = "" + Mathf.Round(LimitTime);
           
        }
        else
        {
            //LimitTime = 0.0f;
            print("TIME OVER");
            TimeOver_Manager.gameOver = true;
        }
    }
}
