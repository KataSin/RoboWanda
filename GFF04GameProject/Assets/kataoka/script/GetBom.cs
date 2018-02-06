using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBom : MonoBehaviour
{
    private BomSpawn m_Spawn;
    private BomUI m_BomUi;
    void Start()
    {
        m_Spawn = GameObject.FindGameObjectWithTag("BomSpawn").GetComponent<BomSpawn>();
        m_BomUi = GameObject.FindGameObjectWithTag("BomUi").GetComponent<BomUI>();
    }

    void Update()
    {

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "AmmoBox")
        {
            BomSpawn.Bom bom = other.gameObject.transform.parent.GetComponent<ItemBox>().GetItemState();

            if (m_Spawn.GetUseBom().IndexOf(bom) == -1)
            {
                m_BomUi.SetDraw(true);
                m_BomUi.SetPosition(other.gameObject);
                int index = m_Spawn.GetUseBom().IndexOf(bom);
                if (Input.GetButton("Cancel"))
                {
                    m_Spawn.AddBom(bom);
                }
            }
            else
                m_BomUi.SetDraw(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "AmmoBox")
        {
            m_BomUi.SetDraw(false);
        }
    }
}
