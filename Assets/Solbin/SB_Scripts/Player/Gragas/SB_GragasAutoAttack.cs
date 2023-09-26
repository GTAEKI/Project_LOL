using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_GragasAutoAttack : MonoBehaviour
{
    GameObject gragas;
    GameObject enemy;
    bool trace = false;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        gragas = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player" || other.name != "Gragas")
    //    {
    //        enemy = other.gameObject;
    //        targetPos = enemy.transform.position;
    //        gragas.transform.LookAt(targetPos);

    //        trace = true;
    //    }
    //}

    private void FixedUpdate()
    {
        if (trace)
        {
            //if (Vector3.Distance(gragas.transform.position, targetPos) > 1)
            //{
            //    gragas.transform.Translate(Vector3.forward * Time.deltaTime * 8f);
            //}

            //if (Vector3.Distance(gragas.transform.position, targetPos) <= 1)
            //{
            //    trace = false;
            //}
        }
    }

}
