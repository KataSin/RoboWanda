using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player_;

    [SerializeField]
    private Camera mainCamera_;

    private Camera deadCamera_;

    private float m_intervalTimer;

    private Vector3 m_originPos;

    private float t;

    private Quaternion m_lerp_rotation;

    private Quaternion m_origin_rotation;

    [SerializeField]
    private GameObject black_curtain_;

    [SerializeField]
    private GameObject go_uis_;

    // Use this for initialization
    void Start()
    {
        deadCamera_ = GetComponent<Camera>();

        m_intervalTimer = 0f;
        deadCamera_.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (player_.GetComponent<PlayerController1>().GetPlayerState() != 4)
        {
            m_originPos = transform.position;
            m_lerp_rotation = transform.rotation;
            m_origin_rotation = transform.rotation;
        }

        else if (player_.GetComponent<PlayerController1>().GetPlayerState() == 4)
        {
            m_intervalTimer += 1.0f * Time.deltaTime;
            if (t < 3f)
                transform.LookAt(player_.transform.position + player_.transform.forward);
            mainCamera_.enabled = false;
            deadCamera_.enabled = true;

            if (m_intervalTimer >= 2f)
            {
                t += 1.0f * Time.deltaTime;
                transform.position = Vector3.Lerp(m_originPos, player_.transform.position+ player_.transform.forward + new Vector3(0f, 3f, 0f), t / 3f);

                if (t >= 3f)
                {
                    black_curtain_.GetComponent<BlackOut_UI>().GameOverFead();
                    if (black_curtain_.GetComponent<BlackOut_UI>().Get_ClearGO())
                        go_uis_.SetActive(true);
                }
            }
        }
    }
}
