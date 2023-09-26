using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Define.UnitName SelectedCharacter { get; private set; }
    public string Nickname { get; private set; }

    public void SetCharacterAndNickName(Define.UnitName character, string nickname)
    {
        SelectedCharacter = character;
        Nickname = nickname;
    }
}