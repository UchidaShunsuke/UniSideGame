using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;     //削除する時間指定

    void Start()
    {
        Destroy(gameObject, deleteTime);    //削除設定
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);    //何かに接触したら消す
    }
}
