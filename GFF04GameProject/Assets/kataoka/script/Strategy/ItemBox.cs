using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public enum Item
    {
        SMOKE_BULLET,
        LIGHT_BULLET
    }
    public Item m_Item;
    private Rigidbody m_Rb;
    // Use this for initialization
    void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Rb.velocity.magnitude == 0.0f)
        {
            m_Rb.isKinematic = true;
        }
    }
    /// <summary>
    /// アイテムの種類を取得
    /// </summary>
    public Item GetItemState()
    {
        return m_Item;
    }
}
