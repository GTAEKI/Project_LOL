using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class SB_GragasR : MonoBehaviour
{
    public GameObject barrelPrefab;
    GameObject barrel;
    Rigidbody rb;
    Camera camera;

    bool orbitJump = false;
    bool orbitForward = false;

    Vector3 targetPosition; // 마우스 위치 
    Animator animator;

    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        barrel = Instantiate(barrelPrefab);
        barrel.transform.position = new Vector3(0, 0, -10);
        rb = barrel.transform.GetComponent<Rigidbody>();
        camera = GameObject.Find("GameView Camera").GetComponent<Camera>();

        animator = transform.GetComponent<Animator>();

        pv = transform.GetComponent<PhotonView>();
    }

    public void SkillR()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Floor");

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (!SB_GragasMoving.gragasSkill)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                targetPosition = hit.point;// 폭탄의 목적지, 플레이어가 바라봄
                transform.LookAt(targetPosition);
                StartCoroutine(ThrowBomb());

                barrel.transform.position = transform.position + transform.forward * 3f + transform.up * 3f;
                orbitJump = true;
                orbitForward = true;
            }
        }
    }

    /// <summary>
    /// 폭탄 던지기 애니메이션
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThrowBomb()
    {
        animator.SetTrigger("PressR");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        pv.RPC("AnimationRSync", RpcTarget.All);
    }

    [PunRPC]
    private void AnimationRSync()
    {
        animator.SetTrigger("Back Idle");
    }

    private void FixedUpdate()
    {
        if (orbitJump) // 폭탄 위로 상승
        {
            orbitJump = false;
            rb.useGravity = true;
            rb.AddForce(Vector3.up * 23f, ForceMode.Impulse);
        }

        if (orbitForward) // 폭탄 앞으로 이동
        {
            barrel.transform.position = Vector3.MoveTowards(barrel.transform.position, targetPosition, Time.deltaTime * 25f);

            if (Vector3.Distance(barrel.transform.position, targetPosition) <= 0.1f)
            {
                orbitForward = false;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;

                barrel.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = false;
                barrel.transform.GetChild(2).gameObject.SetActive(true);
                Invoke("ReturnPool", 3f);
            }
        }
    }

    private void ReturnPool() 
    {
        barrel.transform.position = new Vector3(0, 0, -10);
        barrel.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
    }

}
