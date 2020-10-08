using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {
    private float lenght, startPosition;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
       startPosition = transform.position.x;
       lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
    }
}
