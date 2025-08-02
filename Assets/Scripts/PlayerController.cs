using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;             //����
    public float speed = 3.0f;      //�ړ����x

    public float jump = 9.0f;       //�W�����v��
    public LayerMask groundLayer;   //���n�ł��郌�C���[
    bool goJump = false;            //�W�����v�J�n�t���O

    public static string gameState = "playing";     //�Q�[���̏��

    //�A�j���[�V�����Ή�
    Animator animator;  //�A�j���[�^�[
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";

    public int score = 0;       //�X�R�A

    //�^�b�`�X�N���[���Ή��ǉ��@�������������Ŗ�����
    //bool isMoving = false;

    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();   //Rigidbody2D���擾
        animator = GetComponent<Animator>();        //Animetor���擾
        nowAnime = stopAnime;                       //��~����J�n����
        oldAnime = stopAnime;                       //��~����J�n����
        gameState = "playing";                      //�Q�[�����ɂ���
    }

    void Update()
    {
        if(gameState != "playing")
        {
            return;     //�L�����N�^�[���삳���Ȃ��istatic�ϐ��j
        }

        //�ړ�    �������������Ŗ�����
        /*
        if (isMoving)
        {
            //���������̓��͂��`�F�b�N
        */
        axisH = Input.GetAxisRaw("Horizontal");
        /*
        }
        */

        //�����̒���
        if (axisH > 0.0f)
        {
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
        }
        else if(axisH < 0.0f)
        {
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1); //���E���]
        }

        //�L�����N�^�[���W�����v������
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;     //�L�����N�^�[���삳���Ȃ��istatic�ϐ��j
        }

        //�n�㔻��
        bool onGround = Physics2D.CircleCast(transform.position,    //���ˈʒu
                                            0.2f,                   //�~�̔��a
                                            Vector2.down,           //���˕��@
                                            0.0f,                   //���ˋ���
                                            groundLayer);           //���o���郌�C���[

        if(onGround || axisH != 0)  //�n�ʂ̏� or ���x�� 0 �ł͂Ȃ�
        {
            //���x���X�V
            rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
        }

        if(onGround && goJump)  //�n�ʂ̏�ŃW�����v�L�[�������ꂽ
        {
            //�W�����v����
            Vector2 jumpPw = new Vector2(0, jump);  //�W�����v������x�N�g�������
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�u���I�ȗ͂�������
            goJump = false; //�W�����v�t���O�����낷
        }

        //�A�j���[�V�����X�V
        if (onGround)
        {
            //�n�ʂ̏�
            if(axisH == 0)
            {
                nowAnime = stopAnime;   //��~��
            }
            else
            {
                nowAnime = moveAnime;   //�ړ�
            }
        }
        else
        {
            //��
            nowAnime = jumpAnime;
        }
        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);    //�A�j���[�V�����Đ�
        }
    }

    public void Jump()  //�W�����v
    {
        goJump = true;  //�W�����v�t���O�𗧂Ă�
    }

    //�ڐG�J�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal();     //�S�[��
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver(); //�Q�[���I�[�o�[
        }
        else if(collision.gameObject.tag == "ScoreItem")
        {
            //�X�R�A�A�C�e��
            //ItemData�𓾂�
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            //�X�R�A�𓾂�
            score = item.value;

            //�A�C�e�����폜����
            Destroy(collision.gameObject);
        }
    }

    //�S�[��
    public void Goal()
    {
        animator.Play(goalAnime);

        gameState = "gameclear";
        GameStop(); //�Q�[����~
    }

    //�Q�[���I�[�o�[
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop(); //�Q�[����~
        //=======================
        //�Q�[���I�[�o�[���o
        //=======================
        //�v���C���[�����������
        GetComponent<CapsuleCollider2D>().enabled = false;
        //�v���C���[��������ɒ��ˏグ�鉉�o
        rbody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
    }

    //�Q�[����~
    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();    //Rigidbody2D���擾
        rbody.linearVelocity = new Vector2(0, 0);           //���x�� 0 �ɂ��ċ�����~
    }

    //�^�b�`�X�N���[���Ή��ǉ�  �������������Ŗ�����
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
