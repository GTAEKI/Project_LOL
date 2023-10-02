using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_SpellQ : MonoBehaviour
{
    private BoxCollider hitbox;     // 히트박스
    
    public List<GameObject> HitTargets { private set; get; } = new List<GameObject>();    // 피해를 받은 적

    private void Start()
    {
        hitbox = GetComponent<BoxCollider>();

        SetActiveHitbox(false);
    }

    public void SetActiveHitbox(bool isActive)
    {
        if (isActive) hitbox.transform.localScale = Vector3.one;
        else hitbox.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Dummy")
        {
            if(!HitTargets.Find(x => x.gameObject == other.gameObject))
            {
                HitTargets.Add(other.gameObject);
            }
        }
    }
}
