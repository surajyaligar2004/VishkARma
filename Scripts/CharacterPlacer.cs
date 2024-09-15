using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterPlacer : MonoBehaviour
{
    public GameObject character1Prefab;
    public GameObject character2Prefab;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    private GameObject spawnedCharacter;
    private GameObject characterToPlace;

    void Start()
    {
        if (planeManager == null)
        {
            UnityEngine.Debug.LogError("AR Plane Manager is not assigned. Please assign it.");
        }
        if (raycastManager == null)
        {
            UnityEngine.Debug.LogError("AR Raycast Manager is not assigned. Please assign it.");
        }
    }

    public void PlaceCharacter1()
    {
        characterToPlace = character1Prefab;
        PlaceCharacterOnPlane();
    }

    public void PlaceCharacter2()
    {
        characterToPlace = character2Prefab;
        PlaceCharacterOnPlane();
    }

    private void PlaceCharacterOnPlane()
    {
        if (characterToPlace == null)
        {
            UnityEngine.Debug.LogError("No character selected to place.");
            return;
        }

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            ARPlane plane = planeManager.GetPlane(hits[0].trackableId);

            if (plane != null && plane.boundary.Length > 0)
            {
                UnityEngine.Debug.Log("Plane detected and placing character");

                if (spawnedCharacter != null)
                {
                    Destroy(spawnedCharacter);
                }

                spawnedCharacter = Instantiate(characterToPlace, hitPose.position, hitPose.rotation);
            }
            else
            {
                UnityEngine.Debug.Log("No valid plane detected.");
            }
        }
        else
        {
            UnityEngine.Debug.Log("No plane detected in the center of the screen.");
        }
    }
}