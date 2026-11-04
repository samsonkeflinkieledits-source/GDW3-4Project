using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float mouseSens = 2.0f;
    float camVertRot = 0.0f;

    bool lockedCursor = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible=false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSens;
        float inputY = Input.GetAxis("Mouse Y") * mouseSens;

        camVertRot -= inputY;
        camVertRot = Mathf.Clamp(camVertRot, -60f, 60f);
        transform.localEulerAngles = Vector3.right * camVertRot;

        player.Rotate(Vector3.up * inputX);
    }
}