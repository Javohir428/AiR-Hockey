using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


//[RequireComponent(typeof(ARSessionOrigin))]
//[RequireComponent(typeof(ARRaycastManager))]
//[RequireComponent(typeof(ARAnchorManager))]

public class PlaceGameArena : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private ARPlaneManager m_ARPlaneManager;
    private List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private ARPointCloudManager pointCloudManager;
    ARAnchorManager m_AnchorManager;
    ARAnchor m_anchor;

    private bool isPlaced;

    public GameObject visual;
    public GameObject gameArena;

    public GameObject reanchor_button;
    public GameObject start_game_button;


    ARAnchor CreateAnchor(in ARRaycastHit hit)
    {
        ARAnchor anchor;

        // If we hit a plane, try to "attach" the anchor to the plane
        if (hit.trackable is ARPlane plane)
        {
            var planeManager = GetComponent<ARPlaneManager>();
            if (planeManager)
            {
                var oldPrefab = m_AnchorManager.anchorPrefab;
                m_AnchorManager.anchorPrefab = gameArena;
                anchor = m_AnchorManager.AttachAnchor(plane, hit.pose);
                m_AnchorManager.anchorPrefab = oldPrefab;
                return anchor;
            }
        }

        // Note: the anchor can be anywhere in the scene hierarchy
        var gameObject = Instantiate(gameArena, hit.pose.position, hit.pose.rotation * Quaternion.Euler(0, 90, 0));

        // Make sure the new GameObject has an ARAnchor component
        anchor = gameObject.GetComponent<ARAnchor>();
        if (anchor == null)
        {
            anchor = gameObject.AddComponent<ARAnchor>();
        }

        return anchor;
    }

    public void RemoveAllAnchors()
    {
        Destroy(m_anchor.gameObject);
    }

    private void Start()
    {
        // get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        m_AnchorManager = FindObjectOfType<ARAnchorManager>();
        m_ARPlaneManager = FindObjectOfType<ARPlaneManager>();
        pointCloudManager = FindObjectOfType<ARPointCloudManager>();

        visual = transform.GetChild(0).gameObject;

        // hide the placement indicator visual
        visual.SetActive(false);
        gameArena.SetActive(false);
        isPlaced = false;

        reanchor_button.SetActive(false);
        start_game_button.SetActive(false);
    }

    public void TogglePlaneDetection()
    {
        m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;

        if (!m_ARPlaneManager.enabled)
        {
            SetAllPlanesActive(false);
        }
    }
    public void TogglePointDetection()
    {
        pointCloudManager.enabled = !pointCloudManager.enabled;

        if (!pointCloudManager.enabled)
        {
            SetAllPointsActive(false);
        }
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }

    }

    void SetAllPointsActive(bool value)
    {
        foreach (var point in pointCloudManager.trackables)
        {
            point.gameObject.SetActive(value);
        }

    }

    private void Update()
    {
        if ((!isPlaced) && (!gameArena.activeSelf))
        {
            // shoot a raycast from the center of the screen
            rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), s_Hits, TrackableType.Planes);
            // if we hit an AR plane surface, update the position and rotation
            if (s_Hits.Count > 0)
            {
                transform.position = s_Hits[0].pose.position;
                transform.rotation = s_Hits[0].pose.rotation;

                // enable the visual if it's disabled
                if (!visual.activeInHierarchy)
                    visual.SetActive(true);

            }
        }

        if ((!isPlaced) && (Input.GetTouch(0).phase == TouchPhase.Began) && (!gameArena.activeSelf) && (visual.activeSelf))
        {
            visual.SetActive(false);
            gameArena.SetActive(true);
            isPlaced = true;
            TogglePointDetection();
            TogglePlaneDetection();

            reanchor_button.SetActive(true);
            start_game_button.SetActive(true);


            // Raycast hits are sorted by distance, so the first one will be the closest hit.
            var hit = s_Hits[0];

            // Create a new anchor
            var anchor = CreateAnchor(hit);
            if (anchor)
            {
                // Remember the anchor so we can remove it later.
                m_anchor = anchor;
            }
        }

    }


}
