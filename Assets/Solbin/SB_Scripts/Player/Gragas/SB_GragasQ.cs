using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SB_GragasQ : MonoBehaviour
{
    GameObject q1;
    GameObject q2;

    Camera camera;

    bool isAttack = false;
    Vector3 targetPosition;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject q1Prefab = (GameObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Solbin/SB_Prefabs/Gragas/Barrel_Q1.prefab", typeof(GameObject));
        q1 = Instantiate(q1Prefab);
        q1.transform.position = new Vector3(0, 0, -10);

        GameObject q2Prefab = (GameObject)AssetDatabase.LoadAssetAtPath
            ("Assets/Solbin/SB_Prefabs/Gragas/Barrel_Q2.prefab", typeof(GameObject));
        q2 = Instantiate(q2Prefab);
        q2.transform.position = new Vector3 (0, 0, -10);

        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkillQ()
    {
        if (!isAttack)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Floor");

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                targetPosition = hit.point;
                StartCoroutine(RollingQ());
            }
        }
    }

    private IEnumerator RollingQ()
    {
        transform.LookAt(targetPosition);
        animator.SetTrigger("PressQ");
        //q1.transform.LookAt(targetPosition);
        //q1.transform.position = transform.position + new Vector3 (0, 0, 5);

        yield return null;
    }
}
