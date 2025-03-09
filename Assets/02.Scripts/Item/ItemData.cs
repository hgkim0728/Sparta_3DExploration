using System;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equip,
    Resources
}

public enum ConsumableType
{
    Health
}

[Serializable]
public class ConsumableItemData
{
    public ConsumableType consumableType;
    public int itmeStat;
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class ItemData : ScriptableObject
{
    [Header("������ ����")]
    [SerializeField, Tooltip("������ �̸�")] private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField, Tooltip("������ ����")] private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }
    [SerializeField, Tooltip("������ ����")] private ItemType itemType;
    [SerializeField, Tooltip("�������� ������")] private GameObject itemPrefab;
    public GameObject ItemPrefab { get { return itemPrefab; } }
    public ItemType Type { get { return itemType; } }

    [Header("Consumable")]
    [SerializeField, Tooltip("������ ȿ�� ��ġ")] private ConsumableItemData[] itemStats;
    public ConsumableItemData[] ItemStats { get { return itemStats; } }
    [SerializeField, Tooltip("���� ���� ������ �� �ִ��� ����")] private bool canStack;
    public bool Stacking { get { return canStack; } }
    [SerializeField, Tooltip("���� ������ �ִ� ����")] private int maxStack;
    public int MaxStack { get { return maxStack; } }
}