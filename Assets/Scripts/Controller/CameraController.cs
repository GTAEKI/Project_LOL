using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;                  // target
    private bool isFollow = true;               // Camera Follow Player
    private float camSpeed = 20f;               // Camera movementSpeed
    private float screenSizeThickness = 10;     // Screen Side
    public  float camFOV;                       // Camera Field of View
    private float zoomSpeed = 10f;              // Zoom speed
    private float mouseScrollInput;             // Mouse Scroll Input
    private  Vector3 delta;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        delta = new Vector3(0, 15, -9f); //230914 배경택 값 수정

        //camFOV = GetComponent<Camera>().fieldOfView;        
        //transform.position = player.transform.position + delta;
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            if (!isFollow) isFollow = true;
            else isFollow = false;
        }

        Vector3 pos = transform.position;

        if(isFollow && player != null)
        {   // Follow Target Player
            pos = player.transform.position + delta;
            transform.LookAt(player.transform);
        }

        if(!isFollow)
        {   // Not Follow Target Player
            if(Input.GetKey(KeyCode.Space))
            {   // Holding Player Position
                pos = player.transform.position + delta;
                transform.LookAt(player.transform);
            }

            if(Input.mousePosition.y >= Screen.height - screenSizeThickness)
            {   // Check to Screen Side Thickness
                pos.x += camSpeed * Time.deltaTime;
                pos.z += camSpeed * Time.deltaTime;
            }

            if(Input.mousePosition.y <= screenSizeThickness)
            {
                pos.x -= camSpeed * Time.deltaTime;
                pos.z -= camSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x >= Screen.width - screenSizeThickness)
            {
                pos.x += camSpeed * Time.deltaTime;
                pos.z -= camSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x <= screenSizeThickness)
            {
                pos.x -= camSpeed * Time.deltaTime;
                pos.z += camSpeed * Time.deltaTime;
            }
        }

        transform.position = pos;

        // Zoom In
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 30, 60);
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, camFOV, zoomSpeed);
    }
}
