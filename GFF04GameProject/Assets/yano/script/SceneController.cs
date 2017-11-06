using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //シーン切り替え
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //現在のシーンの名前を取得
    public string Get_CurrentScene()
    {
        return SceneManager.GetActiveScene().ToString();
    }
}
