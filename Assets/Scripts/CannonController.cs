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

        return ret;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
