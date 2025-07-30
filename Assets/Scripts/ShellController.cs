using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;     //íœ‚·‚éŠÔw’è

    void Start()
    {
        Destroy(gameObject, deleteTime);    //íœİ’è
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);    //‰½‚©‚ÉÚG‚µ‚½‚çÁ‚·
    }
}
