using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;

    public float maxHeight = 17.0f;
    public float maxWidth = 20.0f;

    private float maxCameraX;
    private float maxCameraY;
    private float minCameraX;
    private float minCameraY;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        maxCameraX = maxHeight - height;
        minCameraX = -maxHeight + height;

        maxCameraY = maxWidth - width;
        minCameraY = -maxWidth + width;
    }

    Rect getCameraBounds()
    {
        Rect bounds = new Rect();

        var cameraPosition = Camera.main.transform.position;

        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        bounds.x = cameraPosition.x - width;
        bounds.width = bounds.x + width * 2.0f;
        bounds.y = cameraPosition.y - height;
        bounds.height = bounds.y + height * 2.0f;

        return bounds;
    }

    void UpdateBounds()
    {



    }

    // Update is called once per frame
    void Update()
    {
        var cameraPosition = Camera.main.transform.position;
        cameraPosition.x = Mathf.Clamp((player1.transform.position.x + player2.transform.position.x) / 2.0f, minCameraX, maxCameraX);
        cameraPosition.y = Mathf.Clamp((player1.transform.position.y + player2.transform.position.y) / 2.0f, minCameraY, maxCameraY);
        Camera.main.transform.position = cameraPosition;

        player1.GetComponent<PlayerController>().SetCameraBounds(this.getCameraBounds());
        player2.GetComponent<PlayerController>().SetCameraBounds(this.getCameraBounds());
    }
}
