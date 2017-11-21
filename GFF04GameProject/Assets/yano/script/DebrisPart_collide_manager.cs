using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisPart_collide_manager : MonoBehaviour
{
    [SerializeField]
    [Header("部分の当たり判定")]
    private List<GameObject> debrisParts_collide_;

    [SerializeField]
    [Header("破片パーツ")]
    private List<GameObject> debrisParts_obj_;

    [SerializeField]
    private GameObject parentBill_obj_;

    [SerializeField]
    private GameObject originBill_obj_;

    [SerializeField]
    private List<GameObject> afterBills_obj_;

    private bool isClear;

    // Use this for initialization
    void Start()
    {
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collapse_Parts();
    }

    //破片崩壊
    private void Collapse_Parts()
    {
        if ((debrisParts_collide_[2].GetComponent<DebrisPart_collide>().Get_BreakFlag()
            ||
            debrisParts_collide_[1].GetComponent<DebrisPart_collide>().Get_BreakFlag()
            ||
            debrisParts_collide_[0].GetComponent<DebrisPart_collide>().Get_BreakFlag()) && !isClear)
        {
            if (debrisParts_collide_[0].GetComponent<DebrisPart_collide>().Get_BreakFlag()
                ||
                debrisParts_collide_[1].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            {
                for (int i = 0; i < Get_Parts_collide(); i++)
                {
                    Instantiate(debrisParts_obj_[i], debrisParts_collide_[i].transform.position - (new Vector3(0f, 30f * i, 0f)), debrisParts_collide_[i].transform.rotation);
                    Destroy(debrisParts_collide_[i]);
                }
            }

            else if (debrisParts_collide_[2].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            {
                for (int i = 2; i < Get_Parts_collide(); i++)
                {
                    Instantiate(debrisParts_obj_[i], debrisParts_collide_[i].transform.position, debrisParts_collide_[i].transform.rotation);
                    Destroy(debrisParts_collide_[i]);
                }
            }

            Destroy(originBill_obj_);

            if (debrisParts_collide_[2].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            {
                Instantiate(
                afterBills_obj_[0],
                originBill_obj_.transform.position + new Vector3(0f, 30f, 0f),
                originBill_obj_.transform.rotation,
                parentBill_obj_.transform
                );
            }

            else if (debrisParts_collide_[1].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            {
                Instantiate(
                afterBills_obj_[1],
                originBill_obj_.transform.position,
                originBill_obj_.transform.rotation,
                parentBill_obj_.transform
                );
            }

            else if (debrisParts_collide_[0].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            {
                Instantiate(
                afterBills_obj_[0],
                originBill_obj_.transform.position,
                originBill_obj_.transform.rotation,
                parentBill_obj_.transform
                );
            }

            isClear = true;
        }
    }

    //部分の当たり判定の個数取得
    private int Get_Parts_collide()
    {
        if (debrisParts_collide_[2].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            return 3;

        else if (debrisParts_collide_[1].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            return 2;

        else if (debrisParts_collide_[0].GetComponent<DebrisPart_collide>().Get_BreakFlag())
            return 1;

        else return 0;
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}
