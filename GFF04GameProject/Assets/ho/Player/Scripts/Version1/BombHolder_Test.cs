using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト：ボム保持点
/// 製作者：Ho Siu Ki（何　兆祺）
/// </summary>
public class BombHolder_Test : MonoBehaviour
{
    [SerializeField]
    private GameObject m_BombPrefab;    // ボムのプレハブ

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // RBボタンを押すと、ボムを生成
        if (Input.GetButtonDown("Bomb_Hold"))
        {
            GameObject bomb = Instantiate(m_BombPrefab);
            bomb.transform.position = transform.position;
        }
    }
}