using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour
{

    public bool isActive = true;
    private bool isBGWhite = false;

    //Basic Variables
    public Transform target;
    private float distance = 350.0f;
    private float xSpeed = 5.0f;
    private float ySpeed = 5.0f;

    private float yMinLimit = -20f;
    private float yMaxLimit = 180f;

    private float distanceMin = 0.03f;
    private float distanceMax = 1550f;
    public bool isMoving = false;
    public bool wasMoving = false;
    public bool startedMoving = false;
    public bool stoppedMoving = false;

    public bool isOrbiting = false;
    private float orbitSpeed = 10f;



    //Smooth Camera and GoTo
    private float x = 0.0f;
    private float y = 0.0f;
    private float xSmooth = 0.0f;
    private float ySmooth = 0.0f;
    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;
    private float dVelocity = 0.0f;
    private float targetVelocity = 0.0f;
    private float targetPosZSmooth = 0.0f;
    private float targetPosZ = 0.0f;
    private float smoothTime = 0.15f;
    private float smoothGoToTime = 1f;
    private float smoothDistance = 0f;
    private Vector3 goToVelocity = Vector3.zero;
    private Vector3 goToPos = Vector3.zero;
    private float goToTime = 5f;
    private bool isOnGoTo = false;
    private float gotoThreshold = 12f;


    //Previous Camera Values
    private float previousxSmooth = 0.0f;
    private float previousySmooth = 0.0f;
    private float previoussmoothDistance = 0f;
    private float previoustargetPosZSmooth = 0.0f;
    private Vector3 previousCameraPosition = Vector3.zero;

    //Perspective Variables
    private Matrix4x4 ortho, perspective;
    private float fov = 60f;
    private float near = 0.3f;
    private float far = 1000f;
    private float orthographicSize = 80f;
    private float aspect;
    private bool  orthoOn;
    private float matrixTransitionTime = 0f;


    void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        goToTime = smoothTime;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        //Perspective matrix
        UpdateMatrix();
        orthoOn = false;
        GetComponent<Camera>().projectionMatrix = perspective;
        GetComponent<Camera>().farClipPlane = 10000.0f;
    }

    public void SetActive(bool act)
    {
        isActive = act;
    }

    void UpdateMatrix()
    {
        aspect = (float) Screen.width / (float) Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
    }

    void LateUpdate ()
    {
        if (target && isActive )
        {
            isMoving = false;
            if (!isOnGoTo)
            {


                if (Input.GetMouseButton (0))
                {
                    x += Input.GetAxis("Mouse X") * xSpeed * 0.5f;
                    y -= Input.GetAxis("Mouse Y") * ySpeed *  0.3f;

                    y = ClampAngle(y, yMinLimit, yMaxLimit);

                }

                if (Input.GetKey ("up") || Input.GetKey ("w"))
                {
                    if (orthoOn)
                    {
                        orthographicSize--;
                        orthographicSize = Mathf.Clamp(orthographicSize, 0.2f, 350f);
                        UpdateMatrix();
                        GetComponent<Camera>().projectionMatrix = ortho;
                    }
                    else
                    {
                        distance -= 0.01f * distance;
                    }
                }

                if (Input.GetKey ("down") || Input.GetKey ("s"))
                {
                    if (orthoOn)
                    {
                        orthographicSize++;
                        orthographicSize = Mathf.Clamp(orthographicSize, 0.2f, 350f);
                        UpdateMatrix();
                        GetComponent<Camera>().projectionMatrix = ortho;
                    }
                    else
                    {
                        distance += 0.01f * distance;
                    }
                }

                if (Input.GetKey ("right") || Input.GetKey ("d"))
                {
                    x +=  xSpeed  * 0.1f;
                }

                if (Input.GetKey ("left") || Input.GetKey ("a"))
                {
                    x -=  xSpeed  * 0.1f;
                }

                if (Input.GetKey ("e"))
                {
                    targetPosZ += 1f;
                }

                if (Input.GetKey ("q"))
                {
                    targetPosZ -= 1f;
                }

                if (targetPosZ < 0f)
                {
                    targetPosZ = 0f;
                }

                if (Input.GetKeyUp ("j"))
                {
                    isOrbiting = !isOrbiting;
                }

            }
            else if (isOnGoTo)
            {
                x = goToPos.x;
                y = goToPos.y;
                distance = goToPos.z;
                //isMoving = true;
                if (Mathf.Abs(goToPos.x - xSmooth) <= gotoThreshold && Mathf.Abs(goToPos.y - ySmooth) <= gotoThreshold  && Mathf.Abs(goToPos.z - smoothDistance) <= gotoThreshold)
                    //if ((transform.position - previousCameraPosition).magnitude <= gotoThreshold)
                {
                    isOnGoTo = false;
                    goToTime = smoothTime;
                    x = goToPos.x;
                    y = goToPos.y;
                    distance = goToPos.z;

                    previousxSmooth = xSmooth;
                    previousySmooth = ySmooth;
                    previoussmoothDistance = smoothDistance;
                    previoustargetPosZSmooth = targetPosZSmooth;

                    //Debug.Log("potato");
                }
                //Debug.Log(isMoving);
            }

            if (isOrbiting)
            {
                x += orbitSpeed * Time.deltaTime;
                //Debug.Log(x);
            }

            xSmooth = Mathf.SmoothDamp(xSmooth, x, ref xVelocity, goToTime);
            ySmooth = Mathf.SmoothDamp(ySmooth, y, ref yVelocity, goToTime);

            ySmooth = ClampAngle(ySmooth, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(ySmooth, xSmooth, 0);


            targetPosZSmooth = Mathf.SmoothDamp(targetPosZSmooth, targetPosZ, ref targetVelocity, goToTime);
            target.position = new Vector3(0, 0, targetPosZSmooth);


            //Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 25, distanceMin, distanceMax);
            smoothDistance = Mathf.SmoothDamp(smoothDistance, distance, ref dVelocity, goToTime);
            //smoothDistance = distance;


            Vector3 negDistance = new Vector3(0.0f, 0.0f, -smoothDistance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
            previousCameraPosition = position;
            transform.LookAt(target);


            if (!isOnGoTo && (previousxSmooth != xSmooth || previousySmooth != ySmooth ||  previoussmoothDistance != smoothDistance || previoustargetPosZSmooth != targetPosZSmooth))
            {
                isMoving = true;
            }

            previousxSmooth = xSmooth;
            previousySmooth = ySmooth;
            previoussmoothDistance = smoothDistance;
            previoustargetPosZSmooth = targetPosZSmooth;



            if (isMoving && !wasMoving)
            {
                startedMoving = true;
                //Debug.Log("Started Moving");
            }
            else
            {
                startedMoving = false;
            }

            if (wasMoving && !isMoving)
            {
                stoppedMoving = true;
                //Debug.Log("Stopped Moving");
            }
            else
            {
                stoppedMoving = false;
            }

            wasMoving = isMoving;


            if (Input.GetKey ("8"))
            {
                ProjectionZY();
            }
            if (Input.GetKey ("9"))
            {
                ProjectionXZ();
            }
            if (Input.GetKey ("0"))
            {
                ProjectionXY();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ToggleCameraProjection();
            }
        }
    }

    public void ToggleCameraProjection()
    {
        orthoOn = !orthoOn;
        if (orthoOn)
        {
            BlendToMatrix(ortho, 1f, 1);
        }
        else
        {
            BlendToMatrix(perspective, 1f, 0);
        }
    }

    public void ProjectionZY()
    {
        CameraGoToSmooth(-90f, 0f, 222f, 0.3f);
        TargetGoToSmooth(95, 0.3f);
        orthographicSize = 120f;
        UpdateMatrix();
        BlendToMatrix(ortho, 1f, 1);
        orthoOn = true;
    }

    public void ProjectionXY()
    {
        CameraGoToSmooth(0f, 0f, 12f, 0.3f);
        TargetGoToSmooth(-5f, 0.3f);
        orthographicSize = 80f;
        UpdateMatrix();
        BlendToMatrix(ortho, 1f, 1);
        orthoOn = true;
    }

    public void ProjectionXZ()
    {
        CameraGoToSmooth(0f, 90f, 222f, 0.3f);
        TargetGoToSmooth(95, 0.3f);
        orthographicSize = 120f;
        UpdateMatrix();
        BlendToMatrix(ortho, 1f, 1);
        orthoOn = true;
    }


    public void TargetGoToSmooth(float newTargetPosZ, float newTime)
    {
        targetPosZ = newTargetPosZ;
        goToTime = newTime;
        isOnGoTo = true;
    }

    public void CameraGoToSmooth(float newX, float newY, float newDist, float newTime)
    {
        goToPos = new Vector3(newX, newY, newDist);
        goToTime = newTime;
        isOnGoTo = true;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    //*** Matrix Methods *//
    public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
        {
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        }
        return ret;
    }

    private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration, int ease)
    {

        /* ease: 0= inOut; 1= in; 2= out */
        float ratio = matrixTransitionTime / duration;
        float power = 8f;
        //float param = Mathf.Pow(ratio, power) / (Mathf.Pow(ratio, power) + Mathf.Pow(1f - ratio, power));
        float param = 0f;
        while (matrixTransitionTime < duration)
        {
            ratio = matrixTransitionTime / duration;
            if (ease == 0)
            {
                power = 4f;
                param = Mathf.Pow(ratio, power) / (Mathf.Pow(ratio, power) + Mathf.Pow(1f - ratio, power));
            }
            else if (ease == 1)
            {
                power = 10f;
                param = - Mathf.Pow(ratio - 1, power) + 1;
            }
            else if (ease == 2)
            {
                power = 2f;
                param = Mathf.Pow(ratio - 1, power) + 1;
            }

            GetComponent<Camera>().projectionMatrix = MatrixLerp(src, dest, param);

            matrixTransitionTime += Time.deltaTime;
            yield return 1;
        }
        GetComponent<Camera>().projectionMatrix = dest;
    }

    public Coroutine BlendToMatrix(Matrix4x4 targetMatrix, float duration, int ease)
    {
        matrixTransitionTime = 0f;
        StopAllCoroutines();
        return StartCoroutine(LerpFromTo(GetComponent<Camera>().projectionMatrix, targetMatrix, duration, ease));
    }

    public void ToggleCameraColor()
    {
        Debug.Log("bababa");
        if (isBGWhite)
        {
            GetComponent<Camera>().backgroundColor = new Color(0.05f, 0.07f, 0.125f);
        }
        else
        {
            GetComponent<Camera>().backgroundColor = new Color(1, 1, 1);
        }

        isBGWhite = !isBGWhite;
    }


}