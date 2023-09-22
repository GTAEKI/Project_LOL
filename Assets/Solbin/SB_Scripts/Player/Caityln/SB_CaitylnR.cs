using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SB_CaitylnR : MonoBehaviour
{
    Transform caityln; // 케이틀린
    Animator animator;

    bool skillR = false; // r스킬 사용
    bool getTarget = false; // 범위 내 적이 존재하는가

    Vector3 targetPosition; // 타겟팅 위치 (적 위치)
    GameObject enemy; // 적

    GameObject rEffectPrefab; // 타겟팅 이펙트 프리팹
    GameObject rEffect; // 타겟팅 이펙트

    private void Start()
    {
        caityln = transform.parent;
        animator = caityln.GetComponent<Animator>();
        rEffectPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Solbin/SB_Prefabs/Attack_E.prefab", typeof(GameObject));
        rEffect = Instantiate(rEffectPrefab);
    }

    public void SkillR()
    {
        skillR = true;
    }

    // 적이 범위 내 있을 때 R키를 누른다면 

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && skillR)
        {
            enemy = other.gameObject;
            getTarget = true;

            targetPosition = enemy.transform.position;
        }
    }
}
