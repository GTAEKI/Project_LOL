using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TornadoController : MonoBehaviour
{
    public Vector3 moveDirect = Vector3.zero;

    public List<GameObject> HitTargets { private set; get; } = new List<GameObject>();    // 피해를 받은 적

    private void Update()
    {
        transform.position += moveDirect * 60f * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dummy")
        {
            if (!HitTargets.Find(x => x.gameObject == other.gameObject))
            {
                HitTargets.Add(other.gameObject);

                CurrentUnitStat targetStat = other.GetComponent<Unit>().CurrentUnitStat;
                CurrentUnitStat myStat = GameObject.Find("Yasuo_Photon").GetComponent<Unit>().CurrentUnitStat;

                // 20 + (1.05 물리계수)

                float atk = myStat.Atk * 1.05f;
                float totalDamage = 20 + atk;

                targetStat?.OnDamaged(totalDamage);

                // 공중에 띄움
                other.GetComponent<Rigidbody>().AddForce(Vector3.up * 0.3f, ForceMode.Impulse);

                StartCoroutine(AddAirbone(other.transform));
            }
        }
    }

    private IEnumerator AddAirbone(Transform target)
    {
        target.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        target.GetComponent<Rigidbody>().velocity = -Vector3.one * 3f;
    }
}
