using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rbPlayer;
    public GameObject dirPos;
    public GameObject RPos;

    public float playerSpeed = 0.25f;
    public float jumpSpeed = 1.0f;

    //public float xPos = 0.0f;
    //public float yPos = 0.0f;
    //public float zPos = 0.0f;

    public float forwardVal = 0.0f;
    public float rightVal = 0.0f;
    public float jumpVal = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            forwardVal = playerSpeed;
        }

        if(Keyboard.current.sKey.isPressed)
        {
            forwardVal =- playerSpeed;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            rightVal = playerSpeed;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            rightVal = -playerSpeed;
        }
    }

    private void FixedUpdate()
    {
        rbPlayer.transform.position = Vector3.MoveTowards(rbPlayer.transform.position, dirPos.transform.position, forwardVal);
        rbPlayer.transform.position = Vector3.MoveTowards(rbPlayer.transform.position, RPos.transform.position, rightVal);

        forwardVal = 0.0f;
        rightVal = 0.0f;
    }
}
