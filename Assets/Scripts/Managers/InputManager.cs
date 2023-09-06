using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action keyAction = null;
    public Action<Define.MouseEvent> mouseAction = null;

    private bool mousePress = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.anyKey && keyAction != null) keyAction.Invoke();

        if(mouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                mouseAction.Invoke(Define.MouseEvent.Press);
                mousePress = true;
            }
            else
            {
                if (mousePress) mouseAction.Invoke(Define.MouseEvent.Click);
                mousePress = false;
            }
        }
    }

    public void Clear()
    {
        keyAction = null;
        mouseAction = null;
    }
}
