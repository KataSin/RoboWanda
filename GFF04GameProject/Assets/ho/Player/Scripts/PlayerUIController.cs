using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：プレイヤーUI制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class PlayerUIController : MonoBehaviour
{
    enum PlayerUIMode   // UIモード
    {
        Bomb,           // 爆弾
        FlashGrenade,   // 閃光弾
        SmokeBall       // 煙玉
    }

    [SerializeField]
    private GameObject m_BomSpawn;          // 擲弾オブジェクト
    [SerializeField]
    private GameObject m_ExplosionImage;    // 爆弾残弾の画像
    [SerializeField]
    private GameObject m_FlashGrenadeImage; // 閃光弾残弾の画像

    PlayerUIMode m_Mode;                    // UIモード

    // Use this for initialization
    void Start()
    {
        // 擲弾オブジェクトが存在しない場合、強制終了
        if (m_BomSpawn == null)
        {
            Debug.Log("エラー発生したので終了します");
            Debug.Log("Error Log：擲弾オブジェクトが存在しない");
            Application.Quit();
        }

        m_Mode = PlayerUIMode.Bomb;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_Mode)
        {
            case PlayerUIMode.Bomb:
                Debug.Log("爆弾選択中");
                Bomb();
                break;
            case PlayerUIMode.FlashGrenade:
                Debug.Log("閃光弾選択中");
                FlashGrenade();
                break;
            case PlayerUIMode.SmokeBall:
                Debug.Log("煙玉選択中");
                SmokeBall();
                break;
        }

        CheckMode();
    }

    // 選択弾種を更新
    private void CheckMode()
    {
        switch (m_BomSpawn.GetComponent<BomSpawn>().GetMode())
        {
            case 0:
                m_Mode = PlayerUIMode.Bomb;
                break;
            case 1:
                m_Mode = PlayerUIMode.FlashGrenade;
                break;
            case 2:
                m_Mode = PlayerUIMode.SmokeBall;
                break;
            default:
                m_Mode = PlayerUIMode.Bomb;
                break;
        }
    }

    // 爆弾選択中の処理
    void Bomb()
    {
        m_ExplosionImage.SetActive(true);
    }

    // 閃光弾選択中の処理
    void FlashGrenade()
    {
        m_ExplosionImage.SetActive(false);
    }

    // 煙玉選択中の処理
    void SmokeBall()
    {
        m_ExplosionImage.SetActive(false);
    }
}