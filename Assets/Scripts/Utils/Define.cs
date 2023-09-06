/// <summary>
/// ��Ÿ Enum���� ��� ���� Ŭ����
/// ��μ�_230906
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
    /// ���� ���� enum
    /// </summary>
    public enum UnitState
    {
        NONE, IDLE, MOVE
    }   // �⺻, �̵�
}
