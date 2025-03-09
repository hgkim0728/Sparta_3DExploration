using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private ItemSlot[] slots;
    [SerializeField] private GameObject inventoryWindow;
    [SerializeField] private Transform slotPanel;
    private Transform dropPosition;

    [Header("인벤토리 창 내부 UI")]
    [SerializeField] private Text selectedItemName;
    [SerializeField] private Text selectedItemDescription;
    [SerializeField] private Text selectedItemStatName;
    [SerializeField] private Text selectedItemStatValue;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject dropButton;

    private PlayerController playerController;
    private PlayerCondition playerCondition;

    private ItemSlot selectedItem;
    private int selectedItemIndex = 0;

    void Start()
    {
        playerController = Player.Instance.Controller;
        playerCondition = Player.Instance.Condition;
        dropPosition = Player.Instance.dropPosition;

        playerController.inventory += Toggle;
        Player.Instance.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
    }

    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void Toggle()
    {
        if(IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData data = Player.Instance.itemData;

        if(data.CanStack)
        {
            ItemSlot slot = GetItemStack(data);

            if(slot != null)
            {
                slot.quantity++;
                UpdateUI();
                Player.Instance.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            Player.Instance.itemData = null;
            return;
        }

        ThrowItem(data);
        Player.Instance.itemData = null;
    }

    ItemSlot GetItemStack(ItemData _data)
    {
        foreach(ItemSlot slot in slots)
        {
            if(slot.item == _data && slot.quantity < _data.MaxStack)
            {
                return slot;
            }
        }

        return null;
    }

    void UpdateUI()
    {
        foreach(ItemSlot slot in slots)
        {
            if(slot.item != null)
            {
                slot.SetSlot();
            }
            else
            {
                slot.ClearSlot();
            }
        }
    }

    ItemSlot GetEmptySlot()
    {
        foreach(ItemSlot slot in slots)
        {
            if(slot.item == null)
            {
                return slot;
            }
        }

        return null;
    }

    void ThrowItem(ItemData _data)
    {
        Instantiate(_data.ItemPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(int _index)
    {
        if (slots[_index].item == null) return;

        selectedItem = slots[_index];
        selectedItemIndex = _index;

        selectedItemName.text = selectedItem.item.ItemName;
        selectedItemDescription.text = selectedItem.item.ItemDescription;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for(int i  = 0; i < selectedItem.item.ConsumableStat.Length; i++)
        {
            selectedItemStatName.text += selectedItem.item.ConsumableStat[i].consumableType.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.item.ConsumableStat[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.item.Type == ItemType.Consumable);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if(selectedItem.item.Type == ItemType.Consumable)
        {
            for(int i = 0; i < selectedItem.item.ConsumableStat.Length; i++)
            {
                switch(selectedItem.item.ConsumableStat[i].consumableType)
                {
                    case ConsumableType.Health:
                        playerCondition.Heal(selectedItem.item.ConsumableStat[i].value);
                        break;

                    case ConsumableType.SpeedBuff:
                        playerController.UseSpeedItem(selectedItem.item.ItemDuration, selectedItem.item.ConsumableStat[i].value);
                        break;
                }
            }

            RemoveSelectedItem();
        }
    }

    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }
}
