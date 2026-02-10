using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class MouseManager : MonoBehaviour
{
    private InputAction mouseClick;

    [Header("Canvas Properties")]
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI rewardText;

    [SerializeField] private TextMeshProUGUI optionalTitleText;
    [SerializeField] private TextMeshProUGUI optionalDescriptionText;

    private void Start()
    {
        mouseClick = InputSystem.actions.FindAction("MousePressed", true);
    }
    // Update is called once per frame
    void Update()
    {
        if(mouseClick.WasPressedThisFrame())
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);

            if (hit.collider != null && hit.collider.gameObject.tag == "Node")
            {
                targetCanvas.enabled = true;

                var nodeReference = hit.collider.gameObject.GetComponent<Node>();

                titleText.text = nodeReference.questType;

                descriptionText.text = nodeReference.questDescription;

                rewardText.text = nodeReference.questReward;

                if(EventBus.Instance.hasQuestBranching.GetToggleValue())
                {
                    if(nodeReference.optionalQuestDescription != string.Empty && nodeReference.optionalQuestType != string.Empty)
                    {
                        optionalTitleText.enabled = true;
                        optionalDescriptionText.enabled = true;

                        optionalTitleText.text = nodeReference.optionalQuestType;
                        optionalDescriptionText.text = nodeReference.optionalQuestDescription;
                    }
                    else
                    {
                        optionalTitleText.enabled = false;
                        optionalDescriptionText.enabled = false;
                    }
                }
            }

            if (!hit.collider)
            {
                targetCanvas.enabled = false;
            }
        }

        if(mouseClick.WasReleasedThisFrame())
        {
        }
    }
}
