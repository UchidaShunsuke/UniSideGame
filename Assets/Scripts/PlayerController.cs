using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;             //入力
    public float speed = 3.0f;      //移動速度

    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地できるレイヤー
    bool goJump = false;            //ジャンプ開始フラグ

    public static string gameState = "playing";     //ゲームの状態

    //アニメーション対応
    Animator animator;  //アニメーター
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public int score = 0;       //スコア

    //タッチスクリーン対応追加　※書いただけで未実装
    //bool isMoving = false;

    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();   //Rigidbody2Dを取得
        animator = GetComponent<Animator>();        //Animetorを取得
        nowAnime = stopAnime;                       //停止から開始する
        oldAnime = stopAnime;                       //停止から開始する
        gameState = "playing";                      //ゲーム中にする
    }

    void Update()
    {
        if(gameState != "playing")
        {
            return;     //キャラクター操作させない（static変数）
        }

        //移動    ※書いただけで未実装
        /*
        if (isMoving)
        {
            //水平方向の入力をチェック
        */
        axisH = Input.GetAxisRaw("Horizontal");
        /*
        }
        */

        //向きの調整
        if (axisH > 0.0f)
        {
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if(axisH < 0.0f)
        {
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1); //左右反転
        }

        //キャラクターをジャンプさせる
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;     //キャラクター操作させない（static変数）
        }

        //地上判定
        bool onGround = Physics2D.CircleCast(transform.position,    //発射位置
                                            0.2f,                   //円の半径
                                            Vector2.down,           //発射方法
                                            0.0f,                   //発射距離
                                            groundLayer);           //検出するレイヤー

        if(onGround || axisH != 0)  //地面の上 or 速度が 0 ではない
        {
            //速度を更新
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }

        if(onGround && goJump)  //地面の上でジャンプキーが押された
        {
            //ジャンプする
            Vector2 jumpPw = new Vector2(0, jump);  //ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //瞬発的な力を加える
            goJump = false; //ジャンプフラグを下ろす
        }

        //アニメーション更新
        if (onGround)
        {
            //地面の上
            if(axisH == 0)
            {
                nowAnime = stopAnime;   //停止中
            }
            else
            {
                nowAnime = moveAnime;   //移動
            }
        }
        else
        {
            //空中
            nowAnime = jumpAnime;
        }
        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    //アニメーション再生
        }
    }

    public void Jump()  //ジャンプ
    {
        goJump = true;  //ジャンプフラグを立てる
    }

    //接触開始
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal();     //ゴール
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver(); //ゲームオーバー
        }
        else if(collision.gameObject.tag == "ScoreItem")
        {
            //スコアアイテム
            //ItemDataを得る
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            //スコアを得る
            score = item.value;

            //アイテムを削除する
            Destroy(collision.gameObject);
        }
    }

    //ゴール
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop(); //ゲーム停止
    }

    //ゲームオーバー
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop(); //ゲーム停止
        //=======================
        //ゲームオーバー演出
        //=======================
        //プレイヤー当たりを消す
        GetComponent<CapsuleCollider2D>().enabled = false;
        //プレイヤーを少し上に跳ね上げる演出
        rbody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
    }

    //ゲーム停止
    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();    //Rigidbody2Dを取得
        rbody.linearVelocity = new Vector2(0, 0);           //速度を 0 にして強制停止
    }

    //タッチスクリーン対応追加  ※書いただけで未実装
    /*
    public void SetAxis(float h, float v)
    {
        axisH = h;
        if(axisH == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving - true;
        }
    }
    */
}
