using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float xSpeed = 4f;
    float ySpeed = 4f;
    float xRange = 6f;

    void Start()
    {

    }

    private void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = transform.position.x + xOffset;
        float rawNewYPos = transform.position.z + yOffset;

        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        transform.position = new Vector3(clampedXPos, transform.position.y, rawNewYPos);
    }

}
