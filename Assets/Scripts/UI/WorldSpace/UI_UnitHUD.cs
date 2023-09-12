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

    private float hpShieldRatio;        // �ǵ� ü�� ����
    private float rectWidth = 100f;
    private float thickness = 2f;

    #region ���

    private const string STEP = "_Steps";
    private const string RATIO = "_HSRatio";
    private const string WIDTH = "_Width";
    private const string THICKNESS = "_Thickness";

    #endregion

    #region �׽�Ʈ ����

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
    /// ���̴� ��Ƽ���� ���� �Լ�
    /// ��μ�_230911
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
    /// ���� gameview ī�޶� ���缭 ��ġ�� ������ ����Ѵ�.
    /// ��μ�_230911
    /// </summary>
    private void UpdateTransformHUD()
    {
        transform.position = transform.parent.position + Vector3.up;
        transform.rotation = gameViewCamera.transform.rotation;
    }

    /// <summary>
    /// HUD ��ġ ��� �Լ�
    /// ��μ�_230911
    /// </summary>
    private void UpdateValueHUD()
    {
        if (unit.CurrentUnitStat.UnitStat.Hp < unit.CurrentUnitStat.Hp)
        {
            unit.CurrentUnitStat.UnitStat.SettingMaxHp(unit.CurrentUnitStat.Hp);
        }

        float step;

        // ���尡 ���� �� ��
        if (sp > 0)
        {
            if (unit.CurrentUnitStat.Hp + sp > unit.CurrentUnitStat.UnitStat.Hp)
            {   // ���� ü�� + ���� > �ִ� ü��
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

    #region �׽�Ʈ �ڵ�

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
