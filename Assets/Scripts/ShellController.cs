using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;     //�폜���鎞�Ԏw��

    void Start()
    {
        Destroy(gameObject, deleteTime);    //�폜�ݒ�
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);    //�����ɐڐG���������
    }
}
