using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField, Tooltip("üũ ����")] private float checkRate = 0.05f;
    private float lastCheckTime = 0f;   // ���������� üũ�� �ð�
    [SerializeField, Tooltip("��ü�� �����ϴ� �ִ� �Ÿ�")] private float maxCheckDistance;
    [SerializeField] private LayerMask interactObjeckLayer;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
