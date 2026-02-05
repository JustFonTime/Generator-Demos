using UnityEngine;

public class DescriptionButton : MonoBehaviour
{
    public Canvas targetCanvas;

    public void HideCanvas()
    {
        targetCanvas.enabled = false;
    }
}
