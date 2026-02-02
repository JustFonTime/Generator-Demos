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
        Vector3 mousePos = Mouse.current.position.ReadValue();
        //print(mousePos);

        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        if(mouseClick.WasPressedThisFrame())
        {
            print("Mouse was pressed");
        }

        if(mouseClick.WasReleasedThisFrame())
        {
            print("Mouse was released");
        }
    }
}
