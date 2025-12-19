using UnityEngine;

public class TriggerUnlock : MonoBehaviour
{
    [Header("Object to unlock/appear")]
    public GameObject objectToUnlock;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            if (objectToUnlock != null)
            {
                objectToUnlock.SetActive(true); // make the object appear
            }

            // Unlock and show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
