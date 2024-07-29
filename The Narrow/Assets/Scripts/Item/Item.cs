//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    [Header("������ �̸�")]
    public string itemName;

    [Header("������ ��������Ʈ")]
    public Sprite itemSprite;

    [Header("������ ������")]
    public GameObject itemPrefab;

    //������ Ÿ�� ����
    public enum ItemType
    {
        //Define ItemType
        Key,
        Ingredient,
    }

    [Header("������ Ÿ��")]
    public ItemType itemType;


}
