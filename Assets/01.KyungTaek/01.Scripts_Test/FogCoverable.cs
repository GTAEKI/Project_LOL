using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FogCoverable : MonoBehaviourPun
{
    //Renderer renderer;
    SkinnedMeshRenderer skinnedMeshRenderer;


    void Start()
    {
        //renderer = GetComponent<Renderer>();
        //if (!photonView.IsMine)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            FieldOfView.OnTargetsVisibilityChange += FieldOfViewOnTargetsVisibilityChange;
        }
    }

    void OnDestroy()
    {
        FieldOfView.OnTargetsVisibilityChange -= FieldOfViewOnTargetsVisibilityChange;
    }

    void FieldOfViewOnTargetsVisibilityChange(List<Transform> newTargets)
    {
        Debug.Log("왜 못찾니");
        //renderer.enabled = newTargets.Contains(transform);
        skinnedMeshRenderer.enabled = newTargets.Contains(transform);
        //skinnedMeshRenderer.enabled = true;

    }
}