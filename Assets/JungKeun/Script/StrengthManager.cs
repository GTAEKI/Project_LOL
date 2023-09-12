using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StrengthManager : MonoBehaviour
{
    //몇번째 증강인지?
    public int turn = 0;
    //선택할 카드 3개
    public GameObject strengthcanvas;

    public GameObject[] card;
    //남은 리롤횟수
    public GameObject RetryNumber;
    //증강 스프라이트이미지들
    public Sprite[] strength;
    //증강 이름들
    private string[] Name = { "사악한정신", "능수능란", "정신변환", "육중한힘", "야수화", "바늘에실끼우기", "되풀이", "치명적인공격", "천상의 신체", "극악무도", "속행", "신비한주먹" };
    //증강 설명들
    private string[] Detail = { "주문력100증가", "공격속도100%증가", "최대마나만큼체력이늘어납니다.", "공격력이15%오릅니다", "공격력이25오르고 스킬가속이 10 물리관통력이15오릅니다.", "마법관통력,방어구관통력이 25%증가합니다.", "스킬가속이 60늘어납니다", "치명타확률이 40%늘어납니다.", "체력이 1000늘어납니다.", "주문력이 15%늘어납니다.", "스킬가속의1.5배만큼 이동속도가 늘어납니다.", "공격속도가15%,공격력이100늘어납니다." };
    //증강 보관함(아래쪽인터페이스 증강)
    public GameObject[] StrengthCase;
    //증강 설명
    public GameObject[] explanation;
    //중복제거를 위한 카드번호List
    public List<int> cardNum = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //리롤횟수번호
    private int rerollchance = 0;
    //클릭한카드의 인덱스번호
    public int[] clickedcardnumber;

    public Button[] myButton;

    public void Start()
    {
        myButton[0].onClick.AddListener(() => clickCard(myButton[0].transform));
        myButton[1].onClick.AddListener(() => clickCard(myButton[1].transform));
        myButton[2].onClick.AddListener(() => clickCard(myButton[2].transform));
       


        // 스왑을 500번 돌려
        for (int i = 0; i < 500; i++)
        {
            Swap();
        }

        for (int a = 0; a < 3; a++)
        {
            for (int i = 0; i < strength.Length; i++)
            {
                card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a]];
                card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a]];
                card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a]];
            }

        }

        RetryNumber.GetComponent<Text>().text = "2";




    }
   


    public void reroll()
    {
        Debug.Log(rerollchance);
        if (turn == 0)
        {
            if (rerollchance == 0)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 3]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 3]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 3]];


                }
                RetryNumber.GetComponent<Text>().text = "1";
                rerollchance++;
            }

            else if (rerollchance == 1)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 6]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 6]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 6]];


                }
                RetryNumber.GetComponent<Text>().text = "0";
                rerollchance++;
            }
        }

        if (turn == 1)
        {
            if (rerollchance == 1)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 3]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 3]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 3]];


                }
                RetryNumber.GetComponent<Text>().text = "1";
                rerollchance++;
            }

            else if (rerollchance == 2)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 6]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 6]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 6]];


                }
                RetryNumber.GetComponent<Text>().text = "0";
                rerollchance++;
            }
        }
        else if (turn == 2)
        {
            if (rerollchance == 1)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 3]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 3]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 3]];


                }
                RetryNumber.GetComponent<Text>().text = "1";
                rerollchance++;
            }

            else if (rerollchance == 2)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 6]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 6]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 6]];


                }
                RetryNumber.GetComponent<Text>().text = "0";
                rerollchance++;
            }
        }
        else if (turn == 3)
        {
            if (rerollchance == 1)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 3]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 3]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 3]];


                }
                RetryNumber.GetComponent<Text>().text = "1";
                rerollchance++;
            }

            else if (rerollchance == 2)
            {
                for (int a = 0; a < 3; a++)
                {
                    card[a].transform.GetChild(0).GetComponent<Image>().sprite = strength[cardNum[a + 6]];
                    card[a].transform.GetChild(1).GetComponent<TMP_Text>().text = Name[cardNum[a + 6]];
                    card[a].transform.GetChild(2).GetComponent<TMP_Text>().text = Detail[cardNum[a + 6]];


                }
                RetryNumber.GetComponent<Text>().text = "0";
                rerollchance++;
            }
        }
        else { return; }
    }

    // 스왑으로 짜
    public void Swap()
    {
        int ranIndex1 = Random.Range(0, 10);
        int ranIndex2 = Random.Range(0, 10);

        int _temp = cardNum[ranIndex1];
        cardNum[ranIndex1] = cardNum[ranIndex2];
        cardNum[ranIndex2] = _temp;
    }

    public void clickCard(Transform clickedTransform)
    {
        GameObject clickedCard= GetComponent<GameObject>();
        //Debug.Log(clickedCard.transform.GetChild(1).GetComponent<TMP_Text>());

        if (turn == 0)
        {
            StrengthCase[0].gameObject.GetComponent<Image>().sprite = clickedTransform.transform.GetChild(0).GetComponent<Image>().sprite;
            explanation[0].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(1).GetComponent<TMP_Text>().text;
            explanation[0].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(2).GetComponent<TMP_Text>().text;
            strengthcanvas.gameObject.SetActive(false);
        }
        else if(turn ==1)
        {
            StrengthCase[1].gameObject.GetComponent<Image>().sprite = clickedTransform.transform.GetChild(0).GetComponent<Image>().sprite;
            explanation[1].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(1).GetComponent<TMP_Text>().text;
            explanation[1].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(2).GetComponent<TMP_Text>().text;
            strengthcanvas.gameObject.SetActive(false);

        }
        else if (turn == 2)
        {
            StrengthCase[2].gameObject.GetComponent<Image>().sprite = clickedTransform.transform.GetChild(0).GetComponent<Image>().sprite;
            explanation[2].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(1).GetComponent<TMP_Text>().text;
            explanation[2].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(2).GetComponent<TMP_Text>().text;
            strengthcanvas.gameObject.SetActive(false);

        }
        else if (turn == 3)
        {
            StrengthCase[3].gameObject.GetComponent<Image>().sprite = clickedTransform.transform.GetChild(0).GetComponent<Image>().sprite;
            explanation[3].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(1).GetComponent<TMP_Text>().text;
            explanation[3].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = clickedTransform.transform.GetChild(2).GetComponent<TMP_Text>().text;
            strengthcanvas.gameObject.SetActive(false);

        }

    }
}
