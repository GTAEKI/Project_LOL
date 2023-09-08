using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ �Ӽ��� ��� ���� Ŭ����
/// </summary>

public class SB_ItemProperty : MonoBehaviour
{
    public int typeNumber; // 0: ����, 1: ��ȭ
    public string name; // ������ �̸�
    public string englishName; // �̹��� ��Ī - ������
    public int attackDamage; // ���ݷ�
    public int attackSpeed; // ���� �ӵ�
    public int armor; // ����
    public int magicResistance; // ��������
    public int health; // ü��
    public int abilityHaste; // ��ų ����
    public int lifeSteal; // ����� ���
    public int criticalStrikeChance; // ġ��Ÿ Ȯ��
    public int movementSpeed;
    public int lethality;
    public int gold = 3000; // ������ ���� (����)
        
    /// <summary>
    /// �̹��� ��ü�� ���� �޼����Դϴ�.
    /// </summary>
    /// <param name="_sprite">ItemDataReader.cs���� Sprite Name �ޱ�</param>
    public void ChangeImg(string _sprite)
    {
        string sprite = _sprite;

        Image image = transform.GetComponent<Image>();
        Sprite itemImg = Resources.Load<Sprite>($"Item Img/Legend/{sprite}");
        Debug.Assert(itemImg != null);
        image.sprite = itemImg;
    }
}