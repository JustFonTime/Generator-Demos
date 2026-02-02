using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    private InputAction mouseClick;

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
                print(hit.collider.gameObject.name);
            }
        }

        if(mouseClick.WasReleasedThisFrame())
        {
        }
    }
}
