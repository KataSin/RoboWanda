using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingStTank : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tank_spawnPoints_;

    [SerializeField]
    private List<GameObject> tank_goPoints_;

    [SerializeField]
    private GameObject tank_;

    private bool isSpawn;

    private bool isClear;

    private GameObject cloneTank_;

    // Use this for initialization
    void Start()
    {
        isSpawn = false;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn && !isClear)
        {
            for (int i = 0; i < tank_spawnPoints_.Count; i++)
            {
                GameObject l_tank =
                    Instantiate(tank_, tank_spawnPoints_[i].transform.position, tank_spawnPoints_[i].transform.rotation);

                TankGoPointSet(l_tank, i);
            }

            isClear = true;
        }

        if (GameObject.FindGameObjectWithTag("GameTank"))
            cloneTank_ = GameObject.FindGameObjectWithTag("GameTank");
    }

    private void TankGoPointSet(GameObject l_tank, int l_i)
    {
        switch (l_i)
        {
            case 0:
                l_tank.GetComponent<Tank2>().Set_GPoint1(tank_goPoints_[0].transform.position, 2);
                l_tank.GetComponent<Tank2>().Set_GPoint2(tank_goPoints_[1].transform.position);
                break;
            case 1:
                l_tank.GetComponent<Tank2>().Set_GPoint1(tank_goPoints_[2].transform.position, 1);
                break;
            case 2:
                l_tank.GetComponent<Tank2>().Set_GPoint1(tank_goPoints_[3].transform.position, 2);
                l_tank.GetComponent<Tank2>().Set_GPoint2(tank_goPoints_[4].transform.position);
                break;
            case 3:
                l_tank.GetComponent<Tank2>().Set_GPoint1(tank_goPoints_[5].transform.position, 1);
                break;
            case 4:
                l_tank.GetComponent<Tank2>().Set_GPoint1(tank_goPoints_[6].transform.position, 1);
                break;
            case 5:
                l_tank.GetComponent<Tank2>().Set_GPoint1(tank_goPoints_[7].transform.position, 1);
                break;
        }
    }

    public bool Get_SpawnFlag()
    {
        return isSpawn;
    }

    public void Set_SpawnFlag(bool l_flag)
    {
        isSpawn = l_flag;
    }

    public bool Get_Clear()
    {
        return cloneTank_.GetComponent<Tank2>().Get_Clear();
    }
}
