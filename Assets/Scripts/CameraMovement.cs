using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    
  
    private Transform playerBody;
    private float xrot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        // Assumes the camera is a child inside the player object
        playerBody = transform.parent; 
        
        if (playerBody == null)
        {
            Debug.LogError("CameraMovement Error: The Camera must be placed INSIDE the Player object in the Hierarchy!");
        }
    }

    void Update()
    {
        if (playerBody == null) return;

        float Xmouse = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float Ymouse = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // 1. Vertical rotation (Tilt only the camera up and down)
        xrot -= Ymouse;
        xrot = Mathf.Clamp(xrot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xrot, 0f, 0f); // Y and Z are kept at 0 here
        
        // 2. Horizontal rotation (Rotate the entire player body left and right)
        playerBody.Rotate(Vector3.up * Xmouse);
    }
}
