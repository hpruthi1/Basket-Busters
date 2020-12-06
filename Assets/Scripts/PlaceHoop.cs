using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceHoop : MonoBehaviour
{
    public GameObject Hoop;
    public GameObject Ball;
    public Transform FirstPersonCamera;
    private List<DetectedPlane> allPlanes;
    private List<AugmentedImage> trackedMarkers = null;
    private Dictionary<string, Transform> CreatedObjects;
    public AugmentedImageDatabase database;
    public Texture2D BallSpawner;
    public TextMeshProUGUI PlaneCount;

    private bool isPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        allPlanes = new List<DetectedPlane>();
        trackedMarkers = new List<AugmentedImage>();
        CreatedObjects = new Dictionary<string, Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaced)
        {
            return;
        }
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;
        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) > 0)
            {
                var gameObject = Instantiate(Hoop, hit.Pose.position, Hoop.transform.rotation);
                isPlaced = true;

                //StartCoroutine(BallSpawn());
            }
        }

        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            return;
        }

        Session.GetTrackables<DetectedPlane>(allPlanes, TrackableQueryFilter.All);
        Session.GetTrackables<AugmentedImage>(trackedMarkers);
        PlaneCount.text = "Plane Count:" + allPlanes.Count;

        foreach (AugmentedImage marker in trackedMarkers)
        {
            if (!CreatedObjects.ContainsKey(marker.Name))
            {
                GameObject go = Instantiate(Ball);
                CreatedObjects[marker.Name] = Ball.transform;
            }
        }

        IEnumerator BallSpawn()
        {
            yield return new WaitForSeconds(3);
            var ball = Instantiate(Ball);
            Hoop.GetComponent<SwipeControl>().rotatespeed = 0;
        }
    }
}
