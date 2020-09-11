using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector3 cameraOffset;
    public float followSpeed = 10f;
    public float xMain = 0f;
    Vector3 velocity = Vector3.zero;
    public bool yBlock = false;

    // Camera follow target and smooth effect
    void FixedUpdate () {
        Vector3 targetPosition = target.position + cameraOffset;
        if (yBlock == false) {
            Vector3 clampedPosition = new Vector3 (Mathf.Clamp (targetPosition.x, xMain, float.MaxValue), targetPosition.y, targetPosition.z);
            Vector3 smoothPosition = Vector3.SmoothDamp (transform.position, clampedPosition, ref velocity, followSpeed * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        } else {
            Vector3 clampedPosition = new Vector3 (Mathf.Clamp (targetPosition.x, xMain, float.MaxValue), transform.position.y, targetPosition.z);
            Vector3 smoothPosition = Vector3.SmoothDamp (transform.position, clampedPosition, ref velocity, followSpeed * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }
}