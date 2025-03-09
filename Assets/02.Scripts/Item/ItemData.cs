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
    [Header("������ ����")]
    [SerializeField, Tooltip("������ ������")] private Sprite itemIcon;
    public Sprite ItmeIcon { get { return itemIcon; } }
    [SerializeField, Tooltip("������ �̸�")] private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField, Tooltip("������ ����")] private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }
    [SerializeField, Tooltip("������ ����")] private ItemType itemType;
    [SerializeField, Tooltip("������ ȿ�� ���� �ð�")] private float itemduration;
    public float ItemDuration {  get { return itemduration; } }
    [SerializeField, Tooltip("�������� ������")] private GameObject itemPrefab;
    public GameObject ItemPrefab { get { return itemPrefab; } }
    public ItemType Type { get { return itemType; } }

    [Header("Consumable")]
    [SerializeField, Tooltip("������ ȿ�� ��ġ")] private ConsumableItemData[] itemStats;
    public ConsumableItemData[] ConsumableStat { get { return itemStats; } }
    [SerializeField, Tooltip("���� ���� ������ �� �ִ��� ����")] private bool canStack;
    public bool CanStack { get { return canStack; } }
    [SerializeField, Tooltip("���� ������ �ִ� ����")] private int maxStack;
    public int MaxStack { get { return maxStack; } }
}