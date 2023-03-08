using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform newPoint=null;
    private Transform startPoint = null;
    private int count=0;
    private GameObject[] points;

    public GameObject Point;
    public int numberCells=0;
    public AudioSource AudioConnection;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount=0;
        points = new GameObject[numberCells];
        Point.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetNewPoint(Transform new_point)
    {
        if (count < numberCells)
        {
            if (startPoint == null)
            { //START POINT
                startPoint = new_point;
                newPoint = new_point;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(count, new Vector3(startPoint.position.x, startPoint.position.y + 1.2f, startPoint.position.z)); //create line
                points[count] = Instantiate(Point, new Vector3(startPoint.position.x, startPoint.position.y + 1.2f, startPoint.position.z), Quaternion.identity); //create point
                points[count].SetActive(true);
                count++;
            }
            else
            { //ADD NEW POINT
                newPoint = new_point;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(count, new Vector3(newPoint.position.x, newPoint.position.y + 1.2f, newPoint.position.z)); //create line
                points[count] = Instantiate(Point, new Vector3(newPoint.position.x, newPoint.position.y + 1.2f, newPoint.position.z), Quaternion.identity); //create point
                points[count].SetActive(true);
                count++;
            }

            AudioConnection.Play();

            if (count >= numberCells)
                ResetLine();
        }
    }

    public void ResetLine()
    {
        for (int i = 0; i < count; i++) //DESTROY POINTS
            Destroy(points[i]);

        count = 0;
        startPoint = null;
        newPoint = null;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        lineRenderer.positionCount = 0;
    }
}
