using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SB_PlayerMoving : MonoBehaviour
{
    Vector3 movePoint;
    Camera mainCamera;
    bool moving = false;
    Animator animator;
    Transform model;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();

        layerMask = 1 << LayerMask.NameToLayer("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
            {
                movePoint = raycastHit.point;
            }

            animator.SetBool("Run", true);
            moving = true;
        }

        if (Vector3.Distance(transform.position, movePoint) > 0.1f && moving)
        {
            Move();
        }
        else
        {
            animator.SetBool("Run", false);
            moving = false;
        }
        
    }

    void Move()
    {
        int speed = 4;

        Vector3 dir = movePoint - transform.position;
        dir.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(dir); // 목표 방향

        float rotationSpeed = 10f; // 회전 속도
        // 부드럽게 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += dir.normalized * Time.deltaTime * speed; // 캐릭터 이동
    }
}
