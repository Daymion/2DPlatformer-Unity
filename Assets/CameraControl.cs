using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{
    // Camerea variables
    public int cameraType;

    // Camera Type 1: "Following the player" variables
    public GameObject player;
    private Vector3 cameraStartPos; // The camera should be in the lowest and most left position

    // Camera Type 2: "Autoscrolling" variables
    public Vector3 cameraEndPos; // The camera can start anywhere
    public float cameraSpeed;
    private float cameraStep;
    private bool cameraMove = true;

    void Start()
    {
        cameraStartPos = transform.position;
    }

    void Update()
    {
        switch(cameraType)
        {
            case 1:
                CameraType1();
                break;
            case 2:
                CameraType2();
                break;
            default:
                Debug.Log("No camera type chosen.");
                break;
        }
    }

    /**
     * The camera will follow the player but not go past the lowest and most left point
     * The camera needs to be placed in the lowest and most left point 
     */
    private void CameraType1()
    {
        if (player.transform.position.x > cameraStartPos.x)
        {
            this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, cameraStartPos.z);
        }
        else
        {
            this.transform.position = new Vector3(cameraStartPos.x, this.transform.position.y, cameraStartPos.z);
        }
        if (player.transform.position.y > cameraStartPos.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, cameraStartPos.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, cameraStartPos.y, cameraStartPos.z);
        }
    }

    /**
     * The camera will move from it's starting position to the destination point
     */
    private void CameraType2()
    {
        if (cameraMove)
        {
            cameraStep = cameraSpeed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, cameraEndPos, cameraStep);
        }

        if (Vector3.Distance(cameraStartPos, cameraEndPos) < 0.001f)
        {
            cameraMove = false;
        }
    }
}
