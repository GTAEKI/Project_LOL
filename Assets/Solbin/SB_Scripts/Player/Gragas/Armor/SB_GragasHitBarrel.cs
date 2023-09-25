using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_GragasHitBarrel : MonoBehaviour
{
    bool effectQ = false;
    Vector3 targetPos;
    GameObject barrel;

    // Start is called before the first frame update
    void Start()
    {
        barrel = GameObject.Find("Gragas").transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectQ()
    {
        Debug.Log(barrel.transform.position);
        targetPos = barrel.transform.position;
        targetPos.y = 1.5f;
        transform.position = targetPos;

        StartCoroutine(RotateQ());
    }

    private IEnumerator RotateQ()
    {
        float time = 0;

        while (time < 2)
        {
            time += Time.deltaTime;

            transform.Rotate(Vector3.up * Time.deltaTime * 3f);
        }

        yield return null;
    }

    public void EffectR()
    {
        Debug.Log("R효과 출력 예정");
    }
}
