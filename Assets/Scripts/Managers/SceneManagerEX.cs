using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public BaseScene CurrentScene => GameObject.FindObjectOfType<BaseScene>();

    public void LoadScene(Define.Scene _type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(_type));
    }

    private string GetSceneName(Define.Scene _type) => System.Enum.GetName(typeof(Define.Scene), _type);

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
