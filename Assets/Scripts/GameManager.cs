using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;    //画像を持つGameObject
    public Sprite gameOverSpr;      //GAME OVER画像
    public Sprite gameClearSpr;     //GAME CLEAR画像
    public GameObject panel;        //パネル
    public GameObject restartButton;//RESTARTボタン
    public GameObject nextButton;   //NEXTボタン

    Image titleImage;               //画像を表示しているImageコンポーネント

    void Start()
    {
        //画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        //ボタン（パネル）を非表示にする
        panel.SetActive(false);
    }

    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {
            //ゲームクリア
            mainImage.SetActive(true);      //画像を表示する
            panel.SetActive(true);          //ボタン（パネル）を表示する
            //RESTARTボタンを無効化する
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;  //画像を設定する
            PlayerController.gameState = "gameend";
        }
        else if(PlayerController.gameState == "gameover")
        {
            //ゲームオーバー
            mainImage.SetActive(true);      //画像を表示する
            panel.SetActive(true);          //ボタン（パネル）を表示する
            //NEXTボタンを無効化する
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;  //画像を設定する
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "playing")
        {
            //ゲーム中
        }
    }
    //画像を非表示にする
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
