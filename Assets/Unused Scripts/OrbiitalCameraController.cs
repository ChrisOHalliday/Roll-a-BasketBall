using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Schema;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbiitalCameraController : MonoBehaviour
{
    //target object
    [SerializeField]
    private Transform focus;

    //offset distance from target object
    [SerializeField, Range(1.0f, 10.0f)]
    float offsetDistance = 6.0f;

    //distance before camera refocuses on target
    [SerializeField, Min(0.0f)]
    float focusRadius = 1.0f;

    //the factor at which determines the rate the camera refocuses onto the target
    [SerializeField, Range(0.0f, 1.0f)]
    float focusCentering = 0.5f;

    //camera rotation speed
    [SerializeField, Range(1.0f, 360.0f)]
    float rotationSpeed = 90.0f;

    //camera angle constraints
    [SerializeField, Range(-89.0f, 89.0f)]
    float minVertAngle = -30.0f, maxVertAngle = 60.0f;

    [SerializeField, Min(0.0f)]
    float alignDelay = 5.0f;

    [SerializeField, Range(0.0f, 90.0f)]
    float alignSmoothRange = 45.0f;

    [SerializeField]
    LayerMask obstructionMask = -1;

    float timeSinceLastRotation;

    Vector3 focusPoint, previousFocusPoint;
    Vector2 orbitAngles = new Vector2(20.0f, 0.0f);

    Camera regularCamera;

    Vector3 CameraHalfExtends { 
        get {
            Vector3 halfExtends;
            halfExtends.y = regularCamera.nearClipPlane * Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
            halfExtends.x = halfExtends.y * regularCamera.aspect;
            halfExtends.z = 0.0f;
            return halfExtends; 
        } 
    }


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        regularCamera = GetComponent<Camera>();
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
        
    }
    private void OnValidate()
    {
        if (maxVertAngle < minVertAngle)
        {
            maxVertAngle = minVertAngle;
        }
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();
        Quaternion lookRotation;
        if (ManualRotation() || AutomaticRotation())
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * offsetDistance;

        Vector3 rectOffset = lookDirection * regularCamera.nearClipPlane;
        Vector3 rectPosition = lookPosition + rectOffset;
        Vector3 castFrom = focus.position;
        Vector3 castLine = rectPosition - castFrom;
        float castDistance = castLine.magnitude;
        Vector3 castDirection = castLine / castDistance;


        if (Physics.BoxCast(castFrom,CameraHalfExtends,castDirection, out RaycastHit hit, lookRotation, castDistance,obstructionMask))
        {
            rectPosition = castFrom + castDirection * hit.distance;
            lookPosition = rectPosition - rectOffset;
        }


        transform.SetPositionAndRotation(lookPosition, lookRotation);

    }

    void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position;
        float distance = Vector3.Distance(targetPoint, focusPoint);
        float t = 1.0f;

        if (focusRadius > 0.01f && focusCentering > 0.0f)
        {
            t = Mathf.Pow(1.0f - focusCentering, Time.unscaledDeltaTime);
        }
        if (distance > focusRadius) {

            t = Mathf.Min(t,focusRadius/distance);
        }       
        focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
    }

    bool ManualRotation()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        const float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            timeSinceLastRotation = Time.unscaledDeltaTime;
            return true;
        }
        return false;
    }

    void ConstrainAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVertAngle, maxVertAngle);
        if (orbitAngles.y < 0.0f)
        {
            orbitAngles.y += 360.0f;
        }
        else if (orbitAngles.y >= 360.0f)
        {
            orbitAngles.y -= 360.0f;
        }
    }

    bool AutomaticRotation()
    {
        if (Time.unscaledDeltaTime - timeSinceLastRotation <alignDelay)
        {
            return false;
        }

        Vector2 movement = new Vector2(focusPoint.x - previousFocusPoint.x, focusPoint.z - previousFocusPoint.z);
        float movementSqr = movement.sqrMagnitude;
        if (movementSqr < 0.0001f)
        {
            return false;
        }

        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementSqr));
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y,headingAngle));
        float rotationChange = rotationSpeed * Mathf.Min(Time.unscaledDeltaTime,movementSqr);
        if (deltaAbs < alignSmoothRange)
        {
            rotationChange *= deltaAbs / alignSmoothRange;
        }
        else if (180.0F - deltaAbs < alignSmoothRange)
        {
            rotationChange *= (180.0f - deltaAbs) / alignSmoothRange; 
        }
        orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y,headingAngle,rotationChange);

        return true;
    }

    static float GetAngle(Vector2 direction)
    {
        float angle =Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0.0f ? 360.0f - angle : angle;
    }
    

}
