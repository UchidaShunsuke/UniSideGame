using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;     //true = 時間をカウントダウン計測する
    public float gameTime = 0;          //ゲームの最大時間
    public bool isTimeOver = false;     //true = タイマー停止
    public float displayTime = 0;       //表示時間

    float times = 0;     //現在時間
   
    void Start()
    {
        if (isCountDown)
        {
            //カウントダウン
            displayTime = gameTime;
        }
    }

    void Update()
    {
        if(isTimeOver == false)
        {
            times += Time.deltaTime;
            if (isCountDown)
            {
                //カウントダウン
                displayTime = gameTime - times;
                if(displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            else
            {
                //カウントアップ
                displayTime = times;
                if(displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
            Debug.Log("TIMES: " + displayTime);
        }
    }
}
