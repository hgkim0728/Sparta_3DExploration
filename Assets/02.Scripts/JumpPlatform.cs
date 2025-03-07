using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField, Tooltip("플레이어를 밀어낼 힘")] private float jumpForce = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player.Instance.Controller.Jump(jumpForce);
        }
    }
}
