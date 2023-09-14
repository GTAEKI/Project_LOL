using UnityEngine;
using UnityEngine.UI;

public class StatFrontButtonManager : MonoBehaviour
{
    public GameObject mainstatsystem;
    public GameObject otherstatsystem;
    public GameObject strengthstatsystem;

    public RectTransform maintransform;
    public RectTransform othertransform;
    public RectTransform strengthtransform;

    public Button mainstatbutton;
    public Button otherstatbutton;
    public Button strengthstatbutton;

    private bool Nochat = false;
    private bool MainOn = false;
    private bool OtherOn = false;
    private bool StrengthOn = false;

    private void Update()
    {
        if (MainOn == true)
        {
            if (Nochat == false && Input.GetKeyDown(KeyCode.C))
            {//x는 140 y는 100
                mainstatsystem.gameObject.SetActive(true);
                otherstatsystem.gameObject.SetActive(true);
                strengthstatsystem.gameObject.SetActive(true);

                othertransform.localPosition += Vector3.up * 100;
                strengthtransform.localPosition += Vector3.right * -140;
            }
            if (Nochat == false && Input.GetKeyUp(KeyCode.C))
            {//x는 140 y는 100
                mainstatsystem.gameObject.SetActive(true);
                otherstatsystem.gameObject.SetActive(false);
                strengthstatsystem.gameObject.SetActive(false);

                othertransform.localPosition += Vector3.up * -100;
                strengthtransform.localPosition += Vector3.right * 140;
            }

        }
        else if (OtherOn == true)
        {
            if (Nochat == false && Input.GetKeyDown(KeyCode.C))
            {//x는 140 y는 100
                mainstatsystem.gameObject.SetActive(true);
                otherstatsystem.gameObject.SetActive(true);
                strengthstatsystem.gameObject.SetActive(true);

                othertransform.localPosition += Vector3.up * 100;
                strengthtransform.localPosition += Vector3.right * -140;
            }
            if (Nochat == false && Input.GetKeyUp(KeyCode.C))
            {//x는 140 y는 100
                mainstatsystem.gameObject.SetActive(false);
                otherstatsystem.gameObject.SetActive(true);
                strengthstatsystem.gameObject.SetActive(false);

                othertransform.localPosition += Vector3.up * 100;
                strengthtransform.localPosition += Vector3.right * maintransform.localPosition.x;
            }
        }
        else if (StrengthOn == true)
        {
            if (Nochat == false && Input.GetKeyDown(KeyCode.C))
            {//x는 140 y는 100
                mainstatsystem.gameObject.SetActive(true);
                otherstatsystem.gameObject.SetActive(true);
                strengthstatsystem.gameObject.SetActive(true);

                othertransform.localPosition += Vector3.up * 100;
                strengthtransform.localPosition += Vector3.right * -maintransform.localPosition.x;
            }
            if (Nochat == false && Input.GetKeyUp(KeyCode.C))
            {//x는 140 y는 100
                mainstatsystem.gameObject.SetActive(false);
                otherstatsystem.gameObject.SetActive(false);
                strengthstatsystem.gameObject.SetActive(true);

                othertransform.localPosition += Vector3.up * -100;
                strengthtransform.localPosition += Vector3.right * strengthtransform.localPosition.x;
            }
        }
    }


    public void firstbutton()
    {
        if (MainOn == true)
        {
            mainstatsystem.gameObject.SetActive(false);
            otherstatsystem.gameObject.SetActive(false);
            strengthstatsystem.gameObject.SetActive(false);


            MainOn = false;
            OtherOn = false;
            StrengthOn = false;

        }
        else if (MainOn == false)
        {
            mainstatsystem.gameObject.SetActive(true);
            otherstatsystem.gameObject.SetActive(false);
            strengthstatsystem.gameObject.SetActive(false);


            MainOn = true;
            OtherOn = false;
            StrengthOn = false;
        }
    }

    public void Secondbutton()
    {
        if (OtherOn == true)
        {
            mainstatsystem.gameObject.SetActive(false);
            otherstatsystem.gameObject.SetActive(false);
            strengthstatsystem.gameObject.SetActive(false);


            MainOn = false;
            OtherOn = false;
            StrengthOn = false;

        }
        else if (OtherOn == false)
        {
            mainstatsystem.gameObject.SetActive(false);
            otherstatsystem.gameObject.SetActive(true);
            strengthstatsystem.gameObject.SetActive(false);


            MainOn = false;
            OtherOn = true;
            StrengthOn = false;
        }
    }

    public void Thirdbutton()
    {
        if (StrengthOn == true)
        {
            mainstatsystem.gameObject.SetActive(false);
            otherstatsystem.gameObject.SetActive(false);
            strengthstatsystem.gameObject.SetActive(false);


            MainOn = false;
            OtherOn = false;
            StrengthOn = false;

        }
        else if(StrengthOn ==false)
        {
            mainstatsystem.gameObject.SetActive(false);
            otherstatsystem.gameObject.SetActive(false);
            strengthstatsystem.gameObject.SetActive(true);


            MainOn = false;
            OtherOn = false;
            StrengthOn = true;
        }
    }


}
