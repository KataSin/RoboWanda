using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private Vector3 m_origin_pos;

    private float t, t1, t2;

    private bool isClear;

    [SerializeField]
    private List<GameObject> bills_;
    [SerializeField]
    private List<Vector3> bills_origin_pos;
    [SerializeField]
    private List<Quaternion> bills_origin_rotation;

    [SerializeField]
    private List<GameObject> rebills_;

    [SerializeField]
    private GameObject tower2_;

    [SerializeField]
    private GameObject tower3_;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = new Vector3(0f, -29.3f, 0f);

        t = 0f;
        t1 = 0f;
        t2 = 0f;

        isClear = false;
    }

    public void InitBill()
    {
        for (int i = 0; i < bills_.Count; i++)
        {
            bills_origin_pos[i] = bills_[i].transform.position;
            bills_origin_rotation[i] = bills_[i].transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TowerUp()
    {
        transform.position = Vector3.Lerp(m_origin_pos, Vector3.zero, t / 2f);

        if (t >= 2f)
            isClear = true;

        t += 1.0f * Time.deltaTime;
    }

    public void TowerCheck()
    {
        for (int i = 0; i < bills_.Count; i++)
        {
            if (bills_[i].GetComponent<Break>().Get_BreakFlag())
            {
                Destroy(bills_[i]);
                bills_[i] = Instantiate(rebills_[i],
                    bills_origin_pos[i], bills_origin_rotation[i], tower2_.transform);
            }
        }
    }

    public void TowerCheck2()
    {
        for (int i = 0; i < bills_.Count; i++)
        {
            if (bills_[i].GetComponent<Break>().Get_BreakFlag())
            {
                Destroy(bills_[i]);
                bills_[i] = Instantiate(rebills_[i],
                    bills_origin_pos[i], bills_origin_rotation[i], tower3_.transform);
            }
        }
    }

    public void Tower2Up()
    {
        tower2_.transform.position = Vector3.Lerp(m_origin_pos, Vector3.zero, t1 / 2f);

        t1 += 1.0f * Time.deltaTime;
    }

    public void Tower3Up()
    {
        tower3_.transform.position = Vector3.Lerp(m_origin_pos, Vector3.zero, t2 / 2f);

        t2 += 1.0f * Time.deltaTime;
    }

    public void TowerBreak()
    {
        if (t2 >= 6f)
        {
            for (int i = 0; i < 7; i++)
            {
                bills_[i].GetComponent<Break>().Set_BreakFlag(true);
                bills_[i].GetComponent<Break>().OutBreak_Smoke();
            }
        }
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}
