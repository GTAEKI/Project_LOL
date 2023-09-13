/// <summary>
/// 湲고? 媛믩뱾??紐⑥븘 ?볦? ?대옒??
/// 源誘쇱꽠_230906
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
        NONE, IDLE, MOVE
    }

    /// <summary>
    /// Game Team enum
    /// KimMinSeob_230913
    /// </summary>
    public enum GameTeam
    {
        BLUE, RED
    }
}
