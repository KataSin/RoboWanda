using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆弾の爆発
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>
public class BombExplosion : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // 出現後0.5秒、消滅
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
