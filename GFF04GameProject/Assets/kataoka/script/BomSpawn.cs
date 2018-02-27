using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomSpawn : MonoBehaviour
{
    public enum Bom
    {
        BOM,
        SMOKE_BOM,
        LIGHT_BOM

    }
    public List<Bom> m_UseBom;

    [SerializeField, Tooltip("爆弾プレハブ")]
    public GameObject m_BomPrefab;
    [SerializeField, Tooltip("閃光弾プレハブ")]
    public GameObject m_LightBomPrefab;

    public GameObject m_SmokeBomPrefab;

    [SerializeField, Tooltip("着地地点オブジェクト"), Space(15)]
    public GameObject m_LandingPoint;
    [SerializeField, Tooltip("頂点オブジェクト")]
    public GameObject m_VertexPoint;
    [SerializeField, Tooltip("挙動のテストプレハブ")]
    public GameObject m_Point;
    [SerializeField, Tooltip("通常投げる力")]
    public float m_Power = 100.0f;
    [SerializeField, Tooltip("強い投げる力")]
    public float m_HighPower = 200.0f;

    [SerializeField, Tooltip("PV用にラインを消すかどうか")]
    public bool m_PvLineRendererNoDraw;

    private GameObject m_Player;
    [SerializeField]
    private GameObject player_;

    //頂点の座標
    private Vector3 m_VertexPos;

    //軌道線を表示させているかどうか
    private bool m_IsLineDraw;
    //投げるベクトル
    private Vector3 m_Vec;
    //投げるボムの種類
    public Bom m_Bom;
    private int m_IndexBom;
    //ラインレンダラー
    private LineRenderer m_LineRenderer;
    private List<GameObject> points;

    private bool isTrigger;

    // Use this for initialization
    void Start()
    {
        //初期化
        points = new List<GameObject>();

        m_LineRenderer = GetComponent<LineRenderer>();

        m_LineRenderer.positionCount = 0;
        m_LineRenderer.enabled = false;

        m_IsLineDraw = false;
        //軌道ポイント生成
        for (int time = 0; time <= 80; time++)
        {
            GameObject point = Instantiate(m_Point);
            points.Add(point);
            point.SetActive(false);
        }

        isTrigger = false;
        m_UseBom = new List<Bom>();
        m_UseBom.Add(Bom.BOM);
        m_UseBom.Add(Bom.LIGHT_BOM);
        m_UseBom.Add(Bom.SMOKE_BOM);
        m_IndexBom = 0;

        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_Bom);

        if (GameObject.FindGameObjectWithTag("TutorialObj"))
        {
            if(m_Player.GetComponent<PlayerController_Tutorial>().GetPlayerState() != (int)T_PlayerState.Aiming)
                SetDrawLine(false);
        }

        else if(!GameObject.FindGameObjectWithTag("TutorialObj"))
        {
            if (m_Player.GetComponent<PlayerController>().GetPlayerState() != (int)PlayerState.Aiming)
                SetDrawLine(false);
        }

        //切り替えボタン

        if (Input.GetAxisRaw("Bullet_Select") == 0.0f)
            isTrigger = false;

        // 左ボタン
        if (Input.GetAxisRaw("Bullet_Select") < -0.9
            && !isTrigger)
        {
            m_IndexBom++;
            isTrigger = true;
        }
        // 右ボタン
        if (Input.GetAxisRaw("Bullet_Select") > 0.9
            && !isTrigger)
        {
            m_IndexBom--;
            isTrigger = true;
        }

        if ((int)m_IndexBom < 0) m_IndexBom = m_UseBom.Count - 1;
        if ((int)m_IndexBom > m_UseBom.Count - 1) m_IndexBom = 0;

        m_Bom = m_UseBom[m_IndexBom];

        //表示しないならアクティブをfalseにしてリターン
        if (!m_IsLineDraw)
        {
            foreach (var i in points)
            {
                i.SetActive(false);
            }
            //m_LineRenderer.enabled = false;
            m_LandingPoint.SetActive(false);
            return;
        }
        //m_LineRenderer.enabled = true;
        float power = m_Power;
        if (m_Bom == Bom.LIGHT_BOM)
            power = m_HighPower;
        m_Vec = m_Vec * power;
        //ポジション設定
        for (int time = 0; time <= 80; time++)
        {
            Vector3 pos = Force(transform.position, m_Vec, 0.1f, Physics.gravity, 1, time * 0.05f);
            points[time].transform.position = pos;
        }
        int colNum = 0;
        //反映するポイント
        List<Vector3> linePoints = new List<Vector3>();
        m_VertexPos = Vector3.zero;
        //線設定
        for (int i = 0; i <= points.Count - 1; i++)
        {
            Vector3 start = points[i].transform.position;
            points[i].SetActive(true);

            //オブジェクトの回転
            if (i + 1 < points.Count)
                points[i].transform.LookAt(points[i + 1].transform.position);


            if (points.Count - 1 < i + 1) break;
            Vector3 end = points[i + 1].transform.position;

            Ray ray = new Ray(start, end - start);
            RaycastHit hit;
            int layer = 1 << 8 | 1 << 9 | 1 << 17;
            if (Physics.Raycast(ray, out hit, Vector3.Distance(start, end), layer))
            {
                //着地地点の座標と回転を設定
                m_LandingPoint.transform.position = hit.point + (hit.normal.normalized * 0.1f);
                m_LandingPoint.transform.LookAt(m_LandingPoint.transform.position + hit.normal * 5.0f);
                m_LandingPoint.transform.rotation =
                    Quaternion.Euler(m_LandingPoint.transform.eulerAngles.x + 90,
                    m_LandingPoint.transform.eulerAngles.y,
                    m_LandingPoint.transform.eulerAngles.z);

                m_LandingPoint.SetActive(true);
                for (int j = i + 1; j <= points.Count - 1; j++)
                {
                    points[j].SetActive(false);
                }
                break;
            }
            else
            {
                linePoints.Add(points[i].transform.position);

                //if (m_VertexPos.y <= points[i].transform.position.y)
                //{
                //    m_VertexPos = points[i].transform.position;
                //}

            }
            //着地地点表示するか

            if (i == 80)
            {
                m_LandingPoint.SetActive(false);
            }
            colNum++;
        }
        if (linePoints.Count >= 2)
            m_VertexPos = linePoints[linePoints.Count / 2];
        //一個目は消す
        points[0].SetActive(false);
        //m_LineRenderer.positionCount = linePoints.Count;

        //if (m_Bom == BomSpawn.Bom.LIGHT_BOM)
        //{
        //    m_LineRenderer.positionCount = colNum / 2;

        //}
        //for (int i = 0; i < m_LineRenderer.positionCount; i++)
        //{
        //    m_LineRenderer.SetPosition(i, linePoints[i]);
        //}

        ////消す
        //if (m_PvLineRendererNoDraw) m_LineRenderer.enabled = false;

    }
    //使用できるボムを追加する
    public void AddBom(Bom bom)
    {
        m_UseBom.Add(bom);
    }

    /// <summary>
    /// ボムを投げる
    /// </summary>
    public void SpawnBom()
    {
        GameObject prefab = m_BomPrefab;
        switch (m_Bom)
        {
            case Bom.BOM:
                {
                    prefab = m_BomPrefab;
                    break;
                }
            case Bom.LIGHT_BOM:
                {
                    prefab = m_LightBomPrefab;
                    break;
                }
            case Bom.SMOKE_BOM:
                {
                    prefab = m_SmokeBomPrefab;
                    break;
                }

        }



        GameObject bom = Instantiate(prefab, transform.position, Quaternion.identity);
        if (m_VertexPos.y <= transform.position.y) m_VertexPos = Vector3.zero;
        if (m_Bom == Bom.LIGHT_BOM) bom.GetComponent<LightBullet>().SetVertex(m_VertexPos);
        else if (m_Bom == Bom.BOM) bom.GetComponent<Bomb_v2>().m_Bullet = Bom.BOM;


        bom.transform.rotation = Quaternion.LookRotation(m_Vec) * Quaternion.Euler(90, 0, 0);

        if (m_Bom == Bom.LIGHT_BOM)
            bom.transform.rotation = Quaternion.LookRotation(m_Vec);
        float power = m_Power;
        if (m_Bom == Bom.LIGHT_BOM)
            power = m_HighPower;
        m_Vec = m_Vec * power;



        bom.GetComponent<Rigidbody>().AddForce(m_Vec);
    }




    /// <summary>
    /// ボムの投げるベクトルとパワーを設定する
    /// </summary>
    /// <param name="vec">ベクトル</param>
    /// <param name="power">パワー</param>
    public void Set(Vector3 vec, float power, Bom bom = Bom.BOM)
    {
        m_Vec = Camera.main.transform.forward;
        m_Power = power;
        //m_Bom = Bom.BOM;
    }
    /// <summary>
    /// 軌道線を表示するかどうか
    /// </summary>
    /// <param name="flag">true:する　false:しない</param>
    public void SetDrawLine(bool flag)
    {
        m_IsLineDraw = flag;
    }
    /// <summary>
    /// 時間によって挙動を求める
    /// </summary>
    /// <param name="start">開始地点</param>
    /// <param name="force">力</param>
    /// <param name="mass">重さ</param>
    /// <param name="gravity">重力加速度</param>
    /// <param name="gravityScale">重力の大きさ</param>
    /// <param name="time">時間</param>
    /// <returns>いる座標</returns>
    private Vector3 Force(
    Vector3 start,
    Vector3 force,
    float mass,
    Vector3 gravity,
    float gravityScale,
    float time
)
    {
        var speedX = force.x / mass * Time.fixedDeltaTime;
        var speedY = force.y / mass * Time.fixedDeltaTime;
        var speedZ = force.z / mass * Time.fixedDeltaTime;

        var halfGravityX = gravity.x * 0.5f * gravityScale;
        var halfGravityY = gravity.y * 0.5f * gravityScale;
        var halfGravityZ = gravity.z * 0.5f * gravityScale;

        var positionX = speedX * time + halfGravityX * Mathf.Pow(time, 2);
        var positionY = speedY * time + halfGravityY * Mathf.Pow(time, 2);
        var positionZ = speedZ * time + halfGravityZ * Mathf.Pow(time, 2);

        return start + new Vector3(positionX, positionY, positionZ);
    }
    public float Vector2Cross(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.y - rhs.x * lhs.y;
    }
    //使えるボムを取得
    public List<Bom> GetUseBom()
    {
        return m_UseBom;
    }

    // Ho追加（2017/12/27）
    // 今選択中の弾種を返す
    public int GetMode()
    {
        return (int)m_Bom;
    }
}
