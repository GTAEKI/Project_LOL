using UnityEngine;
using UnityEngine.UI;

public class Strength : MonoBehaviour
{
    public int intdexNumber; // 0: 전설, 1: 신화
    public string name; // 아이템 이름
    public string englishName; // 이미지 매칭 - 영문명
    public int Apper;
    public int Ap;
    public float AttackSpeed;
    public int atkper;
    public int atk;
    public int Hp;
    public int Mp;
    public int SkillBoost;
    public int CriticalChance;
    public int MovementSpeed;
    public int ArmorPenetration;
    public int ArmorPenetrationper;
    public int MagicPenetration;
    public int MagicPenetrationper;
    
    //Hello jungkeun
    //몇번째 증강인지?
    public int turn = 0;
    //선택할 카드 3개
    public GameObject[] card;
    //남은 리롤횟수
    public GameObject RetryNumber;
    //증강 스프라이트이미지들
    public Sprite[] strength;
    //증강 이름들
    private string[] Name = { "사악한정신", "능수능란", "정신변환", "육중한힘", "야수화", "바늘에실끼우기", "되풀이", "치명적인공격", "천상의 신체", "극악무도", "속행", "신비한주먹" };
    //증강 설명들
    private string[] Detail = { "주문력100증가", "공격속도100%증가", "최대마나만큼체력이늘어납니다.", "공격력이15%오릅니다", "공격력이25오르고 스킬가속이 10 물리관통력이15오릅니다.", "마법관통력,방어구관통력이 25%증가합니다.", "스킬가속이 60늘어납니다", "치명타확률이 40%늘어납니다.", "체력이 1000늘어납니다.", "주문력이 15%늘어납니다.", "스킬가속의1.5배만큼 이동속도가 늘어납니다.", "공격속도가15%,공격력이100늘어납니다." };
    //증강 보관함(아래쪽인터페이스 증강)
    public GameObject[] StrengthCase;
    //증강 설명
    public GameObject[] explanation;
    //중복제거를 위한 카드번호List
    public List<int> cardNum = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //리롤횟수번호
    private int rerollchance = 0;
    //클릭한카드의 인덱스번호
    public int[] clickedcardnumber;
    //
    Dictionary<int, string> strengthName;
    Dictionary<int, string> strengthDetail;



}
