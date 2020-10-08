using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockYPosition : MonoBehaviour
{
    public float newY = 7.84f;
    public GameObject cam;

    void Update()
    {
        if (transform.position.y != newY) {
            restCameraYPostion ();
            transform.position = new Vector3(transform.position.x,newY,transform.position.z);
        }
    }

    public void restCameraYPostion () {
        newY = transform.position.y - cam.transform.position.y;
    }
}
