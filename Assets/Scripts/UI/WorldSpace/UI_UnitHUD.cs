using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Unit unit;

    private static readonly int floatSteps = Shader.PropertyToID(STEP);
    private static readonly int floatRatio = Shader.PropertyToID(RATIO);
    private static readonly int floatWidth = Shader.PropertyToID(WIDTH);
    private static readonly int floatThickness = Shader.PropertyToID(THICKNESS);

    private float hpShieldRatio;        // HP Shield
    private float rectWidth = 100f;
    private float thickness = 2f;

    private Collider col;

    #region ?怨몃땾

    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";

    #endregion

    #region ???뮞??癰궰??

    private float sp = 0f; // 쉴드
    private float speed = 3f;

    #endregion

    public override void Init()
    {
        Bind<Image>(typeof(Images));

        gameViewCamera = GameObject.Find("GameView Camera").GetComponent<Camera>();
        unit = transform.parent.GetComponent<Unit>();

        col = transform.parent.GetComponent<Collider>();

        CreateMaterial();
        //StartCoroutine(CoroutineTest());
    }

    /// <summary>
    /// ?癒?뵠???믩챸?싩뵳?堉???밴쉐 ??λ땾
    /// 繹먃沃섏눘苑?230911
    /// </summary>
    private void CreateMaterial()
    {
        GetImage((int)Images.Img_Separator).material = new Material(Shader.Find("MinSeob/UI/HUD"));
    }

    private void Update()
    {
        UpdateTransformHUD();
        UpdateValueHUD();
    }

    /// <summary>
    /// ?袁⑹삺 gameview 燁삳?李??깅퓠 筌띿쉸????袁⑺뒄?? 揶쏄낮猷꾤몴??④쑴沅??뺣뼄.
    /// 繹먃沃섏눘苑?230911
    /// </summary>
    private void UpdateTransformHUD()
    {
        transform.position = transform.parent.position + (Vector3.up * col.bounds.size.y) * 1.3f;
        transform.rotation = gameViewCamera.transform.rotation;
    }

    /// <summary>
    /// HUD 체력바 관리되는것은 이 함수에 다 있음
    /// 김민섭 _ 230911
    /// </summary>
    private void UpdateValueHUD()
    {
        if (unit.CurrentUnitStat.UnitStat.Hp < unit.CurrentUnitStat.Hp)
        {
            unit.CurrentUnitStat.UnitStat.SettingMaxHp(unit.CurrentUnitStat.Hp);
        }

        float step;

        // ??諭뜹첎? 鈺곕똻??????
        if (sp > 0)
        {
            if (unit.CurrentUnitStat.Hp + sp > unit.CurrentUnitStat.UnitStat.Hp)
            {   // ?袁⑹삺 筌ｋ???+ ??諭?> 筌ㅼ뮆? 筌ｋ???
                hpShieldRatio = unit.CurrentUnitStat.Hp / (unit.CurrentUnitStat.Hp + sp);
                GetImage((int)Images.Img_Mana).fillAmount = 1f;
                step = unit.CurrentUnitStat.Hp / 300f;
                GetImage((int)Images.Img_Hp).fillAmount = unit.CurrentUnitStat.Hp / (unit.CurrentUnitStat.Hp + sp);
            }
            else
            {
                GetImage((int)Images.Img_Mana).fillAmount = (unit.CurrentUnitStat.Hp + sp) / unit.CurrentUnitStat.UnitStat.Hp;
                hpShieldRatio = unit.CurrentUnitStat.Hp / unit.CurrentUnitStat.UnitStat.Hp;
                step = unit.CurrentUnitStat.Hp / 300f;
                GetImage((int)Images.Img_Hp).fillAmount = unit.CurrentUnitStat.Hp / unit.CurrentUnitStat.UnitStat.Hp;
            }
        }
        else
        {
            GetImage((int)Images.Img_Mana).fillAmount = 0f;
            step = unit.CurrentUnitStat.UnitStat.Hp / 300f; // 체력 칸 나누는 기준
            hpShieldRatio = 1f;
            GetImage((int)Images.Img_Hp).fillAmount = unit.CurrentUnitStat.Hp / unit.CurrentUnitStat.UnitStat.Hp;
        }

        GetImage((int)Images.Img_Damaged).fillAmount = Mathf.Lerp(GetImage((int)Images.Img_Damaged).fillAmount, GetImage((int)Images.Img_Hp).fillAmount, Time.deltaTime * speed);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatSteps, step); //floatSteps처럼 float붙어있는거 다 쉐이더 값 조절하는 변수
        GetImage((int)Images.Img_Separator).material.SetFloat(floatRatio, hpShieldRatio); //int가 Enum앞에 있으면 index번호대로 들어감
        GetImage((int)Images.Img_Separator).material.SetFloat(floatWidth, rectWidth);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatThickness, thickness);
    }

    #region ???뮞???꾨뗀諭?

    private IEnumerator CoroutineTest()
    {
        yield return new WaitForSeconds(2f);

        //hp = 1500;
        //maxHp = 1500;
        sp = 400;

        while (sp > 0)
        {
            sp -= (int)(280 * Time.deltaTime);
            yield return null;
        }

        sp = 0;

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 8; i++)
        {
            unit.CurrentUnitStat.OnDamaged(120f);
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < 8; i++)
        {
            unit.CurrentUnitStat.UnitStat.OnChangeMaxHp(200f);
            unit.CurrentUnitStat.SettingHp(unit.CurrentUnitStat.UnitStat.Hp);

            yield return new WaitForSeconds(1f);
        }

        //UnityEditor.EditorApplication.isPlaying = false;
    }

    #endregion
}
