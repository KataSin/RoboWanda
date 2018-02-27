using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTank : MonoBehaviour
{
    [SerializeField]
    private GameObject bill_;

    [SerializeField]
    private GameObject gunY_;
    private Quaternion m_gunYorigin_rotation;

    [SerializeField]
    private GameObject gunX_;

    [SerializeField]
    private GameObject bullet_;

    [SerializeField]
    private GameObject fire_effect_;

    private float m_interValTime;

    private float t0, t1;

    private bool isPlay1;
    private bool isPlay2;

    // Use this for initialization
    void Start()
    {
        m_gunYorigin_rotation = gunY_.transform.rotation;
        t0 = 0f;
        t1 = 0f;
        m_interValTime = 2.5f;
        isPlay1 = false;
        isPlay2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        GunToTarget();
        TankGunAttack();
    }

    private void GunToTarget()
    {
        if (bill_.GetComponent<LightIntersectCheck>().Get_AttackFlag())
        {
            Vector3 l_vec = bill_.transform.position - gunY_.transform.position;
            gunY_.transform.rotation =
                Quaternion.Slerp(m_gunYorigin_rotation, Quaternion.Euler(0f, Quaternion.LookRotation(l_vec).eulerAngles.y, 0f), t0 / 2f);

            if (t0 >= 2f)
            {
                gunX_.transform.localRotation =
                    Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(-11f, 0f, 0f), t1 / 2f);

                t1 += 1.0f * Time.deltaTime;

                if (!isPlay2)
                {
                    GetComponents<AudioSource>()[1].PlayOneShot(GetComponents<AudioSource>()[1].clip);
                    isPlay2 = true;
                }
            }

            t0 += 1.0f * Time.deltaTime;

            if (!isPlay1)
            {
                GetComponents<AudioSource>()[1].PlayOneShot(GetComponents<AudioSource>()[1].clip);
                isPlay1 = true;
            }
        }

        t0 = Mathf.Clamp(t0, 0f, 2f);
        t1 = Mathf.Clamp(t1, 0f, 2f);
    }

    private void TankGunAttack()
    {
        if (t1 >= 2f && !bill_.GetComponent<Break_v2Tutorial>().Get_BreakFlag())
        {
            if (m_interValTime <= 0f)
            {
                GameObject l_gun = Instantiate(bullet_, gunX_.transform.position, Quaternion.identity);
                Instantiate(fire_effect_, gunX_.transform.position + gunX_.transform.forward * 9f, Quaternion.identity);
                l_gun.transform.rotation = gunX_.transform.rotation;

                GetComponents<AudioSource>()[0].PlayOneShot(GetComponents<AudioSource>()[0].clip);

                m_interValTime = 2.5f;
            }
            m_interValTime -= 1.0f * Time.deltaTime;
        }
    }
}
