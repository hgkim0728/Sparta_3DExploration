using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [HideInInspector] public ItemData item;
    [HideInInspector] public UIInventory inventory;

    [SerializeField] private Image iconImg;
    [SerializeField] private Text quantityText;

    [HideInInspector] public int index;
    [HideInInspector] public int quantity;

    public void SetSlot()
    {
        iconImg.gameObject.SetActive(true);
        iconImg.sprite = item.ItmeIcon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
    }

    public void ClearSlot()
    {
        item = null;
        iconImg.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
