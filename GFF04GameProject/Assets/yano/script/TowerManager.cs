using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private Vector3 m_origin_pos;

    private float t, t1, t2;

    private bool isClear;
    private bool isColorClear1, isColorClear2, isColorClear3, isColorClear4;

    [SerializeField]
    private List<GameObject> bills_;
    [SerializeField]
    private List<Vector3> bills_origin_pos;
    [SerializeField]
    private List<Quaternion> bills_origin_rotation;

    [SerializeField]
    private List<GameObject> rebills_;

    [SerializeField]
    private List<GameObject> bills_other_;

    [SerializeField]
    private GameObject tower2_;

    [SerializeField]
    private GameObject tower3_;

    [SerializeField]
    private GameObject tower4_;

    // Use this for initialization
    void Start()
    {
        m_origin_pos = new Vector3(0f, -29.3f, 0f);

        t = 0f;
        t1 = 0f;
        t2 = 0f;

        isClear = false;
        isColorClear1 = false;
        isColorClear2 = false;
        isColorClear3 = false;
        isColorClear4 = false;
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

        for (int i = 0; i < bills_.Count; i++)
            bills_[i].GetComponent<TowerMKCnt>().Instantiate(t);

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

    public void Tower4Up()
    {
        tower4_.transform.position = Vector3.Lerp(new Vector3(0f, -21.4f, 0f), Vector3.zero, t2 / 2f);

        t2 += 1.0f * Time.deltaTime;
    }

    public void TowerBreak()
    {
        if (t2 >= 4f)
        {
            BeforeBreakColor4();
            if (t2 >= 6f)
            {
                for (int i = 0; i < 7; i++)
                {
                    bills_[i].GetComponent<Break>().Set_BreakFlag(true);
                    bills_[i].GetComponent<Break>().OutBreak_Smoke();
                }

                for (int i = 0; i < 6; i++)
                {
                    bills_other_[i].GetComponent<Break_ST>().Set_BreakFlag(true);
                    bills_other_[i].GetComponent<Break_ST>().OutBreak_Smoke();
                }
            }
        }
    }

    public void BeforeBreakColor1()
    {
        if (!isColorClear1)
        {
            bills_[11].GetComponent<TowerMKCnt>().BeforeBreakColor();
            bills_[12].GetComponent<TowerMKCnt>().BeforeBreakColor();

            isColorClear1 = true;
        }
    }

    public void BeforeBreakColor2()
    {
        if (!isColorClear2)
        {
            bills_[2].GetComponent<TowerMKCnt>().BeforeBreakColor();
            bills_[3].GetComponent<TowerMKCnt>().BeforeBreakColor();
            bills_[4].GetComponent<TowerMKCnt>().BeforeBreakColor();
            bills_[6].GetComponent<TowerMKCnt>().BeforeBreakColor();

            isColorClear2 = true;
        }
    }

    public void BeforeBreakColor3()
    {
        if (!isColorClear3)
        {
            for (int i = 0; i < bills_.Count; i++)
                bills_[i].GetComponent<TowerMKCnt>().BeforeBreakColor();

            isColorClear3 = true;
        }
    }

    public void BeforeBreakColor4()
    {
        if (!isColorClear4)
        {
            for (int i = 0; i < 7; i++)
                bills_[i].GetComponent<TowerMKCnt>().BeforeBreakColor();

            for (int i = 0; i < 6; i++)
                bills_other_[i].GetComponent<TowerMKCnt>().BeforeBreakColor();

            isColorClear4 = true;
        }
    }

    public bool Get_Clear()
    {
        return isClear;
    }
}
