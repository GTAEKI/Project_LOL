using TMPro;
using UnityEngine.UI;

public class UI_UnitBottomLayer : UI_Scene
{
    private enum Images
    {
        Img_IconPassive,
        Img_IconActiveQ,Img_IconActiveW,Img_IconActiveE,Img_IconActiveR,
        Img_IconSpellD,Img_IconSpellF,
        Img_Hpbar, Img_Mpbar,
        Img_Portrait
    }

    private enum Texts
    {
        Text_HpBarexplanation, Text_MpBarexplanation
    }

    public enum GaugeType
    {
        Hp = 7, Mp
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    /// <summary>
    /// 게이지바 세팅 함수
    /// 김민섭_230915
    /// </summary>
    /// <param name="curr">현재값</param>
    /// <param name="max">최대값</param>
    /// <param name="recovery">회복량</param>
    public void SetGaugeBar(GaugeType type, float curr, float max, float recovery)
    {
        GetImage((int)type).fillAmount = curr / max;
        GetTMP((int)type - 7).text = $"{curr}/{max} (+{string.Format("{0:N1}", recovery)})";
    }
}
