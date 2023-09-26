using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_CaitylnHitAA : MonoBehaviour
{
    GameObject[] playerList;
    int playerCount;

    GameObject target;

    private void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        playerCount = playerList.Length;

        for (int i = 0; i < playerCount; i++)
        {
            transform.GetComponent<ParticleSystem>().trigger.SetCollider(i, playerList[i].transform.GetComponent<Collider>());
        }
    }

    public void GetPlayerName(GameObject _player) // 공격 대상 판별 (HP-)
    {
        target = _player;
    }

    private void OnParticleTrigger()
    {
        target.GetComponent<Unit>().CurrentUnitStat.OnDamaged(6f);
    }
}
