using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject[] card;
    

    public Sprite[] strengthcard;



    public void reroll()
    {
        for(int a = 0; a<3; a++)
        {
            int RandomNumber = Random.Range(0, 5);
            card[a].GetComponent<Image>().sprite = strengthcard[RandomNumber];
        }
    }
}
