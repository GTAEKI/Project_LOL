using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashe_SkillR : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
