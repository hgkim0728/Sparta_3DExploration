using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;

    void Start()
    {
        Player.Instance.Condition.uiCondition = this;
    }
}
