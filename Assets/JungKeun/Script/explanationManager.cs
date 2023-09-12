using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class explanationManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public GameObject explanation;

    void Start()
    { 

    }


    public void OnPointerEnter(PointerEventData eventData)

    {
        explanation.gameObject.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        explanation.gameObject.SetActive(false);
    }
}
