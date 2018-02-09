using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCntrlico : MonoBehaviour
{
    [SerializeField]
    private GameObject ico_3_0_;

    [SerializeField]
    private GameObject ico_3_1_;

    [SerializeField]
    private GameObject ico_3_2_;

    [SerializeField]
    private GameObject player_;

    // Use this for initialization
    void Start()
    {
        ico_3_0_.GetComponent<Image>().enabled = false;
        ico_3_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
        ico_3_2_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ICO_Change()
    {
        switch(player_.GetComponent<PlayerController_Tutorial>().GetPlayerState())
        {
            case 1:
                if (GameObject.FindGameObjectWithTag("Bomb"))
                {
                    ico_3_2_.SetActive(true);
                    ico_3_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                    ico_3_0_.GetComponent<Image>().enabled = false;
                }
                else
                {
                    ico_3_2_.SetActive(false);
                    ico_3_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    ico_3_0_.GetComponent<Image>().enabled = true;
                }
                break;

            default:
                if (GameObject.FindGameObjectWithTag("Bomb"))
                {
                    ico_3_2_.SetActive(true);
                    ico_3_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                    ico_3_0_.GetComponent<Image>().enabled = false;
                }
                else
                {
                    ico_3_2_.SetActive(false);
                    ico_3_1_.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
                    ico_3_0_.GetComponent<Image>().enabled = true;
                }

                break;
        }
    }
}
