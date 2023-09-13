using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Define.CameraMode mode = Define.CameraMode.QuaterView;
    [SerializeField] private Vector3 delta;       // 카메라와 플레이어의 거리
    [SerializeField] private GameObject player;

    private void Start()
    {
        delta = new Vector3(0, 9, -6.5f);
    }

    private void LateUpdate()
    {
        if (mode == Define.CameraMode.QuaterView)
        {
            transform.position = player.transform.position + delta;
            transform.LookAt(player.transform);
        }
    }
}
