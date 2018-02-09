using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCntrlICO2 : MonoBehaviour
{
    [SerializeField]
    private GameObject cntrlICO_5_;

    [SerializeField]
    private GameObject manager_;

    [SerializeField]
    private GameObject bombSpawn_;

    private bool isCheck;

    // Use this for initialization
    void Start()
    {
        cntrlICO_5_.SetActive(false);
        isCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        ICO_Change();
    }

    public void ICO_Change()
    {
        switch (manager_.GetComponent<TutorialManager>().GetTutorialState())
        {
            case 5:
                if (bombSpawn_.GetComponent<BomSpawn>().GetMode() == 1)
                    ICO5_Active();
                else
                    ICO4_Active();
                break;

            case 6:
                if(!isCheck)
                {
                    GetComponent<Image>().enabled = true;
                    cntrlICO_5_.SetActive(false);
                    isCheck = true;
                }

                if (bombSpawn_.GetComponent<BomSpawn>().GetMode() == 2)
                    ICO5_Active();
                else
                    ICO4_Active();
                break;
        }
    }

    private void ICO4_Active()
    {
        GetComponent<Image>().enabled = true;
        cntrlICO_5_.SetActive(false);
    }

    private void ICO5_Active()
    {
        GetComponent<Image>().enabled = false;
        cntrlICO_5_.SetActive(true);
    }
}
