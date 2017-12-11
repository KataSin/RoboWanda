using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliLineMissileManager : MonoBehaviour
{
    public struct LineState
    {
        public GameObject line;
        public GameObject landingPoint;
        public List<GameObject> missiles;
        public float randomY;
    }
    private List<LineState> m_LineObject;

    private List<GameObject> m_Missile;

    private HelicopterMissileManager m_MissileManager;

    private GameObject m_Robot;
    public bool m_DrawFlag;

    private bool m_GoFlag;
    
    private float m_Time;

    // Use this for initialization
    void Start()
    {
        m_Robot = GameObject.FindGameObjectWithTag("Robot");
        m_Missile = new List<GameObject>();
        m_GoFlag = false;
        var missiles = transform.parent.GetComponentsInChildren<Transform>();
        foreach(var i in missiles)
        {
            if (i.name.Substring(0,3) == "Mis")
            {
                m_Missile.Add(i.gameObject);
            }
        }

        m_LineObject = new List<LineState>();
        var lines = transform.GetComponentsInChildren<Transform>();
        int count = 0;
        foreach (var i in lines)
        {
            if (i.name == "LineMissile")
            {
                
                LineState state = new LineState();
                state.line = i.gameObject;
                state.randomY = Random.Range(20, 30);
                state.landingPoint = i.Find("HeliMissileLanding").gameObject;
                state.missiles = new List<GameObject>();
                string nameObj = "MissileRight";
                if (count == 0) nameObj = "MissileLeft";

                foreach (var missile in m_Missile)
                {
                    if (missile.name == nameObj)
                    {
                        state.missiles.Add(missile);
                    }
                }
                count++;
                m_LineObject.Add(state);
            }
        }
        var wae = transform.parent.Find("Heri").Find("MissileManager");
        m_MissileManager = transform.parent.Find("Heri").Find("MissileManager").GetComponent<HelicopterMissileManager>();


        m_Time = 0.0f;

        m_DrawFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var i in m_LineObject)
        {
            i.line.GetComponent<LineRenderer>().SetPosition(0, i.line.transform.position);
            Vector3 endPos = m_Robot.transform.position + new Vector3(0, i.randomY,0);
            i.line.GetComponent<LineRenderer>().SetPosition(1, endPos);
            i.landingPoint.SetActive(false);

            Ray ray = new Ray(transform.position, endPos - transform.position);
            RaycastHit hit;
            m_DrawFlag = false;
            int layer = ~(1 << 14);
            if (Physics.Raycast(ray,out hit, 100.0f,layer))
            {
                i.landingPoint.transform.Find("Particle System").GetComponent<ParticleSystem>().Play();
                i.landingPoint.SetActive(true);
                i.landingPoint.transform.position = hit.point+(hit.normal).normalized*1.5f;
                Quaternion rotate = Quaternion.LookRotation(hit.normal);
                i.line.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                i.landingPoint.transform.rotation = rotate*Quaternion.Euler(90,0,0);
                m_DrawFlag = true;
                m_GoFlag = true;
                
                
            }

        }

        //ベクトル更新
        foreach (var i in m_LineObject)
        {
            foreach (var missile in i.missiles)
            {
                Vector3 vec = (i.landingPoint.transform.position - missile.transform.position).normalized;
                missile.GetComponent<HeliMissileObj>().MissileVec(vec);
            }
        }

        //発射する場合
        if (m_GoFlag)
        {
            m_Time += Time.deltaTime;
            if (m_Time >= 3.0f)
            {

                m_MissileManager.Firing();


                if (m_MissileManager.GetIsEnd())
                {
                    foreach (var i in m_LineObject)
                    {
                        i.landingPoint.SetActive(false);
                    }
                    transform.parent.gameObject.GetComponent<HelicopterMissile>().m_ReturnFlag = true;
                    m_DrawFlag = false;

                    Destroy(gameObject.GetComponent<HeliLineMissileManager>());

                }

            }
        }

        if (m_DrawFlag)
        {
            SetEnable(true);
        }
        else
        {
            SetEnable(false);
        }
    }
    /// <summary>
    /// 表示するかどうか
    /// </summary>
    /// <param name="flag"></param>
    private void SetEnable(bool flag)
    {
        foreach (var i in m_LineObject)
        {
            i.line.GetComponent<LineRenderer>().enabled = flag;
        }
    }


    /// <summary>
    /// 線を表示するかどうか
    /// </summary>
    /// <param name="flag">true:表示 false:非表示</param>
    public void DrawLine(bool flag)
    {
        m_DrawFlag = flag;
    }
}
