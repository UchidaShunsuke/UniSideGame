using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;    //�ǂݍ��ރV�[����

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //�V�[����ǂݍ���
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
