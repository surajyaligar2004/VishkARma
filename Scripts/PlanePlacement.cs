using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlanePlacement : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ModelSelector modelSelector;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;
                List<ARRaycastHit> hits = new List<ARRaycastHit>();

                if (raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    modelSelector.PlaceModel(hitPose.position);
                }
            }
        }
    }
}
