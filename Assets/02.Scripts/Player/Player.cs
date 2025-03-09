using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    { 
        get { return instance; }
    }

    private PlayerCondition condition;
    public PlayerCondition Condition
    {
        get { return condition; }
        set { condition = value; }
    }

    private PlayerController controller;
    public PlayerController Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    [HideInInspector] public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        condition = GetComponent<PlayerCondition>();
        controller = GetComponent<PlayerController>();
    }
}