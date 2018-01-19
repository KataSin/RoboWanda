using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：爆薬
/// 製作者：Ho Siu Ki（何兆祺）
/// </summary>

public class Explosive : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 爆破処理（ゲーム進行タイマーによって起爆）
    public void Explosion()
    {
        Destroy(gameObject);
    }
}
