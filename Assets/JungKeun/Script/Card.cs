using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject[] strength;
    public GameObject RetryNumber;

    public Sprite[] strengthicon;

    // 기존 카드
    public List<int> cardNum = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    private int chance = 0;


    public void reroll()
    {
        if (chance == 0)
        {
            // 스왑을 500번 돌려
            for (int i = 0; i < 500; i++)
            {
                Swap();
            }

            for (int a = 0; a < 3; a++)
            {
                strength[a].GetComponent<Image>().sprite = strengthicon[cardNum[a]];
            }
            RetryNumber.GetComponent<Text>().text = "2";
            chance++;
        }

        else if (chance == 1)
        {
            for (int a = 0; a < 3; a++)
            {
                strength[a].GetComponent<Image>().sprite = strengthicon[cardNum[a + 3]];
            }
            RetryNumber.GetComponent<Text>().text = "1";
            chance++;
        }

        else if (chance == 2)
        {
            for (int a = 0; a < 3; a++)
            {
                strength[a].GetComponent<Image>().sprite = strengthicon[cardNum[a + 6]];
            }
            RetryNumber.GetComponent<Text>().text = "0";
            chance++;
        }

        else { return; }
    }

    public void Swap()
    {
        int ranIndex1 = Random.Range(0, 10);
        int ranIndex2 = Random.Range(0, 10);

        int _temp = cardNum[ranIndex1];
        cardNum[ranIndex1] = cardNum[ranIndex2];
        cardNum[ranIndex2] = _temp;
    }
}
