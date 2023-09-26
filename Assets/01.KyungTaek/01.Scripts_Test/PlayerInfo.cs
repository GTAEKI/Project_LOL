using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private Define.UnitName _myCharactor;

    public Define.UnitName myCharactor
    {
        get
        {
            return _myCharactor;
        }

        set
        {
            _myCharactor = value;
        }
    }

    //다음씬으로 내가 선택한 캐릭터 넘김
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
