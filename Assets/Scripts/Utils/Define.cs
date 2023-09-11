/// <summary>
/// ��Ÿ ������ ��� ���� Ŭ����
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
    /// ��μ�_230907
    /// </summary>
    public enum UnitState
    {
        NONE, IDLE, MOVE
    }   // �⺻, �̵�
}
