using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTurret : MonoBehaviour
{
    private GameObject player_;
    private Vector3 m_lineToPlayer;

    [SerializeField]
    private GameObject muzzle_;

    [SerializeField]
    private float m_originLrotate;

    // Use this for initialization
    void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Ray();
        TargetPlayer();
        TargetToLine();
    }

    private void Ray()
    {
        Ray ray = new Ray(transform.position + new Vector3(0f, 0f, -0.55f), -transform.up);

        float distance = Vector3.Distance(muzzle_.transform.position, player_.transform.position);
        RaycastHit hit;
        int layer = (1 << 13 | 1 << 15);
        if (Physics.SphereCast(ray, 3f, out hit, distance, layer))
        {
            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Player")
            {
                muzzle_.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(1f, 54f / 255f, 54f / 255f));
            }
            else
            {
                muzzle_.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(54f / 255f, 106f / 255f, 1f));
            }
        }
        else
        {
            muzzle_.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(54f / 255f, 106f / 255f, 1f));
        }
    }

    private void TargetPlayer()
    {
        Vector3 l_vec = player_.transform.position - muzzle_.transform.position;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Quaternion.LookRotation(l_vec).eulerAngles.y - 90f + m_originLrotate);
    }

    private void TargetToLine()
    {
        muzzle_.GetComponent<LineRenderer>().SetPosition(0, muzzle_.transform.position);

        m_lineToPlayer = new Vector3(player_.transform.position.x, muzzle_.transform.position.y - 1f, player_.transform.position.z);
        muzzle_.GetComponent<LineRenderer>().SetPosition(1, m_lineToPlayer);
    }
}
