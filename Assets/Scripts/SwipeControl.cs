using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{ 
    public float rotatespeed = 10f;
    private float startingPosition;
    public CollisionDetection cd;

    public PlaceHoop placeHoop;

    private void Start()
    {
        placeHoop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlaceHoop>();
    }

    void Update()
    {
        if (placeHoop.isBallSpawned == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startingPosition = touch.position.x;
                        break;
                    case TouchPhase.Moved:
                        if (startingPosition > touch.position.x)
                        {
                            transform.Rotate(Vector3.up, rotatespeed * Time.deltaTime);
                        }
                        else if (startingPosition < touch.position.x)
                        {
                            transform.Rotate(Vector3.up, -rotatespeed * Time.deltaTime);
                        }
                        break;
                    case TouchPhase.Ended:
                        Debug.Log("Touch Phase Ended.");
                        break;
                }
            }
        }
    }
}
