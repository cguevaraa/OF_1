using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRigidbody;
    private Camera mainCamera;

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
            ballRigidbody.isKinematic = false;
            return;
        }

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        ballRigidbody.isKinematic = true;
        ballRigidbody.position = worldPosition;

    }
}
