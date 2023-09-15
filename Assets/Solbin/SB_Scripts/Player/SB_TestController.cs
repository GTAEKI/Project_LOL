using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_TestController : MonoBehaviour
{
    Vector3 movePoint;
    Camera mainCamera;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                movePoint = raycastHit.point;
            }

            moving = true;
        }

        if (Vector3.Distance(transform.position, movePoint) > 0.1f && moving)
        {
            Move();
        }
        else
            moving = false;
        
    }

    void Move()
    {
        int speed = 4;

        Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * speed;

        transform.Translate(thisUpdatePoint * Time.deltaTime);
    }
}
