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

    public override void Init()
    {
        Bind<Image>(typeof(Images));

        gameViewCamera = GameObject.Find("GameView Camera").GetComponent<Camera>();
        unit = transform.parent.GetComponent<Unit>();

        CreateMaterial();
        StartCoroutine(CoroutineTest());
    }

    /// <summary>
    /// 쉐이더 머티리얼 생성 함수
    /// 김민섭_230911
    /// </summary>
    private void CreateMaterial()
    {
        GetImage((int)Images.Img_Separator).material = new Material(Shader.Find("MinSeob/UI/HUD"));
    }

    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";

    private static readonly int floatSteps = Shader.PropertyToID(STEP);
    private static readonly int floatRatio = Shader.PropertyToID(RATIO);
    private static readonly int floatWidth = Shader.PropertyToID(WIDTH);
    private static readonly int floatThickness = Shader.PropertyToID(THICKNESS);

    //[Range(0, 2800f)] public float hp = 1000f;
    //[Range(0, 2800f)] public float maxHp = 1000f;
    [Range(0, 920f)] public float sp = 0f;
    [Range(0, 10f)] public float speed = 3f;

    public float hpShieldRatio;
    public float RectWidth = 100f;
    [Range(0, 5f)] public float Thickness = 2f;

    private IEnumerator CoroutineTest()
    {
        yield return new WaitForSeconds(2f);

        //hp = 1500;
        //maxHp = 1500;
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
            unit.CurrentUnitStat.OnDamaged(120f);
            yield return new WaitForSeconds(1f);
        }

        for(int i = 0; i < 8; i++)
        {
            unit.CurrentUnitStat.UnitStat.OnChangeMaxHp(200f);
            unit.CurrentUnitStat.SettingHp(unit.CurrentUnitStat.UnitStat.Hp);

            yield return new WaitForSeconds(1f);
        }

        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void Update()
    {
        UpdateHUD();

        if(unit.CurrentUnitStat.UnitStat.Hp < unit.CurrentUnitStat.Hp)
        {
            unit.CurrentUnitStat.UnitStat.SettingMaxHp(unit.CurrentUnitStat.Hp);
        }

        float step;

        // 쉴드가 존재 할 때
        if (sp > 0)
        {
            if(unit.CurrentUnitStat.Hp + sp > unit.CurrentUnitStat.UnitStat.Hp)
            {   // 현재 체력 + 쉴드 > 최대 체력
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

        GetImage((int)Images.Img_Damaged).fillAmount = Mathf.Lerp(GetImage((int)Images.Img_Damaged).fillAmount,
            GetImage((int)Images.Img_Hp).fillAmount, Time.deltaTime * speed);

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
