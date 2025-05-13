using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivotRigidbody;
    [SerializeField] private float detachDelay = 0.15f;
    [SerializeField] private float respawnDelay = 5.0f;

    private GameObject currentBall;
    private Rigidbody2D ballRigidbody;
    private SpringJoint2D ballSpringJoint;
    private Camera mainCamera;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        // Out the method if not touching the screen
        if(!Touchscreen.current.primaryTouch.press.IsPressed())
        {
            if(isDragging)
            {
                LaunchBall();
                Invoke(nameof(SpawnBall), respawnDelay);
                isDragging = false;
            }

            return;
        }

        isDragging = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition); 
        ballRigidbody.isKinematic = true;
        ballRigidbody.position = worldPosition;

    }

    private void SpawnBall()
    {
        if(ballPrefab != null)
        {
            if(currentBall != null)
            {
                Destroy(currentBall);
            }

            currentBall = Instantiate(ballPrefab, pivotRigidbody.transform);
            ballRigidbody = currentBall.GetComponent<Rigidbody2D>();
            ballSpringJoint = currentBall.GetComponent<SpringJoint2D>();
            ballSpringJoint.connectedBody = pivotRigidbody;
        }
    }

    private void LaunchBall()
    {
        if (ballRigidbody != null)
        {
            ballRigidbody.isKinematic = false;
            ballRigidbody = null;
            Invoke(nameof(DetachBall), detachDelay);
        }
    }

    private void DetachBall()
    {
        if (ballSpringJoint != null)
        {
            ballSpringJoint.enabled = false;
            ballSpringJoint = null;
        }
    }
}
