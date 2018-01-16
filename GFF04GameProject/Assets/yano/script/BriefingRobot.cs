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
    }

    private State state_;

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

    private float t;

    private bool isFinishBeam;
    private bool isFinishMissile;

    private bool isDead;

    // Use this for initialization
    void Start()
    {
        animator_ = GetComponent<Animator>();
        state_ = State.Idle;
        isBeam = false;
        isFinishBeam = false;
        isMissile = false;
        isFinishMissile = false;
        t = 0f;
        m_lookoriginPos_ = look_pos_.transform.position;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                isDead = true;
                break;
        }

        animator_.SetBool("IsDead", isDead);
    }

    void OnAnimatorIK()
    {
        if (state_ == State.Beam)
        {
            animator_.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0.0f);
            animator_.SetLookAtPosition(look_pos_.transform.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TankGun>() != null)
            state_ = State.Dead;
    }

    private void BeamUpdate()
    {
        look_pos_.transform.position =
            Vector3.Lerp(m_lookoriginPos_, new Vector3(m_lookoriginPos_.x, 50f, m_lookoriginPos_.z), t / 3f);
        if (t >= 3f)
        {
            if (!isFinishBeam)
            {
                beam_.GetComponent<BeamParam>().bEnd = true;
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

    private void MissileUpdate()
    {
        if (!isMissile)
        {
            missile_.GetComponent<MissileSpawn>().SpawnFlag(true);
            isMissile = true;
            state_ = State.Missile;
        }

        if (t >= 6f)
            isFinishMissile = true;

        t += 1.0f * Time.deltaTime;
    }

    public void Missile()
    {
        if (!isMissile)
        {
            missile_.GetComponent<MissileSpawn>().SpawnFlag(true);
            isMissile = true;
            state_ = State.Missile;
        }
    }

    public bool Get_MissileFinishFlag()
    {
        return isFinishMissile;
    }

    public int GetState()
    {
        return (int)state_;
    }
}
