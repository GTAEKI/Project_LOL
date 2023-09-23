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

    #region Shader Properties

    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";

    #endregion

    #region Shield Variables

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
    /// HUD의 머티리얼을 생성합니다.
    /// 김민섭 _ 230911
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
    /// GameView를 기준으로 HUD의 위치와 회전을 업데이트합니다.
    /// 김민섭 _ 230911
    /// </summary>
    private void UpdateTransformHUD()
    {
        transform.position = transform.parent.position + (Vector3.up * col.bounds.size.y) * 1.3f;
        transform.rotation = gameViewCamera.transform.rotation;
    }

    /// <summary>
    /// HUD 체력바와 관련된 값을 업데이트합니다.
    /// 김민섭 _ 230911
    /// </summary>
    private void UpdateValueHUD()
    {
        if (unit.CurrentUnitStat.UnitStat.Hp < unit.CurrentUnitStat.Hp)
        {
            unit.CurrentUnitStat.UnitStat.SettingMaxHp(unit.CurrentUnitStat.Hp);
        }

        float step;

        // 쉴드가 있을 경우 처리
        if (sp > 0)
        {
            if (unit.CurrentUnitStat.Hp + sp > unit.CurrentUnitStat.UnitStat.Hp)
            {   // 현재 체력 + 쉴드 > 최대 체력일 경우
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
        GetImage((int)Images.Img_Separator).material.SetFloat(floatSteps, step); // floatSteps처럼 float 변수에 쉐이더 값을 조절하는 변수
        GetImage((int)Images.Img_Separator).material.SetFloat(floatRatio, hpShieldRatio); // int가 Enum 앞에 있으면 index 번호대로 들어감
        GetImage((int)Images.Img_Separator).material.SetFloat(floatWidth, rectWidth);
        GetImage((int)Images.Img_Separator).material.SetFloat(floatThickness, thickness);
    }

    #region Coroutine Test

    private IEnumerator CoroutineTest()
    {
        yield return new WaitForSeconds(2f);

        // hp = 1500;
        // maxHp = 1500;
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

        // UnityEditor.EditorApplication.isPlaying = false;
    }

    #endregion
}
