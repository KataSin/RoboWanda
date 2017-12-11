using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private AsyncOperation asyncOp_;

    [SerializeField]
    private int m_nextScene;

    private GameObject gauge_;
    private Image gauge_img_;

    private GameObject gauge_text_obj_;
    private Text gauge_text_;

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
            if (GameObject.FindGameObjectWithTag("LoadingGauge"))
            {
                gauge_ = GameObject.FindGameObjectWithTag("LoadingGauge");
                gauge_img_ = gauge_.GetComponent<Image>();
            }
            if (GameObject.FindGameObjectWithTag("LoadingText"))
            {
                gauge_text_obj_ = GameObject.FindGameObjectWithTag("LoadingText");
                gauge_text_ = gauge_text_obj_.GetComponent<Text>();
            }

            if (gauge_ != null)
            {
                gauge_img_.fillAmount = Mathf.Clamp01(asyncOp_.progress / 0.9f);
            }

            if (gauge_text_ != null)
            {
                gauge_text_.text = (asyncOp_.progress * 100f).ToString("F0");
            }

            yield return new WaitForEndOfFrame();
        }

        if (gauge_ != null)
            gauge_img_.fillAmount = 1f;
        if (gauge_text_ != null)
            gauge_text_.text = "100";

        yield return new WaitForSeconds(1);

        asyncOp_.allowSceneActivation = true;
    }

    //現在のシーンの名前を取得
    public string Get_CurrentScene()
    {
        return SceneManager.GetActiveScene().ToString();
    }

    public int GetNextScene()
    {
        return m_nextScene;
    }

    public void SetNextScene(int scene)
    {
        m_nextScene = scene;
    }
}
