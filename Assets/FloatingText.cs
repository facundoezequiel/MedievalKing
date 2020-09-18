using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {
    Quaternion rotation;
    // public Vector3 Offset = new Vector3 (0, 2, 0);

    void Awake () {
        rotation = transform.rotation;
    }

    void Start () {
        Invoke ("DestroyText", 0.6f);
        // transform.localPosition += Offset;
    }

    void LateUpdate () {
        transform.rotation = rotation;
    }

    public void DestroyText () {
        Destroy (this.gameObject);
    }
}