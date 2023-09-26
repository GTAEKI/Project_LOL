using UnityEngine;
using UnityEngine.UI;


public class SetStatUIManager : MonoBehaviour
{
    public GameObject MainStatSystem;
    public GameObject otherStatSystem;
    public GameObject strengthStatSystem;
    public GameObject[] strengthes;
    public Text atk;
    public Text atkSTAT;
    public Text def;
    public Text defSTAT;
    public Text atkspeed;
    public Text atkspeedSTAT;
    public Text criticalchance;
    public Text criticalchanceSTAT;
    public Text ap;
    public Text apSTAT;
    public Text mdef;
    public Text mdefSTAT;
    public Text SkillBoost;
    public Text SkillBoostSTAT;
    public Text movement;
    public Text movementSTAT;
    public Text hpRegen;
    public Text hpRegenSTAT;
    public Text armorPenetration;
    public Text armorPenetrationSTAT;
    public Text Vamp;
    public Text VampSTAT;
    public Text AttackRange;
    public Text AttackRangeSTAT;
    public Text mpRegen;
    public Text mpRegenSTAT;
    public Text magicPenetration;
    public Text magicPenetrationSTAT;
    public Text allVamp;
    public Text allVampSTAT;
    public Text tenacity;
    public Text tenacitySTAT;
    public GameObject FirstStrengthexplanation;
    public GameObject SecondStrengthexplanation;
    public GameObject ThirdStrengthexplanation;
    public GameObject FourthStrengthexplanation;

    SB_GatherStatus itemStatus;
    Strength strengthStatus;


    private void Start()
    {

        //itemStatus = GameObject.Find("Inventory").GetComponent<SB_GatherStatus>();

    }



    void Update()
    {
        if (MainStatSystem.activeSelf)
        {
            //1.

            //스텟창에 공격력수치
            //{0} 공격력수치 = (기본공격력 + 성장공격력*Level) + 아이템공격력 + 증강공격력
            atkSTAT.text = string.Format("{0}", DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level)
                                              + itemStatus.attackDamege  /*+strengthStatus.atk*/);
            //공격력 설명창
            atk.text = string.Format("현재 공격력 : {0} (기본 {1} + 추가 {2})\r\n기본 공격 시  {3} 의 물리 피해를 입힙니다."
                ,//{0} 총 공격력 = (기본공격력 + 성장공격력*Level) + 아이템공격력 + 증강공격력
                DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level) + itemStatus.attackDamege //+ strengthStatus.atk
                ,//{1} 기본공격력 = (기본공격력 + 성장공격력*Level)
                DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level)
                ,//{2} 추가공격력 = 아이템공격력 + 증강공격력
                itemStatus.attackDamege //+ strengthStatus.atk
                ,//{3} 물리피해량 = 총공격력
                DataManger.unitStatDictionary[1].Atk + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level) + itemStatus.attackDamege //+ strengthStatus.atk
                );

            //2.
            //스텟창에 방어력수치
            //{0} 방어력수치 = (기본방어력 + 성장방어력*Level + 아이템방어력 + 증강방어력)
            defSTAT.text = string.Format("{0}", DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                                              + itemStatus.armor /*+strengthStatus.def*/);
            //방어력 설명창
            if (DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                                   + itemStatus.armor/*+ strengthStatus.atk*/ > 0)
            {
                def.text = string.Format("현재 방어력: {0}(기본 {1} + 추가 {2})\r\n물리 피해를 {3}%만큼 덜 받습니다."
                    ,//{0} 총 방어력 = (기본방어력 + 성장방어력*Level + 아이템방어력 + 증강방어력) 
                                         DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                                       + itemStatus.armor//+ strengthStatus.atk
                    ,//{1} 기본방어력 = (기본공격력 + 성장공격력*Level)
                                         DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                    ,//{2} 추가방어력 = 아이템방어력 + 증강방어력
                                         itemStatus.armor//+ strengthStatus.atk
                    ,//{3} 물리피해 감소량 = %%방어력이 0보다 높으면%%  100-(10000/(100+(기본방어력 + 성장방어력*Level + 아이템방어력 + 증강방어력))
                                      
                    (int)(100-(10000/(100 +((DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                                   + itemStatus.armor/*+ strengthStatus.atk*/)))))
                    );

            }
            else if(DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                                   + itemStatus.armor/*+ strengthStatus.atk*/ < 0)
            {
                def.text = string.Format("현재 방어력: {0}(기본 {1} + 추가 {2})\r\n물리 피해를 {3}%만큼 덜 받습니다."
                   ,//{0} 총 방어력 = (기본방어력 + 성장방어력*Level + 아이템방어력 + 증강방어력) 
                                        DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                                      + itemStatus.armor//+ strengthStatus.atk
                   ,//{1} 기본방어력 = (기본공격력 + 성장공격력*Level)
                                        DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                   ,//{2} 추가방어력 = 아이템방어력 + 증강방어력
                                        itemStatus.armor//+ strengthStatus.atk
                   ,/*{3} 물리피해 감소량 = %%방어력이 0보다 낮으면%%  100-(2-(10000/(100+(기본방어력 + 성장방어력*Level + 아이템방어력 + 증강방어력)))))
                */
                
                   (int)(100-(2-(10000 / (100 + DataManger.unitStatDictionary[1].Defence + (DataManger.unitStatDictionary[1].Growthdefence * DataManger.unitStatDictionary[1].Level)
                                   + itemStatus.armor/*+ strengthStatus.armor*/))))
                   );

            }

            //3.
            //스텟창 공격속도수치
            //{0} 공격속도 수치 = 기본공격속도 +  (기본공격속도 * ((성장공격속도*Level) + 아이템 공격속도 + 증강 공격속도)
            atkspeedSTAT.text = string.Format("{0}", DataManger.unitStatDictionary[1].AtkSpeed
                                                   + (DataManger.unitStatDictionary[1].AtkSpeed * 0.01 *((DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level) 
                                                   + itemStatus.attackSpeed
                                                   /*+ strengthStatus.attackSpeed */
                                                   )));
            //+ DataManger.unitStatDictionary[1].AtkSpeed + (DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level) * strengthStatus.attackDamege);
            //공격속도 설명창
            atkspeed.text = string.Format("추가 공격 속도: {0:0.##} %\r\n현재 초당 공격 횟수: {1:##}\r\n공격 속도 계수 : {2}"

              ,//{0} 추가공격속도:(기본공격속도 * ((성장공격속도*Level) + 아이템 공격속도 + 증강 공격속도)
              (DataManger.unitStatDictionary[1].AtkSpeed * ((DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level) + itemStatus.attackSpeed/*+ strengthStatus.attackSpeed */))
              ,//{1} 현재 초당 공격 횟수 : 기본공격속도 +  (기본공격속도 * ((성장공격속도*Level) + 아이템 공격속도 + 증강 공격속도)

               DataManger.unitStatDictionary[1].AtkSpeed + (DataManger.unitStatDictionary[1].AtkSpeed * 0.01 * ((DataManger.unitStatDictionary[1].Growthatk * DataManger.unitStatDictionary[1].Level)
                                                   + itemStatus.attackSpeed/*+ strengthStatus.attackSpeed */))
              ,//{2} 공격속도 계수 : 기본 공격속도
              DataManger.unitStatDictionary[1].AtkSpeed
              );

            //4.
            //스텟창크리티컬확률 수치
            //{0} 크리티컬 수치 : (기본 크리티컬확률 +아이템 크리티컬확률 +증강 크리티컬 확률)
            //100을 넘으면 100으로 고정
            if (DataManger.unitStatDictionary[1].CriticalPer + itemStatus.criticalStrikeChance/*+strengthStatus.criticalchance*/< 100)
            {
                criticalchanceSTAT.text = string.Format("{0}", DataManger.unitStatDictionary[1].CriticalPer + itemStatus.criticalStrikeChance/*+strengthStatus.criticalchance*/);
            }
            else if (DataManger.unitStatDictionary[1].CriticalPer + itemStatus.criticalStrikeChance/*+strengthStatus.criticalchance*/ >= 100)
            {
                criticalchanceSTAT.text = string.Format("{0}", 100);
            }

            //크리티컬확률 설명창
            if (DataManger.unitStatDictionary[1].CriticalPer + itemStatus.criticalStrikeChance/*+strengthStatus.criticalchance*/< 100)
            {
                criticalchance.text = string.Format("각 기본 공격으로 75%의 추가 피해를 입힐 확률을 부여합니다.\r\n\r\n현재 치명타율:{0}%"
              ,//{0} 치명타율 : (기본 크리티컬확률 +아이템 크리티컬확률 +증강 크리티컬 확률)
                DataManger.unitStatDictionary[1].CriticalPer + itemStatus.criticalStrikeChance //+strengthStatus.CriticalChance
              );
            }
            else if (DataManger.unitStatDictionary[1].CriticalPer + itemStatus.criticalStrikeChance/*+strengthStatus.criticalchance*/ >= 100)
            {
                criticalchance.text = string.Format("각 기본 공격으로 75%의 추가 피해를 입힐 확률을 부여합니다.\r\n\r\n현재 치명타율:{0}%"
               ,//{0} 치명타율 : (기본 크리티컬확률 +아이템 크리티컬확률 +증강 크리티컬 확률)
                 100);
            }

            //5.
            //스텟창 주문력 수치
            //{0} 주문력 수치 : 아이템주문력 + 증강주문력 
            apSTAT.text = string.Format("{0}", itemStatus.abilityPower /*+strengthStatus.apk */);

            //주문력 설명창
            ap.text = string.Format("현재 주문력 : {0}"
              ,//{0} 주문력 : 아이템주문력 + 증강주문력 + (아이템주문력+증강주문력) * 증강 주문력 퍼센트
              itemStatus.abilityPower /* +strength.ap + (itemStatus.abilityPower + strengthStatus.ap)*strengthStatus.apper*/);

            //6.
            //스텟창 마법저항력 수치
            //{0} 마법저항력 수치 : 기본 마법저항력 +성장마법저항력 + 아이템마법저항력
            mdefSTAT.text = string.Format("{0}"
              , DataManger.unitStatDictionary[1].MDefence + DataManger.unitStatDictionary[1].GrowthmDefence + itemStatus.magicResistance);

            //마법저항력이 0보다 크면
            if (DataManger.unitStatDictionary[1].MDefence + DataManger.unitStatDictionary[1].GrowthmDefence + itemStatus.magicResistance > 0)
            {

                //마법저항력 설명창
                mdef.text = string.Format("현재 마법 저항력: {0} (기본 {1} + 추가 {2})\r\n마법 피해를 {3}% 만큼 덜 받습니다."
                ,//{0} 현재 총마법 저항력:기본 마법저항력 +성장마법저항력 + 아이템마법저항력 
                DataManger.unitStatDictionary[1].MDefence + DataManger.unitStatDictionary[1].GrowthmDefence + itemStatus.magicResistance

                ,//{1} 기본 마법 저항력: 기본 마법저항력 
                DataManger.unitStatDictionary[1].MDefence
                ,//{2} 추가 마법 저항력: 아이템마법저항력 +  (성장마법저항력 * 레벨) 
                itemStatus.magicResistance + (DataManger.unitStatDictionary[1].GrowthmDefence * DataManger.unitStatDictionary[1].Level)
                ,/*{3} 마법피해 감소량: %%마법저항력이 0보다 높으면%% 100 -(10000/(100+(기본마법저항력 + 성장마법저항력*Level + 아이템마법저항력)*/

                (int)(100 - (10000/(100 + ((DataManger.unitStatDictionary[1].MDefence + (DataManger.unitStatDictionary[1].GrowthmDefence * DataManger.unitStatDictionary[1].Level)
                                + itemStatus.magicResistance/*+ strengthStatus.magicResistance*/)))))
                );
            }

            //마법저항력이 0보다 작으면
            else if (DataManger.unitStatDictionary[1].MDefence + DataManger.unitStatDictionary[1].GrowthmDefence + itemStatus.magicResistance < 0)
            {
                //마법저항력 설명창
                mdef.text = string.Format("현재 마법 저항력: {0} (기본 {1} + 추가 {2})\r\n마법 피해를 {3}% 만큼 덜 받습니다."
                ,//{0} 현재 총마법 저항력:기본 마법저항력 +성장마법저항력 + 아이템마법저항력 
                DataManger.unitStatDictionary[1].MDefence + DataManger.unitStatDictionary[1].GrowthmDefence + itemStatus.magicResistance

                ,//{1} 기본 마법 저항력: 기본 마법저항력 
                DataManger.unitStatDictionary[1].MDefence
                ,//{2} 추가 마법 저항력: 아이템마법저항력 +  (성장마법저항력 * 레벨) 
                itemStatus.magicResistance + (DataManger.unitStatDictionary[1].GrowthmDefence * DataManger.unitStatDictionary[1].Level)
                ,/*{3} 마법피해 감소량: %%마법저항력이 0보다 낮으면%% 100-(2-(10000 / (100 + (기본마법저항력 + 성장마법저항력 * Level + 아이템마법저항력)))))*/
                (int)100-(2-10000/(100 + (DataManger.unitStatDictionary[1].MDefence + (DataManger.unitStatDictionary[1].GrowthmDefence * DataManger.unitStatDictionary[1].Level)
                            + itemStatus.magicResistance/*+ strengthStatus.magicResistance*/))));
            }

            //7.
            //스텟창 스킬가속도 수치
            //{0} 스킬가속도 수치 : 아이템 스킬가속 + 증강 스킬가속 
            SkillBoostSTAT.text = string.Format("{0}", itemStatus.abilityHaste/* +strengthStatus.SkillBoost*/);


            if (itemStatus.abilityHaste/* +strengthStatus.SkillBoost*/< 500)
            {
                //스킬가속도 설명창
                SkillBoost.text = string.Format("현재 스킬 가속: {0}\r\n스킬 재사용 대기시간이 {1}% 감소하는 효과와 동일합니다."

                ,//{0} 현재 스킬 가속도:아이템 스킬가속 + 증강 스킬가속 
                itemStatus.abilityHaste/* +strengthStatus.SkillBoost*/
                ,//{1} 재사용대기시간: if(500미만일때) 100/(100+아이템 스킬가속 + 증강 스킬가속)
                100 * itemStatus.abilityHaste /*+ strengthStatus.SkillBoost*/  / (100 + itemStatus.abilityHaste /*+ strengthStatus.SkillBoost*/)
                );

            }

            //8.
            //스텟창 이동속도 수치
            //{0} 이동속도 수치 : 기본이동속도 + 아이템이동속도 
            movementSTAT.text = string.Format("{0}",
                DataManger.unitStatDictionary[1].MoveMentSpeed + itemStatus.movementSpeed);

            //이동속도 설명창
            movement.text = string.Format("현재 이동 속도 : 초당 {0}(기본 {1} + 추가 {2})"
              ,//{0} 총이동속도 : 기본이동속도 + 아이템이동속도
              DataManger.unitStatDictionary[1].MoveMentSpeed + itemStatus.movementSpeed
              ,//{1} 기본이동속도 : 기본이동속도
              DataManger.unitStatDictionary[1].MoveMentSpeed
              ,//{2} 추가이동속도 : 아이템이동속도 
              itemStatus.movementSpeed
              );
        }
        if (otherStatSystem.activeSelf)
        {

            //9.
            //스텟창 체력회복수치
            //{0}5초당체력회복 수치 : 기본체력회복 + (성장체력회복 * 레벨) + ((기본체력회복 + (성장체력회복 * 레벨)) * 아이템 체력회복)
            // %%실제 적용은%% 1초당 임으로 (체력회복수치/5)를 적용해야함
            hpRegenSTAT.text = string.Format("{0}", DataManger.unitStatDictionary[1].HpRecovery + (DataManger.unitStatDictionary[1].GrowthhpRecovery * DataManger.unitStatDictionary[1].Level)
                                                  + DataManger.unitStatDictionary[1].HpRecovery + (DataManger.unitStatDictionary[1].GrowthhpRecovery * DataManger.unitStatDictionary[1].Level) * itemStatus.basicHealthRegenaration);

            //체력회복 설명창
            hpRegen.text = string.Format("현재 체력 재생: {0} (기본 {1} + 추가 {2})"
              ,//{0} 총체력회복 : 기본체력회복 + (성장체력회복 * 레벨) + ((기본체력회복 + (성장체력회복 * 레벨)) * 아이템 체력회복)
               DataManger.unitStatDictionary[1].HpRecovery + (DataManger.unitStatDictionary[1].GrowthhpRecovery * DataManger.unitStatDictionary[1].Level)
             + DataManger.unitStatDictionary[1].HpRecovery + (DataManger.unitStatDictionary[1].GrowthhpRecovery * DataManger.unitStatDictionary[1].Level) * itemStatus.basicHealthRegenaration

              ,//{1} 기본체력회복 : 기본체력회복
              DataManger.unitStatDictionary[1].HpRecovery
              ,//{2} 추가체력회복 :(성장체력회복 * 레벨) + ((기본체력회복 + (성장체력회복 * 레벨)) * 아이템 체력회복)
              (DataManger.unitStatDictionary[1].GrowthhpRecovery * DataManger.unitStatDictionary[1].Level)
             + (DataManger.unitStatDictionary[1].HpRecovery + (DataManger.unitStatDictionary[1].GrowthhpRecovery * DataManger.unitStatDictionary[1].Level) * itemStatus.basicHealthRegenaration));

            //10.
            //방어구관통력수치
            //{0} 물리관통력수치 : 아이템방어구관통력 + 증강물리관통력
            //{1} 방어구관통력수치 :증강방어구 관통력%
            armorPenetrationSTAT.text = string.Format("{0}||{1}%", itemStatus.armorPenetration /*+strengthStatus.ArmorPenetration*/,  0 /*+ strengthStatus.ArmorPenetrationper*/);
            //방어구관통력 설명창
            armorPenetration.text = string.Format("현재 물리 관통력 | 방어구 관통력: {0} | {1}%"

              ,//{0} 물리관통력수치 : 아이템방어구관통력 + 증강물리관통력
               itemStatus.armorPenetration /*+strengthStatus.ArmorPenetration*/
              ,//{1} 방어구관통력수치 :아이템방어구관통력% + 증강방어구 관통력%
              0 /*itemStatus.armorPenetrationper + strengthStatus.ArmorPenetrationper*/
              );

            //11.
            //생명력흡수수치
            //{0} 생명력흡수량: 아이템생명력흡수량 + 증강생명력흡수량
            VampSTAT.text = string.Format("{0}", itemStatus.lifeSteal /* + strengthStatus.lifesteal*/);

            //생명력흡수량 설명창
            Vamp.text = string.Format("현재 생명력 흡수량: {0}%"
              ,//{0} 생명력흡수량: 아이템생명력흡수량 + 증강생명력흡수량
             itemStatus.lifeSteal /* + strengthStatus.lifesteal*/
             );

            //12.
            //사정거리
            //{0}사정거리 수치 : 기본사정거리
            AttackRangeSTAT.text = string.Format("{0}", DataManger.unitStatDictionary[1].AttackRange);
            //사정거리 설명창
            AttackRange.text = string.Format("현재 공격 사거리: {0} ( 기본 {1} + 추가 {2})"

              ,//{0} 총사정거리 : 기본사정거리
              DataManger.unitStatDictionary[1].AttackRange
              ,//{1} 기본사정거리 : 기본사정거리
              DataManger.unitStatDictionary[1].AttackRange
              ,//{2} 추가사정거리 : 아이템사정거리
              0/*itemStatus.AttackRange*/              
              );

            //13.
            //자원회복수치
            //{0}자원회복수치 : 기본자원회복수치 +  (성장자원회복수치 * 레벨) + ((기본체력회복+성장자원회복수치*레벨) * 아이템 자원회복))
            // %%실제 적용은%% 1초당 임으로 (체력회복수치/5)를 적용해야함
            mpRegenSTAT.text = string.Format("{0}", DataManger.unitStatDictionary[1].MpRecovery + (DataManger.unitStatDictionary[1].GrowthmpRecovery * DataManger.unitStatDictionary[1].Level)
                                                   + DataManger.unitStatDictionary[1].MpRecovery + (DataManger.unitStatDictionary[1].GrowthmpRecovery * DataManger.unitStatDictionary[1].Level) * itemStatus.basicManaRegenaration
                                                    );

            //자원회복 설명창
            mpRegen.text = string.Format("현재 자원 재생: {0} ( 기본 {1} + 추가 {2} )"
              ,//{0} 총자원회복 : 기본자원회복수치 +  (성장자원회복수치 * 레벨) + ((기본체력회복+성장자원회복수치*레벨) * 아이템 자원회복))
              DataManger.unitStatDictionary[1].MpRecovery + (DataManger.unitStatDictionary[1].GrowthmpRecovery * DataManger.unitStatDictionary[1].Level)
                                                   + DataManger.unitStatDictionary[1].MpRecovery + (DataManger.unitStatDictionary[1].GrowthmpRecovery * DataManger.unitStatDictionary[1].Level) * itemStatus.basicManaRegenaration

              ,//{1} 기본자원회복 : 기본자원회복수치
              DataManger.unitStatDictionary[1].MpRecovery
              ,//{2} 추가자원회복 : (성장자원회복수치 * 레벨) + ((기본체력회복+성장자원회복수치*레벨) * 아이템 자원회복))
              (DataManger.unitStatDictionary[1].GrowthmpRecovery * DataManger.unitStatDictionary[1].Level)
                                                   + DataManger.unitStatDictionary[1].MpRecovery + (DataManger.unitStatDictionary[1].GrowthmpRecovery * DataManger.unitStatDictionary[1].Level) * itemStatus.basicManaRegenaration
);

            //14.
            //마법관통력 수치
            //{0}마법관통력(절대값)수치 : 아이템마법관통력 + 증강마법관통력
            //{1}마법관통력%수치 : 마법관통력% + 증강마법관통력%
            magicPenetrationSTAT.text = string.Format("{0} | {1}", itemStatus.magicPenetration /*+strengthStatus.magicPenetration*/, 0/*itemStatus.magicPenetrationper +strengthStatus.MagicPenetrationper*/);

            //마법관통력 설명창
            magicPenetration.text = string.Format("현재 마법 관통력 : {0} | {1}%"
              ,//{0}마법관통력(절대값)수치 : 아이템마법관통력 + 증강마법관통력
              itemStatus.magicPenetration /*+strengthStatus.magicPenetration*/
              ,//{1}마법관통력%수치 : 마법관통력% + 증강마법관통력%
              0/*itemStatus.magicPenetrationper + strengthStatus.MagicPenetrationper*/
              );

            //15.
            //물리 피해 흡혈||모든 피해 흡혈 수치
            //{0} 물리 피해 흡혈 수치 : 0
            //{1} 모든 피해 흡혈 수치 : 아이템모든피해흡혈 + 증강모든피해흡혈
            allVampSTAT.text = string.Format("{0}||{1}", 0, DataManger.unitStatDictionary[1].SkillBloodSucking /*+strengthStatus.SkillBloodSucking*/);
            //물리 피해 흡혈||모든 피해 흡혈 설명창
            allVamp.text = string.Format("물리 피해 흡혈 | 모든 피해 흡혈 : {0}% |  {1}%"
              ,//{0}물리 피해 흡혈 수치 : 아이템 물리 피해 흡혈 + 증강 물리 피해 흡혈
                0
              ,//{1}모든 피해 흡혈 수치 : 아이템 모든 피해 흡혈 + 증강 모든 피해 흡혈
              DataManger.unitStatDictionary[1].SkillBloodSucking /*+strengthStatus.SkillBloodSucking*/
              );

            //16.
            //강인함
            //{0}강인함수치 : 
            tenacitySTAT.text = string.Format("{0}", 0);
            //강인함 설명창
            tenacity.text = string.Format("현재 강인함: {0}%"
              ,//{0} 아이템강인함 + 증강 강인함
             0);
        }



    }








}
