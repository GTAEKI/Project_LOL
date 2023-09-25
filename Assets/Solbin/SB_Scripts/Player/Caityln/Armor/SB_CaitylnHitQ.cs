using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_CaitylnHitQ : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<Unit>().CurrentUnitStat.OnDamaged(5);
        }
    }
}
