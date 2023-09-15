using UnityEngine;

public class spellGhostingEffect : MonoBehaviour
{
    public GameObject ghostPrefab; // 잔상을 나타내는 프리팹
    public float ghostCreationInterval = 0.2f; // 잔상 생성 간격
    public float ghostDuration = 1.0f; // 잔상 지속 시간

    private bool isGhosting = false; // 유체화 효과 활성화 여부
    private float nextGhostCreationTime; // 다음 잔상 생성 시간

    private void Update()
    {
        // D 키를 누르면 유체화 효과를 활성화하고, 뗄 때 비활성화합니다.
        if (Input.GetKeyDown(KeyCode.D))
        {
            isGhosting = true;
        }
        // 유체화 효과가 활성화되어야 생성 가능하도록 체크합니다.
        if (isGhosting)
        {
            // 우클릭을 감지하여 유체화 효과를 생성합니다.
            if (Input.GetMouseButtonDown(1) && Time.time >= nextGhostCreationTime)
            {
                CreateGhost();
                nextGhostCreationTime = Time.time + ghostCreationInterval;
            }
        }
    }

    private void CreateGhost()
    {
        // 잔상 프리팹을 인스턴스화합니다.
        GameObject ghostInstance = Instantiate(ghostPrefab, transform.position, Quaternion.identity);

        // 일정 시간이 지난 후에 잔상을 삭제합니다.
        Destroy(ghostInstance, ghostDuration);
    }
}