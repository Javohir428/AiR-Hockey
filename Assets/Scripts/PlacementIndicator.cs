using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{


    private ARRaycastManager rayManager;

    public float speed = 300f;
    public Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();

        // hide the placement indicator visual

    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneEstimated);

        if (hits.Count > 0)
        {
            rigidBody.AddForce((hits[0].pose.position - transform.position) * speed);

        }

    }
}
