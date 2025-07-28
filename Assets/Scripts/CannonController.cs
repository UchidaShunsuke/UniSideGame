using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;        //発生させるPrefabデータ
    public float delayTime = 3.0f;      //遅延時間
    public float fireSpeed = 4.0f;      //発射速度
    public float length = 8.0f;         //範囲

    GameObject player;                  //プレイヤー
    Transform gateTransform;            //発射口のTransform
    float passedTimes = 0;              //経過時間

    //距離チェック
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
