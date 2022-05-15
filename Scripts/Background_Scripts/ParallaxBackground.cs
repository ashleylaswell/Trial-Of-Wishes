using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPos;
    private float textureSizeX;
    [SerializeField]
    private Vector2 parallaxMultiplier;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPos = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureSizeX = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMove = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(deltaMove.x * parallaxMultiplier.x, deltaMove.y);
        lastCameraPos = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureSizeX)
        {
            float offset = (cameraTransform.position.x - transform.position.x) % textureSizeX;
            transform.position = new Vector3(cameraTransform.position.x, transform.position.y);
        }
    }
}
