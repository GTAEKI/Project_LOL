using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private Define.UnitName _myCharactor;

    public static PlayerData Instance { get; private set; }
    public string SelectedCharacterName { get; private set; } // 캐릭터 이름을 저장
    public string Nickname { get; private set; }
    public Define.UnitName myCharactor
    {
        get => _myCharactor;
        set => _myCharactor = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCharacterAndNickName(string characterName, string nickname)
    {
        SelectedCharacterName = characterName;
        Nickname = nickname;
    }
}
