/// <summary>
/// 기타 Enum들을 모아 놓은 클래스
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

    public enum CameraMode
    {
        QuaterView
    }

    /// <summary>
    /// 유닛 상태 enum
    /// </summary>
    public enum UnitState
    {
        NONE, IDLE, MOVE
    }   // 기본, 이동
}
