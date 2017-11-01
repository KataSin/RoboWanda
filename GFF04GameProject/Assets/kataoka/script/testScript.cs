using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{
    public GameObject bom;
    public GameObject tyakuti;
    public GameObject goal;
    private Vector3 vec;

    public GameObject point;

    private List<GameObject> points;
    // Use this for initialization
    void Start()
    {
        points = new List<GameObject>();
        for (int time = 0; time <= 200; time++)
        {
            points.Add(Instantiate(point));
        }
    }

    // Update is called once per frame
    void Update()
    {

        vec = transform.forward.normalized * 100.0f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = Instantiate(bom, transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(vec);
        }
        //ポジション設定
        for (int time = 0; time <= 200; time++)
        {
            Vector3 pos = Force(transform.position, vec, 0.1f, Physics.gravity, 1, time * 0.1f);
            points[time].transform.position = pos;
        }
        //線設定
        for (int time = 0; time <= 200; time++)
        {
            Vector3 start = points[time].transform.position;
            if (points.Count - 1 < time + 1) break;
            Vector3 end = points[time + 1].transform.position;

            Ray ray = new Ray(start, end - start);
            RaycastHit hit;
            int layer = ~(1 << 11 | 1 << 12);
            if (Physics.Raycast(ray, out hit, Vector3.Distance(start, end), layer))
            {
                tyakuti.transform.position = hit.point;
                for (int i = time + 1; i <= 200; i++)
                {
                    points[i].SetActive(false);
                }
                break;
            }
            else
            {
                points[time].SetActive(true);
            }

        }

    }

    public Vector3 Force(
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
