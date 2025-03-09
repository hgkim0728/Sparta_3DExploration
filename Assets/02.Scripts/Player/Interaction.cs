using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField, Tooltip("체크 간걱")] private float checkRate = 0.05f;
    private float lastCheckTime = 0f;   // 마지막으로 체크한 시간
    [SerializeField, Tooltip("물체를 감지하는 최대 거리")] private float maxCheckDistance;
    [SerializeField] private LayerMask interactObjeckLayer;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
