using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float _xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public float maxLookUp = 80f;
    public float minLookDown = -80f;

    public void ProcessLook(Vector2 input, float delta)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        _xRotation -= (mouseY * delta) * ySensitivity;
        _xRotation = Mathf.Clamp(_xRotation, minLookDown, maxLookUp);
        cam.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    } 
}
