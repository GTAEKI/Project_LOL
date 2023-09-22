using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleSkillE : MonoBehaviour
{
    int skillSpeed = 30;
    public float maxDistance = 8f; // 스킬 이동 최대 거리

    private Vector3 initialPosition; // 스킬 시작 위치

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * skillSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) >= maxDistance)
        {
            Destroy(gameObject); // 스킬 오브젝트 제거
        }
    }
}
