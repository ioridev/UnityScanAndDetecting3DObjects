using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToPlace;

    [SerializeField]
    private ARRaycastManager arRaycastManager;

    [SerializeField]
    private ARPointCloudManager arPointCloudManager;

    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began) return;

        if (arRaycastManager.Raycast(touch.position, arRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = arRaycastHits[0].pose;
            Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
        }
    }

    public void TogglePointCloud(bool isEnabled)
    {
        arPointCloudManager.enabled = isEnabled;
        foreach (var pointCloud in arPointCloudManager.trackables)
        {
            pointCloud.gameObject.SetActive(isEnabled);
        }
    }
}
