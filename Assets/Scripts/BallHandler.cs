using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRigidbody;
    [SerializeField] private SpringJoint2D ballSpringJoint;
    [SerializeField] private float detachDelay = 0.15f;

    private Camera mainCamera;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;    
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

    private void LaunchBall()
    {
        ballRigidbody.isKinematic = false;
        ballRigidbody = null;
        Invoke(nameof(DetachBall), detachDelay);
    }

    private void DetachBall()
    {
        ballSpringJoint.enabled = false;
        ballSpringJoint = null;
    }
}
