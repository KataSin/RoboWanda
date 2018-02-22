using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCntrlICO2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ico_4_0_;

    [SerializeField]
    private GameObject ico_4_1_;

    [SerializeField]
    private GameObject ico_5;

    [SerializeField]
    private GameObject manager_;

    [SerializeField]
    private GameObject player_;

    [SerializeField]
    private GameObject bombSpawn_;

    private bool isCheck;

    // Use this for initialization
    void Start()
    {
        ico_4_0_.GetComponent<Image>().enabled = false;
        ico_4_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        ico_5.SetActive(false);
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
                    ICO4_Active();
                else
                    ICO5_Active();
                break;

            case 6:

                if (bombSpawn_.GetComponent<BomSpawn>().GetMode() == 2)
                    ICO4_Active();
                else
                    ICO5_Active();
                break;
        }       
    }

    private void ICO4_Active()
    {
        switch (player_.GetComponent<PlayerController_Tutorial>().GetPlayerState())
        {
            case 1:
                ico_4_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                ico_4_0_.GetComponent<Image>().enabled = true;

                break;

            default:
                ico_4_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
                ico_4_0_.GetComponent<Image>().enabled = true;

                break;
        }

        ico_5.SetActive(false);
    }

    private void ICO5_Active()
    {
        ico_4_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        ico_4_0_.GetComponent<Image>().enabled = false;
        ico_5.SetActive(true);
    }
}
