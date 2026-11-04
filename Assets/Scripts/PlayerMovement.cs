using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rbPlayer;
    public GameObject dirPos;
    public GameObject RPos;
    public GameObject UPos;
    public GameObject cam;

    public float playerSpeed = 0.25f;
    public float airSpeed = 0.5f;
    public float jumpSpeed = 3.0f;
    public float slideSpeed = 15.0f;

    public float xPos = 0.0f;
    public float yPos = 0.0f;
    public float zPos = 0.0f;

    public float forwardVal = 0.0f;
    public float rightVal = 0.0f;
    public float upVal = 0.0f;
    public float slideVal = 0.0f;

    public bool isGrounded = false;
    public bool isSliding = false;

    float SlideTimer = 0.0f;
    public float SlideCooldown = 1.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SlideTimer += Time.deltaTime;
        if (Keyboard.current.wKey.isPressed)
        {
            if (isGrounded == false)
            {
                forwardVal = airSpeed;
            }

            else if (isGrounded == true)
            {
                forwardVal = playerSpeed;
            }
        }

        if(Keyboard.current.sKey.isPressed)
        {
            if (isGrounded == false)
            {
                forwardVal =- airSpeed;
            }

            else if (isGrounded == true)
            {
                forwardVal =- playerSpeed;
            }
        }

        if (Keyboard.current.dKey.isPressed)
        {
            if (isGrounded == false)
            {
                rightVal = airSpeed;
            }

            else if (isGrounded == true)
            {
                rightVal = playerSpeed;
            }
        }

        if (Keyboard.current.aKey.isPressed)
        {
            if (isGrounded == false)
            {
                rightVal = -airSpeed;
            }

            else if (isGrounded == true)
            {
                rightVal = -playerSpeed;
            }
        }

        if (Keyboard.current.spaceKey.isPressed && isGrounded == true)
        {
            upVal = jumpSpeed;
        }

        if (Keyboard.current.ctrlKey.isPressed && isSliding == false)
        {
            Sliding();
        }

        if (SlideTimer >= SlideCooldown && isSliding == true)
        {
            isSliding = false;
            Debug.Log("Slide Done");
            slideVal = 0;
        }

        if (yPos > rbPlayer.gameObject.transform.position.y)
        {
            isGrounded = false;
        }

        xPos = rbPlayer.gameObject.transform.position.x;
        yPos = rbPlayer.gameObject.transform.position.y;
        zPos = rbPlayer.gameObject.transform.position.z;
    }

    private void FixedUpdate()
    {
        rbPlayer.transform.position = Vector3.MoveTowards(rbPlayer.transform.position, dirPos.transform.position, forwardVal);
        rbPlayer.transform.position = Vector3.MoveTowards(rbPlayer.transform.position, RPos.transform.position, rightVal);
        if(upVal != 0.0f)
        {
            rbPlayer.AddForce(0, upVal, 0, ForceMode.Impulse);
            //rbPlayer.transform.position = Vector3.MoveTowards(rbPlayer.transform.position, UPos.transform.position, upVal);
            isGrounded = false;
        }

        forwardVal = 0.0f;
        rightVal = 0.0f;
        upVal = 0.0f;
    }

    void Sliding()
    {
        slideVal = slideSpeed;
        Vector3 dir =- (rbPlayer.transform.position - dirPos.transform.position).normalized;
        Vector3 dirfinal = dir * slideVal;
        rbPlayer.AddForce(dirfinal, ForceMode.Impulse);
        SlideTimer = 0.0f;
        isSliding = true;
        Debug.Log("Just slided, slid? slode? " + slideVal);

        slideVal = 0.0f;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
}
