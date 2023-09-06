using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public delegate void CastActiveHandler();
    public CastActiveHandler castActiveHandler;

    private Vector3 targetPos;          // 이동할 위치

    /// <summary>
    /// InputManager 클래스의 OnUpdate 함수에서 사용되는 함수
    /// 김민섭_230906
    /// </summary>
    public void OnUpdate()
    {
        //CastPassiveSkill();

        //Move();
        UpdateMove(targetPos);

        //CastActiveSkill();
        //CastSpellSkill();
    }

    #region 이동 관련 함수

    public void Move()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Unit")))
            {
                Vector3 rayStartPosition = Camera.main.transform.position;      // 레이를 그려줄 시작 위치 (카메라 위치)
                Vector3 rayEndPosition = hit.point;         // 레이를 그려줄 끝 위치 (충돌 지점)    
                float duration = 2f;        // 레이를 그려주기 위한 시간 (프레임당 레이가 유지될 시간)
                Debug.DrawRay(rayStartPosition, rayEndPosition - rayStartPosition, Color.blue, duration);        // Debug.DrawRay를 사용하여 레이를 그림

                Debug.Log("유닛을 선택했습니다.");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Floor")))
            {
                Vector3 rayStartPosition = Camera.main.transform.position;      // 레이를 그려줄 시작 위치 (카메라 위치)
                Vector3 rayEndPosition = hit.point;         // 레이를 그려줄 끝 위치 (충돌 지점)    
                float duration = 2f;        // 레이를 그려주기 위한 시간 (프레임당 레이가 유지될 시간)
                Debug.DrawRay(rayStartPosition, rayEndPosition - rayStartPosition, Color.red, duration);        // Debug.DrawRay를 사용하여 레이를 그림

                targetPos = rayEndPosition;
            }
        }
    }

    public void UpdateMove(Vector3 targetPos)
    {
        if (targetPos == default) return;

        Vector3 direct = targetPos - transform.position;
        direct.y = 0f;

        float minDistance = direct.magnitude;
        if(minDistance <= 1f)
        {
            targetPos = default;
            Debug.Log("목표에 도착함");
            return;
        }
        else
        {
            float moveDistance = Mathf.Clamp(5f * Time.deltaTime, 0f, direct.magnitude);
            transform.position += direct.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direct), 20 * Time.deltaTime);
        }    
    }

    #endregion

    #region 스킬 관련 함수

    /// <summary>
    /// 키 입력에 따라 액티브 스킬 시전하는 함수
    /// 김민섭_230906
    /// </summary>
    public virtual void CastActiveSkill()
    {
        castActiveHandler();

        //if(Input.GetKeyDown(KeyCode.Q))
        //{
        //    CastActiveQ();
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    CastActiveW();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    CastActiveE();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    CastActiveR();
        //}
    }

    /// <summary>
    /// 패시브 스킬 시전하는 함수
    /// 김민섭_230906
    /// </summary>
    public virtual void CastPassiveSkill()
    {
        CastPassive();
    }

    /// <summary>
    /// 키 입력에 따라 스펠 스킬 시전하는 함수
    /// 김민섭_230906
    /// </summary>
    /// <param name="key"></param>
    public virtual void CastSpellSkill()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CastSpellD();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            CastSpellF();
        }
    }

    #region 액티브 스킬

    /// <summary>
    /// 액티브 스킬 Q 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveQ()
    {
        Debug.Log("Q 스킬 발동");
    }

    /// <summary>
    /// 액티브 스킬 W 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveW()
    {
        Debug.Log("W 스킬 발동");
    }

    /// <summary>
    /// 액티브 스킬 E 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveE()
    {
        Debug.Log("E 스킬 발동");
    }

    /// <summary>
    /// 액티브 스킬 R 시전 함수
    /// 김민섭_230906
    /// </summary>
    protected virtual void CastActiveR()
    {
        Debug.Log("R 스킬 발동");
    }

    #endregion

    #region 패시브 스킬

    protected virtual void CastPassive()
    {
        Debug.Log("패시브 스킬");
    }

    #endregion

    #region 스펠 스킬

    protected virtual void CastSpellD()
    {
        Debug.Log("D 스펠 발동");
    }

    protected virtual void CastSpellF()
    {
        Debug.Log("F 스펠 발동");
    }

    #endregion

    #endregion
}
