using System;
using UnityEngine;
using UnityEngine.UI;

public interface IDamagable
{
    public void GetDamage(int _damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    void Update()
    {
        if(health.CurValue < health.MaxValue)
        {
            health.Add(health.PassiveValue * Time.deltaTime);
        }
    }

    public void Heal(float _value)
    {
        health.Add(_value);
    }

    public void GetDamage(int _damage)
    {
        health.Subtract(_damage);
        if(health.CurValue <= 0)
        {
            Die();
        }
        onTakeDamage?.Invoke();
    }

    private void Die()
    {
        Debug.Log("die");
    }
}