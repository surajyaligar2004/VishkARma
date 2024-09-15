using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using Unity.Collections;

public class PlaceObjectOnPlane : MonoBehaviour
{
    public GameObject objectToPlace;
    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                ARPlane detectedPlane = GetPlaneAtPosition(hitPose.position);
                if (detectedPlane != null && objectToPlace != null)
                {
                    Vector3 planeCenter = detectedPlane.center;
                    Instantiate(objectToPlace, planeCenter, Quaternion.identity);
                }
            }
        }
    }

    private ARPlane GetPlaneAtPosition(Vector3 position)
    {
        foreach (var plane in planeManager.trackables)
        {
            if (IsPointInsideBoundary(position, plane.boundary))
            {
                return plane;
            }
        }
        return null;
    }

    private bool IsPointInsideBoundary(Vector3 point, NativeArray<Vector2> boundary)
    {
        int intersections = 0;
        int vertexCount = boundary.Length;

        for (int i = 0, j = vertexCount - 1; i < vertexCount; j = i++)
        {
            Vector2 vi = boundary[i];
            Vector2 vj = boundary[j];

            if (((vi.y > point.z) != (vj.y > point.z)) &&
                (point.x < (vj.x - vi.x) * (point.z - vi.y) / (vj.y - vi.y) + vi.x))
            {
                intersections++;
            }
        }

        return (intersections % 2) == 1;
    }
}