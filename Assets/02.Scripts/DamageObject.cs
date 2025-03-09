using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField, Tooltip("������ �÷��̾�� ���� ������")] private int damage;
    [SerializeField, Tooltip("������ ���¿��� �������� ���ϴ� �ð� ����")] private float damageRate;

    List<IDamagable> things = new List<IDamagable>();

    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        foreach(IDamagable thing in things)
        {
            thing.GetDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            things.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            things.Remove(damagable);
        }
    }
}
