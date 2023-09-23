using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_CaiylnHitAA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleTrigger()
    {
        // 파티클 시스템의 충돌 이벤트가 호출될 때 실행됩니다.
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            Vector3 particlePosition = p.position;

            // 파티클의 위치를 이용하여 충돌 여부를 확인합니다.
            Collider[] colliders = Physics.OverlapSphere(particlePosition, 0.1f); // 적절한 충돌 검출 반경을 지정하세요.

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player") && collider.gameObject.name != "Caityln")
                {
                    Debug.Log("충돌 확인");
                    // 그라가스와 충돌했을 때 실행할 코드를 여기에 작성합니다.
                    // 예를 들어, 그라가스에게 데미지를 입히거나 다른 상호작용을 수행할 수 있습니다.
                }
            }
        }
    }
}
