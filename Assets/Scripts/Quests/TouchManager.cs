using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private InputAction touch;
    private InputAction mouseClick;
    private InputAction regenerate;
    private InputAction openMenu;

    [Header("Canvas Properties")]
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI rewardText;

    [SerializeField] private TextMeshProUGUI optionalTitleText;
    [SerializeField] private TextMeshProUGUI optionalDescriptionText;

    private void Start()
    {
        touch = InputSystem.actions.FindAction("Touch", true);
        mouseClick = InputSystem.actions.FindAction("MousePressed", true);
        regenerate = InputSystem.actions.FindAction("Generate");
        openMenu = InputSystem.actions.FindAction("OpenMenu");
    }
    // Update is called once per frame
    void Update()
    {
        if (touch.WasPerformedThisFrame())
        {
            Vector2 touchPos = touch.ReadValue<Vector2>();
            ApplyText(touchPos);
        }

        if(mouseClick.WasPressedThisFrame())
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            ApplyText(mousePos);
        }

        if (openMenu.WasReleasedThisFrame())
        {

            targetCanvas.enabled = false;
        }

        if(regenerate.WasReleasedThisFrame())
        {
            targetCanvas.enabled = false;
        }
    }

    public void ApplyText(Vector2 startLocation)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(startLocation);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.gameObject.tag == "Node")
        {
            targetCanvas.enabled = true;

            var nodeReference = hit.collider.gameObject.GetComponent<Node>();

            titleText.text = nodeReference.questType;

            descriptionText.text = nodeReference.questDescription;

            rewardText.text = nodeReference.questReward;

            if (EventBus.Instance.hasQuestBranching.GetToggleValue())
            {
                if (nodeReference.optionalQuestDescription != string.Empty && nodeReference.optionalQuestType != string.Empty)
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
            else
            {
                optionalTitleText.text = string.Empty;
                optionalTitleText.enabled = false;

                optionalDescriptionText.text = string.Empty;
                optionalDescriptionText.enabled = false;
            }
        }

        if (!hit.collider)
        {
            targetCanvas.enabled = false;
        }
    }
}
