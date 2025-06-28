using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;    //読み込むシーン名

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //シーンを読み込む
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
