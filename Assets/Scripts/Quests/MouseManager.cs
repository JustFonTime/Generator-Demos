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

                titleText.text = hit.collider.gameObject.GetComponent<Node>().questType;

                //descriptionText.text = hit.collider.gameObject.GetComponent<Node>().questDescription;
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
