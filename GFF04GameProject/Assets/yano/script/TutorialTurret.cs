using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTurret : MonoBehaviour
{
    private enum State
    {
        Boot,
        On,
        Lost,
        Off,
    }
    private State state_;

    private GameObject player_;
    private Vector3 m_lineToPlayer;

    [SerializeField]
    private GameObject muzzle_;

    private Quaternion m_originLRotate;
    private Quaternion m_prevLRotate;

    [SerializeField]
    private float m_originLrotateY;

    private bool isLeft;

    private float t;

    private int m_checkCnt;

    // Use this for initialization
    void Start()
    {
        state_ = State.Off;
        player_ = GameObject.FindGameObjectWithTag("Player");
        isLeft = false;
        t = 0f;
        m_checkCnt = 0;
        m_originLRotate = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();
    }

    private void StateUpdate()
    {
        switch (state_)
        {
            case State.Boot:
                BootUpdate();
                break;
            case State.On:
                TargetPlayer();
                TargetToLine();
                Ray();
                break;
            case State.Lost:
                TargetPlayerLost();
                TargetLoastLine();
                Ray();
                break;
            case State.Off:
                break;
        }

        Debug.Log(state_);
    }

    private void BootUpdate()
    {
        Vector3 l_vec = player_.transform.position - muzzle_.transform.position;
        transform.localRotation =
            Quaternion.Slerp(m_originLRotate, Quaternion.Euler(0.0f, 0.0f, Quaternion.LookRotation(l_vec).eulerAngles.y - 90f + m_originLrotateY), t / 1f);
        if (t >= 1f)
            state_ = State.On;
        t += 1.0f * Time.deltaTime;
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
                t = 0f;
                state_ = State.On;
            }
            else
            {
                muzzle_.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(54f / 255f, 106f / 255f, 1f));
                if (state_ == State.On)
                    m_prevLRotate = transform.localRotation;
                state_ = State.Lost;
            }
        }
        else
        {
            muzzle_.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(54f / 255f, 106f / 255f, 1f));
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f);
    }

    private void TargetPlayer()
    {
        Vector3 l_vec = player_.transform.position - muzzle_.transform.position;
        transform.localRotation =
            Quaternion.Euler(0.0f, 0.0f, Quaternion.LookRotation(l_vec).eulerAngles.y - 90f + m_originLrotateY);
    }

    private void TargetToLine()
    {
        muzzle_.GetComponent<LineRenderer>().SetPosition(0, muzzle_.transform.position);

        m_lineToPlayer = new Vector3(player_.transform.position.x, muzzle_.transform.position.y - 1f, player_.transform.position.z);
        muzzle_.GetComponent<LineRenderer>().SetPosition(1, m_lineToPlayer);
    }

    private void TargetPlayerLost()
    {
        if (!isLeft && m_checkCnt == 0)
            transform.localRotation =
                Quaternion.Slerp(m_prevLRotate, Quaternion.Euler(0f, 0f, 11f), t / 3f);

        else if (!isLeft && m_checkCnt == 1)
            transform.localRotation =
                Quaternion.Slerp(Quaternion.Euler(0f, 0f, -12f), Quaternion.Euler(0f, 0f, 11f), t / 3f);

        else if (isLeft)
            transform.localRotation =
                Quaternion.Slerp(Quaternion.Euler(0f, 0f, 11f), Quaternion.Euler(0f, 0f, -12f), t / 3f);

        t += 1.0f * Time.deltaTime;

        if (t >= 3f)
        {
            isLeft = !isLeft;
            t = 0f;
            m_checkCnt = 1;
        }
    }

    private void TargetLoastLine()
    {
        muzzle_.GetComponent<LineRenderer>().SetPosition(0, muzzle_.transform.position);

        m_lineToPlayer = new Vector3(player_.transform.position.x, muzzle_.transform.position.y - 1f, player_.transform.position.z);
        muzzle_.GetComponent<LineRenderer>().SetPosition(1, muzzle_.transform.position + -transform.up * 20f);
    }

    public int Get_State()
    {
        return (int)state_;
    }

    public void Boot()
    {
        state_ = State.Boot;
    }

    public void Off()
    {
        state_ = State.Off;
    }
}
