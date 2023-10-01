using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


/// <summary>
/// 유도 화살, 기본 공격시 화살이 상대를 따라감
/// 230923 _ 배경택
/// </summary>
public class GuidedArrow : MonoBehaviourPun
{
    private GameObject enemy;
    public int speed = 10;
    public int actorNumber;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            int playerActorNumber = player.GetComponent<PhotonView>().Owner.ActorNumber;
            if (playerActorNumber == actorNumber)
            {
                enemy = player;
                break;
            }
        }
    }

    void Update()
    {
        if(enemy != null)
        {
            transform.position = Vector3.Lerp(transform.position, enemy.transform.position, speed * Time.deltaTime); 
        }
    }
}
