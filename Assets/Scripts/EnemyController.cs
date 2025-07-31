using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;          //�ړ����x
    public bool isToRight = false;      //true=�E����  false=������
    public float revTime = 0;           //���]����
    public LayerMask groundLayer;       //�n�ʃ��C���[

    float time = 0;

    void Start()
    {
        if (isToRight)
        {
            transform.localScale = new Vector2(-1, 1);  //�����̕ύX
        }
    }

    void Update()
    {
        if(revTime > 0)
        {
            time += Time.deltaTime;
            if(time >= revTime)
            {
                isToRight = !isToRight;     //�t���O�𔽓]������
                time = 0;                   //�^�C�}�[��������
                if (isToRight)
                {
                    transform.localScale = new Vector2(-1, 1);  //�����̕ύX
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);   //�����̕ύX
                }
            }
        }
    }

    void FixedUpdate()
    {
        //�n�㔻��
        bool onGround = Physics2D.CircleCast(transform.position,    //���ˈʒu
                                             0.5f,                  //�~�̔��a
                                             Vector2.down,          //���˕���
                                             0.5f,                  //���ˋ���
                                             groundLayer);          //���o���郌�C���[
        if (onGround)
        {
            //���x���X�V����
            //Rigidbody2D���Ƃ��Ă���
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            if (isToRight)
            {
                rbody.linearVelocity = new Vector2(speed, rbody.linearVelocity.y);
            }
            else
            {
                rbody.linearVelocity = new Vector2(-speed, rbody.linearVelocity.y);
            }
        }
    }

    //�ڑ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isToRight = !isToRight;     //�t���O�𔽓]������
        time = 0;                   //�^�C�}�[��������
        if (isToRight)
        {
            transform.localScale = new Vector2(-1, 1);  //�����̕ύX
        }
        else
        {
            transform.localScale = new Vector2(1, 1);   //�����̕ύX
        }
    }
}
