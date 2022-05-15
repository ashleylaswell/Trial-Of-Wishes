using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public Vector3 newtrans;

    void Start()
    {
        offset.x = transform.position.x - player.transform.position.x;
        offset.z = transform.position.z - player.transform.position.z;
        newtrans = transform.position;

    }
    private void FixedUpdate()
    {
        newtrans.x = player.transform.position.x + offset.x;
        newtrans.z = player.transform.position.z + offset.z;
        transform.position = newtrans;
    }
}
