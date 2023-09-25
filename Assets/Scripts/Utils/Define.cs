/// <summary>
/// 다양한 enum 보관 클래스
/// 김민섭_230906
/// </summary>
public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
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
    /// 카메라 타입 enum
    /// 김민섭_230913
    /// </summary>
    public enum CameraMode
    {
        WaitView, BattleViewA, BattleViewB
    }   // 대기존, 전투존A, 전투존B

    /// <summary>
    /// Unit's State enum
    /// KimMinSeob_230907
    /// </summary>
    public enum UnitState
    {
        NONE, IDLE, MOVE, SPELLQ, SPELLW, SPELLE, SPELLR
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
        Yasuo = 100
    }

    #endregion
}
