using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitHUD : UI_Base
{
    private enum Images
    {
        Img_Shield,
        Img_Damaged,
        Img_Hp,
        Img_Separator,
        Img_Mana
    }

    private Camera gameViewCamera;

    public override void Init()
    {
        Bind<Image>(typeof(Images));

        gameViewCamera = GameObject.Find("GameView Camera").GetComponent<Camera>();

        CreateMaterial();
        StartCoroutine(CoroutineTest());
    }

    /// <summary>
    /// 쉐이더 머티리얼 생성 함수
    /// 김민섭_230911
    /// </summary>
    private void CreateMaterial()
    {
        //if(GetImage((int)Images.Img_Separator).material == null)
        {
            GetImage((int)Images.Img_Separator).material = new Material(Shader.Find("MinSeob/UI/HUD"));
        }
    }

    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";

    private static readonly int floatSteps = Shader.PropertyToID(STEP);
    private static readonly int floatRatio = Shader.PropertyToID(RATIO);
    private static readonly int floatWidth = Shader.PropertyToID(WIDTH);
    private static readonly int floatThickness = Shader.PropertyToID(THICKNESS);

    [Range(0, 2800f)] public float hp = 1000f;
    [Range(0, 2800f)] public float maxHp = 1000f;
    [Range(0, 920f)] public float sp = 0f;
    [Range(0, 10f)] public float speed = 3f;

    public float hpShieldRatio;
    public float RectWidth = 100f;
    [Range(0, 5f)] public float Thickness = 2f;

    private IEnumerator CoroutineTest()
    {
        yield return new WaitForSeconds(2f);

        hp = 1500;
        maxHp = 1500;
        sp = 400;

        while(sp > 0)
        {
            sp -= (int)(280 * Time.deltaTime);
            yield return null;
        }

        sp = 0;

        yield return new WaitForSeconds(2f);

        for(int i = 0; i < 8; i++)
        {
            hp -= 120;
            yield return new WaitForSeconds(1f);
        }

        for(int i = 0; i < 8; i++)
        {
            maxHp += 200;
            hp = maxHp;

            yield return new WaitForSeconds(1f);
        }

        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void Update()
    {
        UpdateHUD();

        if(maxHp < hp)
        {
            maxHp = hp;
        }

        float step;

        // 쉴드가 존재 할 때
        if (sp > 0)
        {
            // 현재체력 + 쉴드 > 최대 체력
            if (hp + sp > maxHp)
            {
                hpShieldRatio = hp / (hp + sp);
                GetImage((int)Images.Img_Mana).fillAmount = 1f;
                step = (hp) / 300f;
                GetImage((int)Images.Img_Hp).fillAmount = hp / (hp + sp);
            }
            else
            {
                GetImage((int)Images.Img_Mana).fillAmount = (hp + sp) / maxHp;
                hpShieldRatio = hp / maxHp;
                step = hp / 300f;
                GetImage((int)Images.Img_Hp).fillAmount = hp / maxHp;
            }
        }
        else
        {
            GetImage((int)Images.Img_Mana).fillAmount = 0f;
            step = maxHp / 300f;
            hpShieldRatio = 1f;
            GetImage((int)Images.Img_Hp).fillAmount = hp / maxHp;
        }

        GetImage((int)Images.Img_Damaged).fillAmount = Mathf.Lerp(GetImage((int)Images.Img_Damaged).fillAmount,
            GetImage((int)Images.Img_Hp).fillAmount, Time.deltaTime * 5f);

        GetImage((int)Images.Img_Separator).material.SetFloat(floatSteps, step);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatRatio, hpShieldRatio);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatWidth, RectWidth);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatThickness, Thickness);
    }

    /// <summary>
    /// 현재 gameview 카메라에 맞춰서 위치와 각도를 계산한다.
    /// 김민섭_230911
    /// </summary>
    private void UpdateHUD()
    {
        transform.position = transform.parent.position + Vector3.up;
        transform.rotation = gameViewCamera.transform.rotation;
    }
}
