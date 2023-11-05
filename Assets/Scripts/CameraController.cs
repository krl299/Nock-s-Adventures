using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float lerpTime = 2;
    public GameObject player;
    private Transform objetive;

    private void Awake()
    {
        // Inicialization
        objetive = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        transform.position += new Vector3(0f, 3f, 0f);
    }

    private void Update()
    {
        FollowObjetive();
    }

    private void FollowObjetive()
    {
        // Lerp position
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, objetive.position.x, Time.deltaTime * lerpTime),
            Mathf.Lerp(transform.position.y, objetive.position.y, Time.deltaTime * lerpTime),
            -10
            );
    }
}
