using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [SerializeField]private float curValue;  // �� �Ӽ��� ���� ��
    public float CurValue {  get { return curValue; } }
    [SerializeField, Tooltip("������ �������� �� ������ ��")] private float startValue;
    [SerializeField, Tooltip("�� �Ӽ��� �ִ� ��")] private float maxValue;
    public float MaxValue { get { return maxValue; } }
    [SerializeField, Tooltip("�� �Ӽ��� �ڿ������� ȸ�� or ���ҵ� ��")] private float passiveValue;
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
