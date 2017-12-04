using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private AsyncOperation asyncOp_;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //シーン切り替え
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //シーンの読み込み、切り替え
    public IEnumerator SceneLoad(string sceneName)
    {
        asyncOp_ = SceneManager.LoadSceneAsync(sceneName);
        asyncOp_.allowSceneActivation = false;

        while (asyncOp_.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        asyncOp_.allowSceneActivation = true;
    }

    //現在のシーンの名前を取得
    public string Get_CurrentScene()
    {
        return SceneManager.GetActiveScene().ToString();
    }
}
