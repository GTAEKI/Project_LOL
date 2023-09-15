using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // 이동 속도

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {

        // 마우스 커서 위치를 월드 좌표로 변환합니다.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = transform.position.y; // Y값을 고정합니다.

        // 플레이어의 현재 위치에서 마우스 커서 위치로의 방향을 구합니다.
        Vector3 moveDirection = (mousePosition - transform.position).normalized;

        // 플레이어를 마우스 커서 방향으로 이동합니다.
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
