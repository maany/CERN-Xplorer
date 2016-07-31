
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbitCam: MonoBehaviour
{
    public Transform target;
    public double distance = 10f;
    public int cameraSpeed = 5;
    public double xSpeed = 175;
    public double ySpeed = 175;
    public double minDistance = 3;
    public double maxDistance = 20;
    private double x = 0.0;
    private double y = 0.0;
    public float yMinLimit = 20;
    public float yMaxLimit = 80;
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
    
    void LateUpdate()
    {
        //Zooming with mouse
        distance += Input.GetAxis("Mouse ScrollWheel") * distance;
        distance = Mathf.Clamp((float)distance, (float)minDistance, (float)maxDistance);

        //Detect mouse drag;
        if (Input.GetMouseButton(0))
        {
            
            x += (Input.GetAxis("Mouse X") * xSpeed * 0.02);
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
        }
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler((float)y,(float) x, 0);
        var position = rotation * (new Vector3(0, 0, (float)-distance)) + target.position;

        transform.position = Vector3.Lerp(transform.position, position, cameraSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    private double ClampAngle(double angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp((float)angle, min, max);
    }
}

