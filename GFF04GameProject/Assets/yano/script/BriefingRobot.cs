using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingRobot : MonoBehaviour
{
    private enum State
    {
        Idle,
        Beam,
        Missile,
        Dead,
        BeamBombing,
    }

    [SerializeField]
    private State state_;

    private Quaternion m_origin_rotation;

    private Animator animator_;

    [SerializeField]
    private GameObject beam_;

    [SerializeField]
    private GameObject look_pos_;
    private Vector3 m_lookoriginPos_;

    [SerializeField]
    private GameObject missile_;
    private bool isMissile;

    private bool isBeam;
    private bool isBeamBombing;

    private float t;

    private bool isFinishBeam;
    private bool isFinishMissile;

    private bool isDead;

    [SerializeField]
    private int m_hp;

    // Use this for initialization
    void Start()
    {
        animator_ = GetComponent<Animator>();
        state_ = State.Idle;
        isBeam = false;
        isBeamBombing = false;
        isFinishBeam = false;
        isMissile = false;
        isFinishMissile = false;
        t = 0f;
        m_lookoriginPos_ = look_pos_.transform.position;
        isDead = false;
        m_hp = 3;
        m_origin_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hp <= 0)
            state_ = State.Dead;

        switch (state_)
        {
            case State.Idle:
                break;
            case State.Beam:
                BeamUpdate();
                break;
            case State.Missile:
                MissileUpdate();
                break;
            case State.Dead:
                DeadUpdate();
                break;
            case State.BeamBombing:
                BeamBombingUpdate();
                break;
        }

        animator_.SetBool("IsDead", isDead);
    }

    void OnAnimatorIK()
    {
        if (state_ == State.Beam
            || state_ == State.BeamBombing)
        {
            animator_.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0.0f);
            animator_.SetLookAtPosition(look_pos_.transform.position);
        }
    }

    private void BeamUpdate()
    {
        look_pos_.transform.position =
            Vector3.Lerp(m_lookoriginPos_, new Vector3(m_lookoriginPos_.x, 50f, m_lookoriginPos_.z), t / 3f);

        if (t >= 3f)
        {
            if (!isFinishBeam)
            {
                beam_.GetComponent<RobotBeam>().SetBeamFlag(false);
                isFinishBeam = true;
            }
            if (t >= 6f)
            {
                t = 0f;
                state_ = State.Missile;
            }
        }
        t += 1.0f * Time.deltaTime;
    }

    public void Beam()
    {
        if (!isBeam)
        {
            beam_.SetActive(true);
            isBeam = true;
            state_ = State.Beam;
        }
    }

    private void BeamBombingUpdate()
    {
        transform.rotation =
            Quaternion.Slerp(m_origin_rotation, Quaternion.Euler(m_origin_rotation.x, 220f, m_origin_rotation.z), t / 1f);

        if (t >= 1.5f)
        {
            if (!isFinishBeam)
            {
                beam_.GetComponent<RobotBeam>().SetBeamFlag(false);
                isFinishBeam = true;
            }
            if (t >= 6f)
            {
                t = 0f;
                state_ = State.Idle;
            }
        }
        t += 1.0f * Time.deltaTime;
    }

    public void BeamBombing()
    {
        if (!isBeamBombing)
        {
            beam_.SetActive(true);
            isFinishBeam = false;
            isBeamBombing = true;
            look_pos_.transform.position = 
                GameObject.FindGameObjectWithTag("Bomber").transform.position;
            state_ = State.BeamBombing;
            t = 0f;
        }
    }

    private void MissileUpdate()
    {
        if (!isMissile)
        {
            missile_.GetComponent<MissileSpawn>().SpawnFlag(true);
            isMissile = true;
            state_ = State.Missile;
        }

        if (t >= 6f)
        {
            isFinishMissile = true;
            t = 0f;
        }

        t += 1.0f * Time.deltaTime;
    }

    private void DeadUpdate()
    {
        isDead = true;
    }

    public bool Get_MissileFinishFlag()
    {
        return isFinishMissile;
    }

    public int GetState()
    {
        return (int)state_;
    }

    public void Damage()
    {
        m_hp--;
    }
}
