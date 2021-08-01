using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float devHeight = 12.8f;
    float devWidth = 7.2f;

    void Start()
    {
        float screenHeight = Screen.height;
        Debug.Log("screenHeight = " + screenHeight);

        var camera = this.GetComponent<Camera>();
        Debug.Log("cam.aspect: " + camera.aspect);

        float orthographicSize = camera.orthographicSize;
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        float cameraWidth = orthographicSize * 2 * aspectRatio;
        Debug.Log("cameraWidth = " + cameraWidth);

        return;
        if (cameraWidth < devWidth)
        {
            orthographicSize = devWidth / (2 * aspectRatio);
            Debug.Log("new orthographicSize = " + orthographicSize);
            camera.orthographicSize = orthographicSize;
        }
    }
}
