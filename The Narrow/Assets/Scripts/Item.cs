//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    [Header("아이템 이름")]
    public string itemName;

    [Header("아이템 스프라이트")]
    public Sprite itemSprite;

    [Header("아이템 프리팹")]
    public GameObject itemPrefab;

    //아이템 타입 정리
    public enum ItemType
    {
        //Define ItemType
        Key,
        Ingredient,
    }

    [Header("아이템 타입")]
    public ItemType itemType;


}
