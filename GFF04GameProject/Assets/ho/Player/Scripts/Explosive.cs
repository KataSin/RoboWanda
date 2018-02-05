using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆発物（作戦第2段階用）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class Explosive : MonoBehaviour
{
    private GameObject m_Timer;     // ゲーム進行タイマー
    [SerializeField]
    private GameObject m_Explosion; // 爆発判定

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
        // 爆破時間になると爆発する

    }

    // 爆破処理（ゲーム進行タイマーによって起爆）
    public void Explosion()
    {
        Destroy(gameObject);    // 爆発物オブジェクトを消す
        Instantiate(m_Explosion, transform.position, Quaternion.identity);  // 爆発判定を発生
    }
}
