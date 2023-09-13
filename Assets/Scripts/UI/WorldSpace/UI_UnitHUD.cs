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
    private Unit unit;

    private static readonly int floatSteps = Shader.PropertyToID(STEP);
    private static readonly int floatRatio = Shader.PropertyToID(RATIO);
    private static readonly int floatWidth = Shader.PropertyToID(WIDTH);
    private static readonly int floatThickness = Shader.PropertyToID(THICKNESS);

    private float hpShieldRatio;        // ?ㅻ뱶 泥대젰 鍮꾩쑉
    private float rectWidth = 100f;
    private float thickness = 2f;

    #region ?곸닔

    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";

    #endregion

    #region ?뚯뒪??蹂??

    private float sp = 0f;
    private float speed = 3f;

    #endregion

    public override void Init()
    {
        Bind<Image>(typeof(Images));

        gameViewCamera = GameObject.Find("GameView Camera").GetComponent<Camera>();
        unit = transform.parent.GetComponent<Unit>();

        CreateMaterial();
    }

    /// <summary>
    /// ?먯씠??癒명떚由ъ뼹 ?앹꽦 ?⑥닔
    /// 源誘쇱꽠_230911
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
    /// ?꾩옱 gameview 移대찓?쇱뿉 留욎떠???꾩튂? 媛곷룄瑜?怨꾩궛?쒕떎.
    /// 源誘쇱꽠_230911
    /// </summary>
    private void UpdateTransformHUD()
    {
        transform.position = transform.parent.position + Vector3.up * 2f;
        transform.rotation = gameViewCamera.transform.rotation;
    }

    /// <summary>
    /// HUD ?섏튂 怨꾩궛 ?⑥닔
    /// 源誘쇱꽠_230911
    /// </summary>
    private void UpdateValueHUD()
    {
        if (unit.CurrentUnitStat.UnitStat.Hp < unit.CurrentUnitStat.Hp)
        {
            unit.CurrentUnitStat.UnitStat.SettingMaxHp(unit.CurrentUnitStat.Hp);
        }

        float step;

        // ?대뱶媛 議댁옱 ????
        if (sp > 0)
        {
            if (unit.CurrentUnitStat.Hp + sp > unit.CurrentUnitStat.UnitStat.Hp)
            {   // ?꾩옱 泥대젰 + ?대뱶 > 理쒕? 泥대젰
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
            step = unit.CurrentUnitStat.UnitStat.Hp / 300f;
            hpShieldRatio = 1f;
            GetImage((int)Images.Img_Hp).fillAmount = unit.CurrentUnitStat.Hp / unit.CurrentUnitStat.UnitStat.Hp;
        }

        GetImage((int)Images.Img_Damaged).fillAmount = Mathf.Lerp(GetImage((int)Images.Img_Damaged).fillAmount, GetImage((int)Images.Img_Hp).fillAmount, Time.deltaTime * speed);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatSteps, step);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatRatio, hpShieldRatio);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatWidth, rectWidth);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatThickness, thickness);
    }

    #region ?뚯뒪??肄붾뱶

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

        UnityEditor.EditorApplication.isPlaying = false;
    }

    #endregion
}
