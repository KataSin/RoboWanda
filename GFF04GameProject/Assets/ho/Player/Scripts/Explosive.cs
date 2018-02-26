using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆発物（作戦第2段階用）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class Explosive : MonoBehaviour
{
    private GameObject m_Timer;             // ゲーム進行タイマー
    [SerializeField]
    [Header("起爆時間（秒）")]
    private int m_DetonationTime = 180;     // 起爆時間
    [SerializeField]
    private GameObject m_Explosion;         // 爆発判定

    GameObject m_Buliding;                  // 設置されたビル

    // Use this for initialization
    void Start()
    {
        // ゲーム進行タイマーを取得
        if (GameObject.FindGameObjectWithTag("Timer"))
            m_Timer = GameObject.FindGameObjectWithTag("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        // mキーを押すと起爆（動作確認用）
        if (Input.GetKeyDown(KeyCode.M))
        {
            Explosion();
        }

        if (m_Timer == null) return;
        // Debug.Log(m_Timer);

        float time = m_Timer.GetComponent<Timer>().Get_ElapsedTimer();
        // Debug.Log("経過時間：" + time);

        if (time >= m_DetonationTime)
        {
            Explosion();
        }
    }

    // 爆破処理（ゲーム進行タイマーによって起爆）
    public void Explosion()
    {
        Destroy(gameObject);    // 爆発物オブジェクトを消す
        Instantiate(m_Explosion, transform.position, Quaternion.identity);  // 爆発判定を発生
    }

    // 設置されたビルを登録
    public void AddBuilding(GameObject buliding)
    {
        m_Buliding = buliding;
    }

    // 設置されたビルを返す
    public GameObject GetBuliding()
    {
        return m_Buliding;
    }
}