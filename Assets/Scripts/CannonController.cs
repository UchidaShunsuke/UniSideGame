using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;        //����������Prefab�f�[�^
    public float delayTime = 3.0f;      //�x������
    public float fireSpeed = 4.0f;      //���ˑ��x
    public float length = 8.0f;         //�͈�

    GameObject player;                  //�v���C���[
    Transform gateTransform;            //���ˌ���Transform
    float passedTimes = 0;              //�o�ߎ���

    //�����`�F�b�N
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if(length >= d)
        {
            ret = true;
        }
        return ret;
    }

    void Start()
    {
        //���ˌ��I�u�W�F�N�g��Transform���擾
        gateTransform = transform.Find("gate");
        //�v���C���[���擾
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //�ҋ@���ԉ��Z
        passedTimes += Time.deltaTime;
        //Player�Ƃ̋����`�F�b�N
        if (CheckLength(player.transform.position))
        {
            //�ҋ@���Ԍo��
            if (passedTimes > delayTime)
            {
                passedTimes = 0;        //���Ԃ��O�Ƀ��Z�b�g
                //�C�e���v���n�u������
                Vector2 pos = new Vector2(gateTransform.position.x, gateTransform.position.y);
                GameObject odj = Instantiate(objPrefab, pos, Quaternion.identity);
                //�C�g�������Ă�������ɔ��˂���
                Rigidbody2D rbody = objPrefab.GetComponent<Rigidbody2D>();
                float angleZ = transform.localEulerAngles.z;
                float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
                Vector2 v = new Vector2(x, y) * fireSpeed;
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    //�͈͕\��
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }
}
