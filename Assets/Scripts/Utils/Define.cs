/// <summary>
/// 疫꿸퀬? 揶쏅?諭??筌뤴뫁釉??蹂? ?????
/// 繹먃沃섏눘苑?230906
/// </summary>
public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
        Test
    }

    public enum Sound
    {
        Bgm,
        Sfx,
        MaxCount
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

    public enum MouseEvent
    {
        Press,
        Click
    }

    /// <summary>
    /// Camera Mode enum
    /// KimMinSeob_230913
    /// </summary>
    public enum CameraMode
    {
        QuaterView
    }

    /// <summary>
    /// Unit's State enum
    /// KimMinSeob_230907
    /// </summary>
    public enum UnitState
    {
        NONE, IDLE, MOVE, SPELLQ, SPELLW, SPELLE, SPELLR, CastQ, CastW, CastE, CastR, Attack, Die
        // 경택 q,w,e,r _230919
    }

    /// <summary>
    /// Game Team enum
    /// KimMinSeob_230913
    /// </summary>
    public enum GameTeam
    {
        BLUE, RED
    }

    #region Unit Name Enum

    public enum UnitName
    {
        Dummy_Puppet,
        Rumble,
        Ashe,
        Caityln,
        Gragas,
        Yasuo = 100,
        
    }

    #endregion
}
