using UnityEngine;
using UnityEngine.UI;

public class SetStatUIManager : MonoBehaviour
{
    public Text atk;
    public Text def;
    public Text atkspeed;
    public Text criticalchance;
    public Text ap;
    public Text mdef;
    public Text SkillBoost;
    public Text movement;
    public Text hpRegen;
    public Text armorPenetration;
    public Text Vamp;
    public Text AttackRange;
    public Text mpRegen;
    public Text magicPenetration;
    public Text allVamp;
    public Text tenacity;
    public Text FirstStrengthexplanation;
    public Text SecondStrengthexplanation;
    public Text ThirdStrengthexplanation;
    public Text FourthStrengthexplanation;



    void Update()
    {



        atk.text = string.Format("현재 공격력 : {0} (기본 {1} + 추가 {2})\r\n기본 공격 시  {3} 의 물리 피해를 입힙니다."
            , DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level)
            , DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level)
            , DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level)
            , DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level)
            );
        def.text = string.Format("현재 방어력: {0}(기본 {1} + 추가 {2})\r\n물리 피해를 {3}%만큼 덜 받습니다."
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );

        atkspeed.text = string.Format("추가 공격 속도: {0} %\r\n현재 초당 공격 횟수: {1}\r\n공격 속도 계수 : {2}"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        criticalchance.text = string.Format("각 기본 공격으로 75%의 추가 피해를 입힐 확률을 부여합니다.\r\n\r\n현재 치명타율:{0}%"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        ap.text = string.Format("현재 주문력 : 0"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        mdef.text = string.Format("현재 마법 저항력: {0} (기본 {1} + 추가 {2})\r\n마법 피해를 {3}% 만큼 덜 받습니다."
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        SkillBoost.text = string.Format("현재 스킬 가속: {0}\r\n스킬 재사용 대기시간이 {1}% 감소하는 효과와 동일합니다."
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        movement.text = string.Format("현재 이동 속도 : 초당 {0}(기본 {1} + 추가 {2})"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        hpRegen.text = string.Format("현재 체력 재생: {0} (기본 {1} + 추가 {2})"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        armorPenetration.text = string.Format("현재 물리 관통력 | 방어구 관통력: {0} | {1}%"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        Vamp.text = string.Format("현재 생명력 흡수량: {0}%"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        AttackRange.text = string.Format("현재 공격 사거리: {0} ( 기본 {1} + 추가 {2})"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        mpRegen.text = string.Format("현재 자원 재생: {0} ( 기본 {1} + 추가 {2} )"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        magicPenetration.text = string.Format("현재 마법 관통력 : {0} | {1}%"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        allVamp.text = string.Format("물리 피해 흡혈 | 모든 피해 흡혈 : {0}% |  {1}%"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        tenacity.text = string.Format("현재 강인함: {0}%"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        FirstStrengthexplanation.text = string.Format("첫번째 증강:{0}"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        SecondStrengthexplanation.text = string.Format("두번째 증강:{0}"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        ThirdStrengthexplanation.text = string.Format("세번째 증강:{0}"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
        FourthStrengthexplanation.text = string.Format("네번째 증강:{0}"
          , DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
          );
       


    }







    /*
    //{0}=
    (DataManger.unitStat.Atk + (DataManger.unitStat.Growthatk * DataManger.unitStat.Level))
    /*+아이템 공격력 + 증강 공격력*/
    /*퍼센트공격력강화 x
        (DataManger.unitStat.Atk + (DataManger.unitStat.Growthatk * DataManger.unitStat.Level))*/


    //{1} =
    //DataManger.unitStat.Atk + (DataManger.unitStat.Growthatk * DataManger.unitStat.Level),
    //지워야함

    //{2} =
    /*+아이템 공격력 + 증강 공격력*/
    /*퍼센트공격력강화 x
        (DataManger.unitStat.Atk + (DataManger.unitStat.Growthatk * DataManger.unitStat.Level))*/

    //{3} =

    //(DataManger.unitStat.Atk + (DataManger.unitStat.Growthatk * DataManger.unitStat.Level))
    /*+아이템 공격력 + 증강 공격력*/
    /*퍼센트공격력강화 x
        (DataManger.unitStat.Atk + (DataManger.unitStat.Growthatk * DataManger.unitStat.Level))*/


}
