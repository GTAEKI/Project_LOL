using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance; // 매니저 인스턴스 생성

    // Instance가 get? 호출되면 Init함수가 실행되고 s_instance를 반환함
    private static Managers Instance { get { Init(); return s_instance; } }

    // Core
    private DataManager _data = new DataManager();
    private InputManager _input = new InputManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private UIManager _ui = new UIManager();

    // Content
    private GameManager _game = new GameManager();

    #region 프로퍼티

    // Core
    public static DataManager Data => Instance._data;
    public static InputManager Input => Instance._input;
    public static PoolManager Pool => Instance._pool;
    public static ResourceManager Resource => Instance._resource;
    public static SceneManagerEx Scene => Instance._scene;
    public static SoundManager Sound => Instance._sound;
    public static UIManager UI => Instance._ui;

    // Contents
    public static GameManager Game => Instance._game;

    #endregion

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _game.OnUpdate();
        _input.OnUpdate();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if (obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
            s_instance._input.Init();
            s_instance._ui.Init();

            s_instance._game.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
