using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushCheck : MonoBehaviour
{
    private readonly int floatAlpha = Shader.PropertyToID("_Alpha");

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bush")
        {
            Debug.Log("In the Bush");

            transform.parent.GetComponent<MeshRenderer>().material.SetFloat(floatAlpha, 0.5f);

            Camera.main.cullingMask = ~(1 << LayerMask.GetMask("Unit_HUD"));

            // TODO: 팀에 따라 레이어 처리 
            Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Unit_Blue"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Bush")
        {
            Debug.Log("Out the Bush");

            transform.parent.GetComponent<MeshRenderer>().material.SetFloat(floatAlpha, 1f);

            Camera.main.cullingMask |= 1 << LayerMask.GetMask("Unit_HUD");

            // TODO: 팀에 따라 레이어 처리
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Unit_Blue");
        }
    }
}
