using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField, Tooltip("�÷��̾ �о ��")] private float jumpForce = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player.Instance.Controller.Jump(jumpForce);
        }
    }
}
