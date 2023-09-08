using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class explanationManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    void Start()
    {
    }


    public void OnPointerEnter(PointerEventData eventData)

    {
        transform.GetChild(0).gameObject.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
