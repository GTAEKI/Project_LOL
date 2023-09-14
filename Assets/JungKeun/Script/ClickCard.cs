using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickCard : MonoBehaviour
{
    public Sprite[] sprite;
    public GameObject[] strength;
    public TMP_Text[] Name;
    public TMP_Text[] Detail;
    private Sprite Image;
    private TMP_Text ChooseCardName;
    private TMP_Text ChooseCardDetail;
    StrengthManager strengthclass;

    private void Start()
    {

        Image = transform.GetChild(0).GetComponent<Image>().sprite;
        ChooseCardName = transform.GetChild(1).GetComponent<TMP_Text>();
        ChooseCardDetail = transform.GetChild(2).GetComponent<TMP_Text>();
    }

    void OnMouseDown()
    {
        if (strengthclass.turn == 0)
        {
            strength[0].gameObject.GetComponent<Image>().sprite = Image;
        }
        else if (strengthclass.turn == 1)
        {
            strength[1].gameObject.GetComponent<Image>().sprite = Image;
        }
        else if (strengthclass.turn == 2)
        {
            strength[2].gameObject.GetComponent<Image>().sprite = Image;
        }
        else if (strengthclass.turn == 3)
        {
            strength[3].gameObject.GetComponent<Image>().sprite = Image;
        }
    }

}