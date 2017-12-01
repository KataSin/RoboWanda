using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera_onj_;

    [SerializeField]
    private Camera mainCamera_;

    [SerializeField]
    private GameObject heri_;

    [SerializeField]
    private GameObject player_;

    private Vector3 m_originPos;
    private Quaternion m_originRotate;

    private Camera thisCamera_;

    private float t;

    [SerializeField]
    private GameObject black_canvus_;

    // Use this for initialization
    void Start()
    {
        m_originPos = transform.position;
        m_originRotate = transform.rotation;
        thisCamera_ = GetComponent<Camera>();

        mainCamera_.enabled = false;
        thisCamera_.enabled = true;

        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        black_canvus_.GetComponent<BlackOut_UI>().FeadIn();

        transform.LookAt(heri_.transform);

        if (player_.GetComponent<PlayerController>().GetPlayerState() != 0)
        {
            transform.position = Vector3.Lerp(m_originPos, mainCamera_.transform.position, t / 1f);
            transform.rotation = Quaternion.Lerp(m_originRotate, mainCamera_.transform.rotation, t / 1f);

            t += 1.0f * Time.deltaTime;

            if (t >= 1f)
            {
                mainCamera_.enabled = true;
                Destroy(this.gameObject);
            }
        }
    }
}
