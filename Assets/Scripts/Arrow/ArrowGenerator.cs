using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ArrowGenerator : MonoBehaviour
{
    public float stemLenght;
    public float stemWidth;
    public float tipLength;
    public float tipWidth;

    List<Vector3> verticesList;
    List<int> trianglesList;
    Mesh mesh;

    Renderer arrowRenderer;
    Camera cam;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        cam = Camera.main;
        arrowRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {

        ArrowWithPointer();
        
    }
    void GenerateArrow()
    {
        verticesList = new List<Vector3>();
        trianglesList = new List<int>();

        Vector3 stemOrigin = Vector3.zero;
        float stemHalfWidth = stemWidth / 2f;

        verticesList.Add(stemOrigin + stemHalfWidth * Vector3.down);
        verticesList.Add(stemOrigin + stemHalfWidth * Vector3.up);
        verticesList.Add(verticesList[0] + stemLenght * Vector3.right);
        verticesList.Add(verticesList[1] + stemLenght * Vector3.right);

        trianglesList.Add(0);
        trianglesList.Add(1);
        trianglesList.Add(3);

        trianglesList.Add(0);
        trianglesList.Add(3);
        trianglesList.Add(2);


        Vector3 tipOrigin = stemLenght * Vector3.right;
        float tipHalfWidth = tipWidth / 2f;

        verticesList.Add(tipOrigin + tipHalfWidth * Vector3.up);
        verticesList.Add(tipOrigin + tipHalfWidth * Vector3.down);
        verticesList.Add(tipOrigin + tipLength * Vector3.right);


        trianglesList.Add(4);
        trianglesList.Add(6);
        trianglesList.Add(5);

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = trianglesList.ToArray();
    }

    void ArrowWithPointer()
    {
        if (PlayerManager.playerManagerInstance.drag)
        {
            GenerateArrow();

            var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            var offSet = PlayerManager.playerManagerInstance.transform.position - transform.position;

            stemLenght = offSet.magnitude;
            arrowRenderer.enabled = true;
        }
        else
        {
            stemLenght = 0;
            arrowRenderer.enabled = false;
        }
    }
}
