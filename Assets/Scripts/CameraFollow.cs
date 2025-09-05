using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player's transform
    public float smoothness = 0.125f; // Smoothness of the camera follow

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position; // Calculate the initial offset
        offset.y = 0; // Ensure the Z value of the offset is 0
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset; // Calculate the target position

        // Interpolate between current position and target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothness);
         smoothedPosition.y = transform.position.y; // Preserve the Z position


        // Set the position of the camera
        transform.position = smoothedPosition;
    }
}
