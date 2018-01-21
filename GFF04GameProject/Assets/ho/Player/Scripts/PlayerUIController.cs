using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スクリプト：プレイヤーUI制御
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ExplosionImage;    // 爆発弾残弾の画像
    [SerializeField]
    private GameObject m_FlashGrenadeImage; // 照明弾残弾の画像
    [SerializeField]
    private GameObject m_SmokeGrenadeImage; // スモーク弾残弾の画像

    private BomSpawn.Bom m_Mode;            // UIモード（BomSpawnスクリプトから取得）
    BomSpawn.Bom m_PreviousMode;            // 前のモード

    [SerializeField]
    private float m_PositionSpeedHigh;
    [SerializeField]
    private float m_PositionSpeedLow;
    [SerializeField]
    private float m_ScaleSpeed;

    [SerializeField]
    private GameObject m_BomSpawn;          // 擲弾オブジェクト

    // 動作検証用のダミー変数
    /*
    int m_ModeSelect = 0;                   // モード選択
    bool m_IsTriggered = false;             // 十字キーの操作判定
    */

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

        m_Mode = BomSpawn.Bom.BOM;
        m_PreviousMode = m_Mode;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_Mode)
        {
            case BomSpawn.Bom.BOM:
                // Debug.Log("爆発弾選択中");
                Bomb();
                break;
            case BomSpawn.Bom.LIGHT_BOM:
                // Debug.Log("照明弾選択中");
                FlashGrenade();
                break;
            case BomSpawn.Bom.SMOKE_BOM:
                // Debug.Log("スモーク弾選択中");
                SmokeGrenade();
                break;
        }

        // 現在選択中の弾種を反映
        CheckMode();
    }

    // 選択弾種を更新
    private void CheckMode()
    {
        m_Mode = m_BomSpawn.GetComponent<BomSpawn>().m_Bom;

        if (m_PreviousMode != m_Mode)
            m_PreviousMode = m_Mode;
    }

    // 爆発弾選択中の処理
    void Bomb()
    {
        // 弾表示の新しい座標とスケール
        // 爆発弾（表示順：1）
        Vector3 new_explosion_position = new Vector3(455.0f, -290.0f, 0.0f);
        Vector3 new_explosion_scale = new Vector3(0.5f, 0.5f, 1.0f);

        // 照明弾（表示順：2）
        Vector3 new_flash_position = new Vector3(504.0f, -200.0f, 0.0f);
        Vector3 new_flash_scale = new Vector3(0.35f, 0.35f, 0.0f);

        // スモーク弾（表示順：3）
        Vector3 new_smoke_position = new Vector3(504.0f, -130.0f, 0.0f);
        Vector3 new_smoke_scale = new Vector3(0.35f, 0.35f, 0.0f);

        // 新しい座標に移動し、アイコンサイズが変化
        if (m_PreviousMode == BomSpawn.Bom.LIGHT_BOM)
        {
            // 爆発弾（3 -> 1、快速）
            m_ExplosionImage.transform.localPosition = Vector3.Lerp(m_ExplosionImage.transform.localPosition, new_explosion_position, m_PositionSpeedHigh * Time.deltaTime);
            m_ExplosionImage.transform.localScale = Vector3.Lerp(m_ExplosionImage.transform.localScale, new_explosion_scale, m_ScaleSpeed * Time.deltaTime);
            // 照明弾（1 -> 2、普通）
            m_FlashGrenadeImage.transform.localPosition = Vector3.Lerp(m_FlashGrenadeImage.transform.localPosition, new_flash_position, m_PositionSpeedLow * Time.deltaTime);
            m_FlashGrenadeImage.transform.localScale = Vector3.Lerp(m_FlashGrenadeImage.transform.localScale, new_flash_scale, m_ScaleSpeed * Time.deltaTime);
            // スモーク弾（2 -> 3、普通）
            m_SmokeGrenadeImage.transform.localPosition = Vector3.Lerp(m_SmokeGrenadeImage.transform.localPosition, new_smoke_position, m_PositionSpeedLow * Time.deltaTime);
            m_SmokeGrenadeImage.transform.localScale = Vector3.Lerp(m_SmokeGrenadeImage.transform.localScale, new_smoke_scale, m_ScaleSpeed * Time.deltaTime);
        }
        else
        {
            // 爆発弾（2 -> 1、普通）
            m_ExplosionImage.transform.localPosition = Vector3.Lerp(m_ExplosionImage.transform.localPosition, new_explosion_position, m_PositionSpeedLow * Time.deltaTime);
            m_ExplosionImage.transform.localScale = Vector3.Lerp(m_ExplosionImage.transform.localScale, new_explosion_scale, m_ScaleSpeed * Time.deltaTime);
            // 照明弾（3 -> 2、普通）
            m_FlashGrenadeImage.transform.localPosition = Vector3.Lerp(m_FlashGrenadeImage.transform.localPosition, new_flash_position, m_PositionSpeedLow * Time.deltaTime);
            m_FlashGrenadeImage.transform.localScale = Vector3.Lerp(m_FlashGrenadeImage.transform.localScale, new_flash_scale, m_ScaleSpeed * Time.deltaTime);
            // スモーク弾（1 -> 3、快速）
            m_SmokeGrenadeImage.transform.localPosition = Vector3.Lerp(m_SmokeGrenadeImage.transform.localPosition, new_smoke_position, m_PositionSpeedHigh * Time.deltaTime);
            m_SmokeGrenadeImage.transform.localScale = Vector3.Lerp(m_SmokeGrenadeImage.transform.localScale, new_smoke_scale, m_ScaleSpeed * Time.deltaTime);
        }
    }

    // 照明弾選択中の処理
    void FlashGrenade()
    {
        // 弾表示の新しい座標とスケール
        // 照明弾（表示順：1）
        Vector3 new_flash_position = new Vector3(455.0f, -290.0f, 0.0f);
        Vector3 new_flash_scale = new Vector3(0.5f, 0.5f, 1.0f);

        // スモーク弾（表示順：2）
        Vector3 new_smoke_position = new Vector3(504.0f, -200.0f, 0.0f);
        Vector3 new_smoke_scale = new Vector3(0.35f, 0.35f, 0.0f);

        // 爆発弾（表示順：3）
        Vector3 new_explosion_position = new Vector3(504.0f, -130.0f, 0.0f);
        Vector3 new_explosion_scale = new Vector3(0.35f, 0.35f, 0.0f);

        // 新しい座標に移動し、アイコンサイズが変化
        if (m_PreviousMode == BomSpawn.Bom.BOM)
        {
            // 爆発弾（1 -> 3、快速）
            m_ExplosionImage.transform.localPosition = Vector3.Lerp(m_ExplosionImage.transform.localPosition, new_explosion_position, m_PositionSpeedHigh * Time.deltaTime);
            m_ExplosionImage.transform.localScale = Vector3.Lerp(m_ExplosionImage.transform.localScale, new_explosion_scale, m_ScaleSpeed * Time.deltaTime);
            // 照明弾（2 -> 1、普通）
            m_FlashGrenadeImage.transform.localPosition = Vector3.Lerp(m_FlashGrenadeImage.transform.localPosition, new_flash_position, m_PositionSpeedLow * Time.deltaTime);
            m_FlashGrenadeImage.transform.localScale = Vector3.Lerp(m_FlashGrenadeImage.transform.localScale, new_flash_scale, m_ScaleSpeed * Time.deltaTime);
            // スモーク弾（3 -> 2、普通）
            m_SmokeGrenadeImage.transform.localPosition = Vector3.Lerp(m_SmokeGrenadeImage.transform.localPosition, new_smoke_position, m_PositionSpeedLow * Time.deltaTime);
            m_SmokeGrenadeImage.transform.localScale = Vector3.Lerp(m_SmokeGrenadeImage.transform.localScale, new_smoke_scale, m_ScaleSpeed * Time.deltaTime);
        }
        else
        {
            // 爆発弾（2 -> 3、普通）
            m_ExplosionImage.transform.localPosition = Vector3.Lerp(m_ExplosionImage.transform.localPosition, new_explosion_position, m_PositionSpeedLow * Time.deltaTime);
            m_ExplosionImage.transform.localScale = Vector3.Lerp(m_ExplosionImage.transform.localScale, new_explosion_scale, m_ScaleSpeed * Time.deltaTime);
            // 照明弾（3 -> 1、快速）
            m_FlashGrenadeImage.transform.localPosition = Vector3.Lerp(m_FlashGrenadeImage.transform.localPosition, new_flash_position, m_PositionSpeedHigh * Time.deltaTime);
            m_FlashGrenadeImage.transform.localScale = Vector3.Lerp(m_FlashGrenadeImage.transform.localScale, new_flash_scale, m_ScaleSpeed * Time.deltaTime);
            // スモーク弾（1 -> 2、普通）
            m_SmokeGrenadeImage.transform.localPosition = Vector3.Lerp(m_SmokeGrenadeImage.transform.localPosition, new_smoke_position, m_PositionSpeedLow * Time.deltaTime);
            m_SmokeGrenadeImage.transform.localScale = Vector3.Lerp(m_SmokeGrenadeImage.transform.localScale, new_smoke_scale, m_ScaleSpeed * Time.deltaTime);
        }
    }

    // スモーク弾選択中の処理
    void SmokeGrenade()
    {
        // 弾表示の新しい座標とスケール
        // スモーク弾（表示順：1）
        Vector3 new_smoke_position = new Vector3(455.0f, -290.0f, 0.0f);
        Vector3 new_smoke_scale = new Vector3(0.5f, 0.5f, 1.0f);

        // 爆発弾（表示順：2）
        Vector3 new_explosion_position = new Vector3(504.0f, -200.0f, 0.0f);
        Vector3 new_explosion_scale = new Vector3(0.35f, 0.35f, 0.0f);

        // 照明弾（表示順：3）
        Vector3 new_flash_position = new Vector3(504.0f, -130.0f, 0.0f);
        Vector3 new_flash_scale = new Vector3(0.35f, 0.35f, 0.0f);

        // 新しい座標に移動し、アイコンサイズが変化
        if (m_PreviousMode == BomSpawn.Bom.BOM)
        {
            // 爆発弾（1 -> 2、普通）
            m_ExplosionImage.transform.localPosition = Vector3.Lerp(m_ExplosionImage.transform.localPosition, new_explosion_position, m_PositionSpeedLow * Time.deltaTime);
            m_ExplosionImage.transform.localScale = Vector3.Lerp(m_ExplosionImage.transform.localScale, new_explosion_scale, m_ScaleSpeed * Time.deltaTime);
            // 照明弾（2 -> 3、普通）
            m_FlashGrenadeImage.transform.localPosition = Vector3.Lerp(m_FlashGrenadeImage.transform.localPosition, new_flash_position, m_PositionSpeedLow * Time.deltaTime);
            m_FlashGrenadeImage.transform.localScale = Vector3.Lerp(m_FlashGrenadeImage.transform.localScale, new_flash_scale, m_ScaleSpeed * Time.deltaTime);
            // スモーク弾（3 -> 1、快速）
            m_SmokeGrenadeImage.transform.localPosition = Vector3.Lerp(m_SmokeGrenadeImage.transform.localPosition, new_smoke_position, m_PositionSpeedHigh * Time.deltaTime);
            m_SmokeGrenadeImage.transform.localScale = Vector3.Lerp(m_SmokeGrenadeImage.transform.localScale, new_smoke_scale, m_ScaleSpeed * Time.deltaTime);
        }
        else
        {
            // 爆発弾（3 -> 2、普通）
            m_ExplosionImage.transform.localPosition = Vector3.Lerp(m_ExplosionImage.transform.localPosition, new_explosion_position, m_PositionSpeedLow * Time.deltaTime);
            m_ExplosionImage.transform.localScale = Vector3.Lerp(m_ExplosionImage.transform.localScale, new_explosion_scale, m_ScaleSpeed * Time.deltaTime);
            // 照明弾（1 -> 3、快速）
            m_FlashGrenadeImage.transform.localPosition = Vector3.Lerp(m_FlashGrenadeImage.transform.localPosition, new_flash_position, m_PositionSpeedHigh * Time.deltaTime);
            m_FlashGrenadeImage.transform.localScale = Vector3.Lerp(m_FlashGrenadeImage.transform.localScale, new_flash_scale, m_ScaleSpeed * Time.deltaTime);
            // スモーク弾（2 -> 1、普通）
            m_SmokeGrenadeImage.transform.localPosition = Vector3.Lerp(m_SmokeGrenadeImage.transform.localPosition, new_smoke_position, m_PositionSpeedLow * Time.deltaTime);
            m_SmokeGrenadeImage.transform.localScale = Vector3.Lerp(m_SmokeGrenadeImage.transform.localScale, new_smoke_scale, m_ScaleSpeed * Time.deltaTime);
        }
    }
}