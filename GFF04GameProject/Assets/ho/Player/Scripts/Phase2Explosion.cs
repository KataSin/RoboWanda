using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆発物の爆発判定（作戦第2段階用）
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class Phase2Explosion : MonoBehaviour
{
    [SerializeField]
    [Header("爆発エフェクト")]
    private GameObject m_ExplosionEffect;

    // Use this for initialization
    void Start()
    {
        Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);    // 爆発エフェクトを発生

        // 出現後0.5秒、消滅
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
