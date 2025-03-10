using System;
using UnityEngine;

public enum ItemType
{
    Consumable,
    NotUse
}

public enum ConsumableType
{
    Health,
    SpeedBuff
}

[Serializable]
public class ConsumableItemData
{
    public ConsumableType consumableType;
    public int value;
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class ItemData : ScriptableObject
{
    [Header("아이템 정보")]
    [SerializeField, Tooltip("아이템 아이콘")] private Sprite itemIcon;
    public Sprite ItmeIcon { get { return itemIcon; } }
    [SerializeField, Tooltip("아이템 이름")] private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField, Tooltip("아이템 설명")] private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }
    [SerializeField, Tooltip("아이템 종류")] private ItemType itemType;
    [SerializeField, Tooltip("아이템 효과 지속 시간")] private float itemduration;
    public float ItemDuration {  get { return itemduration; } }
    [SerializeField, Tooltip("아이템의 프리팹")] private GameObject itemPrefab;
    public GameObject ItemPrefab { get { return itemPrefab; } }
    public ItemType Type { get { return itemType; } }

    [Header("Consumable")]
    [SerializeField, Tooltip("아이템 효과 수치")] private ConsumableItemData[] itemStats;
    public ConsumableItemData[] ConsumableStat { get { return itemStats; } }
    [SerializeField, Tooltip("여러 개를 소지할 수 있는지 여부")] private bool canStack;
    public bool CanStack { get { return canStack; } }
    [SerializeField, Tooltip("소지 가능한 최대 개수")] private int maxStack;
    public int MaxStack { get { return maxStack; } }
}