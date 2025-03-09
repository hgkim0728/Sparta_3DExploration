using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField, Tooltip("아이템 데이터")] private ItemData data;
    public ItemData Data { get { return data; } }

    public string GetInteractPrompt()
    {
        string str = $"{data.ItemName}\n{data.ItemDescription}";
        return str;
    }

    public void OnInteract()
    {
        Player.Instance.itemData = data;
        Player.Instance.addItem?.Invoke();
        Destroy(gameObject);
    }
}
