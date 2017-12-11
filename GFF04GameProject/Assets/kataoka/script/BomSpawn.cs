using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomSpawn : MonoBehaviour
{
    [SerializeField, Tooltip("爆弾プレハブ")]
    public GameObject m_BomPrefab;
    [SerializeField, Tooltip("着地地点オブジェクト")]
    public GameObject m_LandingPoint;
    [SerializeField, Tooltip("挙動のテストプレハブ")]
    public GameObject m_Point;
    [SerializeField, Tooltip("投げる力")]
    public float m_Power = 100.0f;

    [SerializeField]
    private GameObject player_;

    //軌道線を表示させているかどうか
    private bool m_IsLineDraw;
    //投げるベクトル
    private Vector3 m_Vec;

    private List<GameObject> points;
    // Use this for initialization
    void Start()
    {
        //初期化
        points = new List<GameObject>();
        m_IsLineDraw = false;
        //軌道ポイント生成
        for (int time = 0; time <= 200; time++)
        {
            GameObject point = Instantiate(m_Point);
            points.Add(point);
            point.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //表示しないならアクティブをfalseにしてリターン
        if (!m_IsLineDraw)
        {
            foreach (var i in points)
            {
                i.SetActive(false);
            }
            m_LandingPoint.SetActive(false);
            return;
        }

        m_Vec = m_Vec * m_Power;

        //ポジション設定
        for (int time = 0; time <= 200; time++)
        {
            Vector3 pos = Force(transform.position, m_Vec, 0.1f, Physics.gravity, 1, time * 0.1f);
            points[time].transform.position = pos;
        }
        //線設定
        for (int i = 0; i <= 200; i++)
        {
            Vector3 start = points[i].transform.position;
            if (points.Count - 1 < i + 1) break;
            Vector3 end = points[i + 1].transform.position;

            Ray ray = new Ray(start, end - start);
            RaycastHit hit;
            int layer = ~(1 << 11 | 1 << 12 | 1 << 13);
            if (Physics.Raycast(ray, out hit, Vector3.Distance(start, end), layer))
            {
                //着地地点の座標と回転を設定
                m_LandingPoint.transform.position = hit.point;
                m_LandingPoint.transform.LookAt(m_LandingPoint.transform.position + hit.normal * 5.0f);
                m_LandingPoint.transform.rotation =
                    Quaternion.Euler(m_LandingPoint.transform.eulerAngles.x + 90,
                    m_LandingPoint.transform.eulerAngles.y,
                    m_LandingPoint.transform.eulerAngles.z);
                for (int j = i + 1; j <= 200; j++)
                {
                    points[i].SetActive(false);
                }
                break;
            }
            else
            {
                points[i].SetActive(true);
            }
            //着地地点表示するか
            m_LandingPoint.SetActive(true);
            if (i == 200)
            {
                m_LandingPoint.SetActive(false);
            }
        }

    }
    /// <summary>
    /// ボムを投げる
    /// </summary>
    public void SpawnBom()
    {
        GameObject bom = Instantiate(m_BomPrefab, transform.position, Quaternion.identity);
        bom.transform.rotation = Quaternion.Euler(90f, 0f, -(player_.transform.rotation.y * 180f / Mathf.PI) * 2f);
        bom.GetComponent<Rigidbody>().AddForce(m_Vec * m_Power);
    }
    /// <summary>
    /// ボムの投げるベクトルとパワーを設定する
    /// </summary>
    /// <param name="vec">ベクトル</param>
    /// <param name="power">パワー</param>
    public void Set(Vector3 vec, float power)
    {
        m_Vec = vec;
        m_Power = power;
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
}
