using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField, Tooltip("접촉한 플레이어에게 가할 데미지")] private int damage;
    [SerializeField, Tooltip("접촉한 상태에서 데미지를 가하는 시간 단위")] private float damageRate;

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
