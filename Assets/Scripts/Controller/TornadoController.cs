using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TornadoController : MonoBehaviour
{
    public Vector3 moveDirect = Vector3.zero;

    private void Update()
    {
        transform.position += moveDirect * 60f * Time.deltaTime;
    }
}
