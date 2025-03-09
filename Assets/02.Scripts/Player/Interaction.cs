using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField, Tooltip("체크 간걱")] private float checkRate = 0.05f;
    private float lastCheckTime = 0f;   // 마지막으로 체크한 시간
    [SerializeField, Tooltip("물체를 감지하는 최대 거리")] private float maxCheckDistance;
    [SerializeField] private LayerMask interactObjeckLayer;

    private GameObject curInteractGameObject;
    private IInteractable curInteractable;

    [SerializeField] private Text promptText;
    [SerializeField] private Transform interactRayPoint;

    void Update()
    {
        CheckInteract();
    }

    void CheckInteract()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            Ray ray = new Ray(interactRayPoint.position, interactRayPoint.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, interactObjeckLayer))
            {
                if(hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
