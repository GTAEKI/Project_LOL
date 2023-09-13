using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Illusion : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    
    public bool IsPossible { private set; get; }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Bush")
        {
            meshRenderer.materials[0].color = Color.red;
            IsPossible = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Bush")
        {
            meshRenderer.materials[0].color = Color.green;
            IsPossible = true;
        }
    }
}
