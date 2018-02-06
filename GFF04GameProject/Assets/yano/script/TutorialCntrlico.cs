using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCntrlico : MonoBehaviour
{
    [SerializeField]
    private GameObject ico_3_2_;

    [SerializeField]
    private GameObject player_;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        ICO_Change();
    }

    public void ICO_Change()
    {
        switch(player_.GetComponent<PlayerController_Tutorial>().GetPlayerState())
        {
            case 1:
                ico_3_2_.SetActive(false);
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            break;

            default:
                if (GameObject.FindGameObjectWithTag("Bomb"))
            {
                ico_3_2_.SetActive(true);
                GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                ico_3_2_.SetActive(false);
                GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
            }

            break;
        }
    }
}
