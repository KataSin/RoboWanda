using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class testScript : MonoBehaviour
{

    List<GameObject> m_RobotMeshs;
    List<GameObject> m_DeadRobotMeshs;
    // Use this for initialization
    void Start()
    {
        var robot = GameObject.FindGameObjectWithTag("Robot");
        var robotMeshs = robot.transform.GetComponentsInChildren<Transform>();
        //ロボットの方のメッシュ取得
        m_RobotMeshs = GetRobotMesh(robotMeshs);
        //自身のメッシュ取得
        m_DeadRobotMeshs = GetRobotMesh(transform.GetComponentsInChildren<Transform>());

        m_RobotMeshs.Sort(delegate(GameObject left,GameObject right) { return string.Compare(left.name, right.name); });
        m_DeadRobotMeshs.Sort(delegate (GameObject left, GameObject right) { return string.Compare(left.name, right.name); });
        int a = 0;

    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach(var i in m_RobotMeshs)
        {
            if (i.name != m_DeadRobotMeshs[count].name)
            {
                int a = 0;
                continue;
            }
            m_DeadRobotMeshs[count].transform.position = i.GetComponent<SkinnedMeshRenderer>().rootBone.gameObject.transform.position+new Vector3(50,0,0);
            m_DeadRobotMeshs[count].transform.rotation = i.GetComponent<SkinnedMeshRenderer>().rootBone.gameObject.transform.rotation;

            var bones = i.GetComponent<SkinnedMeshRenderer>().bones;
            
            count++;
        }
    }

    public float Vector2Cross(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.z - rhs.x * lhs.z;
    }


    private List<GameObject> GetRobotMesh(Transform[] trans)
    {
        List<GameObject> meshs = new List<GameObject>();
        foreach(var i in trans)
        {
            string name = i.gameObject.name.Substring(0, 3);
            if (name == "Cyl" ||
                name == "Box" ||
                name == "Sph" ||
                name == "Pla" ||
                name == "Obj")
            {
                meshs.Add(i.gameObject);
            }
        }
        return meshs;
    }

    //private static int SortFunction(GameObject left, GameObject right)
    //{
    //    return left.
    //}
}
