using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class polygons : MonoBehaviour {

    int vertices;
    int Polygons;

    [SerializeField]
    int minFps = 60;

    int frameCount = 0;
    float nextTime = 0.0f;

    void Start()
    {
        nextTime = Time.time + 1;
		
	}
	
	// Update is called once per frame
	void Update () {
        frameCount++;

        // 1秒ごとにFPS検証
        if (Time.time >= nextTime)
        {
            // Debug.LogFormat ("{0}fps", frameCount);
            if (frameCount < minFps) PolygonCount(frameCount);

            frameCount = 0;
            nextTime += 1f;
        }
    }

    [ContextMenu("CountStart")]
    void PolygonCount(int fps = -1)
    {

        vertices = 0;
        Polygons = 0;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {

            if (obj.activeInHierarchy)
            {

                SkinnedMeshRenderer skin = obj.GetComponent<SkinnedMeshRenderer>();

                if (skin != null)
                {
                    int vert = skin.sharedMesh.vertices.Length;
                    vertices += vert;

                    int polygon = skin.sharedMesh.triangles.Length / 3;
                    Polygons += polygon;
                }

                MeshFilter mesh = obj.GetComponent<MeshFilter>();

                if (mesh != null)
                {
                    int vert = mesh.sharedMesh.vertices.Length;
                    vertices += vert;

                    int polygon = mesh.sharedMesh.triangles.Length / 3;
                    Polygons += polygon;
                }

            }
        }
        Debug.LogFormat("Vertices(verts) : {0} , Polygons(Tris) : {1} , FPS : {2} ",
            vertices, Polygons, fps);

    }

    [CustomEditor(typeof(polygons))]
    public class CountStartEditor : Editor
    {

        polygons polygonCounter;

        void OnEnable()
        {
            polygonCounter = target as polygons;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("カウント開始"))
            {
                polygonCounter.PolygonCount();
            }
        }

    }
}
