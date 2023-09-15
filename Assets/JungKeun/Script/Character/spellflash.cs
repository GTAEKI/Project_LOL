using UnityEngine;

public class spellflash : MonoBehaviour
{
    public float maxteleportDistance = 10f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Flash();
        }
    }


    void Flash()
    {
        Vector3 mousePosition = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {

            Vector3 newPosition = hit.point;
            newPosition.y = transform.position.y;
            transform.LookAt(newPosition);
            float distanceToMouse = Vector3.Distance(transform.position, newPosition);

            if (distanceToMouse > maxteleportDistance)
            {
                
                newPosition = transform.position + transform.forward * maxteleportDistance;


            }
            transform.position = newPosition;
        }
    }
}
