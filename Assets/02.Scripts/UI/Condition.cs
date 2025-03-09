using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [SerializeField]private float curValue;  // 이 속성의 현재 값
    public float CurValue {  get { return curValue; } }
    [SerializeField, Tooltip("게임을 시작했을 때 적용할 값")] private float startValue;
    [SerializeField, Tooltip("이 속성의 최대 값")] private float maxValue;
    public float MaxValue { get { return maxValue; } }
    [SerializeField, Tooltip("이 속성이 자연적으로 회복 or 감소될 값")] private float passiveValue;
    public float PassiveValue { get {return passiveValue; } }
    [SerializeField] private Image uiBar;

    void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float _value)
    {
        curValue = Mathf.Min(curValue + _value, maxValue);
    }

    public void Subtract(float _value)
    {
        curValue = Mathf.Max(curValue - _value, 0);
    }
}
