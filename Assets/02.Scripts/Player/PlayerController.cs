using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerMove")]
    [SerializeField, Tooltip("플레이어 이동 속도")] private float moveSpeed = 5f;
    [SerializeField, Tooltip("점프 강도")] private float jumpForce = 50f;
    [SerializeField, Tooltip("IsGround 체크 범위")] private float isGroundCheckRange = 0.2f;
    [SerializeField, Tooltip("Ground 레이어")] private LayerMask groundLayer;
    
    [SerializeField, Tooltip("플레이어 모델")]private Transform playerModel;

    private Player player;
    private Rigidbody rigid;
    private Animator anim;
    private Vector2 curMoveInput = Vector2.zero;    // 이동키 입력값
    private Vector3 moveDir = Vector3.zero;     // 플레이어 이동 방향

    [HideInInspector] public Action inventory;

    private readonly int IsMove = Animator.StringToHash("IsMove");
    private readonly int IsJump = Animator.StringToHash("IsJump");

    private bool isGround = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        player = Player.Instance;
        player.Controller = this;
    }

    void FixedUpdate()
    {
        isGround = IsGround();
        Move();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
            anim.SetBool(IsMove, true);
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
            anim.SetBool(IsMove, false);
        }
    }

    void Move()
    {
        moveDir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        Rotate();
        moveDir *= moveSpeed;
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && isGround)
        {
            Jump(jumpForce);
        }
    }

    public void Jump(float _force)
    {
        rigid.AddForce(Vector2.up * _force, ForceMode.Impulse);
    }

    void Rotate()
    {
        if (moveDir != Vector3.zero)
        {
            playerModel.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }
    }

    private bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position+(transform.forward * isGroundCheckRange) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position+(-transform.forward * isGroundCheckRange) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position+(transform.right * isGroundCheckRange) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position+(-transform.right * isGroundCheckRange) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayer))
            {
                anim.SetBool(IsJump, false);
                return true;
            }
        }

        anim.SetBool(IsJump, true);
        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
        }
    }

    public void UseSpeedItem(float _time, float _value)
    {
        StartCoroutine(SpeedUp(_time, _value));
    }

    private IEnumerator SpeedUp(float _tiem, float _value)
    {
        moveSpeed += _value;

        while(_tiem > 0)
        {
            _tiem -= Time.deltaTime;
            yield return null;
        }

        moveSpeed -= _value;
    }
}