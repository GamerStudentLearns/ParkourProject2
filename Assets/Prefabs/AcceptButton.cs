using UnityEngine;

public class AcceptButton : MonoBehaviour
{
    
    public void Accept()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Accept Button Pressed");
    }
}
