
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

    public float HealthMax = 100.0f;
    float healthCurr;

    // Attack Variables
    public int attackDamage = 25;
    public float attackRange = 2.0f;
    public float attackCooldown = 0.5f;
    public float attackRadius = 0.75f;

    private float attackTimer = 0.0f;


    // Start is called once before the first execution of Update
    void Start()
    {
        healthCurr = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        SlideTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;

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

        if (Keyboard.current.sKey.isPressed)
        {
            if (isGrounded == false)
            {
                forwardVal = -airSpeed;
            }
            else if (isGrounded == true)
            {
                forwardVal = -playerSpeed;
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

        if (Keyboard.current.ctrlKey.isPressed && isSliding == false && isGrounded == true)
        {
            Sliding();
        }

        // Attack Input - Left Mouse Button
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (attackTimer >= attackCooldown)
            {
                Attack();
                attackTimer = 0.0f;
            }
        }

        if (SlideTimer >= SlideCooldown && isSliding == true)
        {
            isSliding = false;
            cam.transform.localPosition = new Vector3(0, 0.5f, 0);
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
        rbPlayer.transform.position = Vector3.MoveTowards(
            rbPlayer.transform.position, dirPos.transform.position, forwardVal);

        rbPlayer.transform.position = Vector3.MoveTowards(
            rbPlayer.transform.position, RPos.transform.position, rightVal);

        if (upVal != 0.0f)
        {
            rbPlayer.AddForce(0, upVal, 0, ForceMode.Impulse);
            isGrounded = false;
        }

        forwardVal = 0.0f;
        rightVal = 0.0f;
        upVal = 0.0f;
    }

    void Sliding()
    {
        slideVal = slideSpeed;

        Vector3 dir = -(rbPlayer.transform.position - dirPos.transform.position).normalized;
        Vector3 dirfinal = dir * slideVal;

        rbPlayer.AddForce(dirfinal, ForceMode.Impulse);

        SlideTimer = 0.0f;
        cam.transform.localPosition = new Vector3(0, 0.25f, 0);

        isSliding = true;

        Debug.Log("Just slided, slid? slode? " + slideVal);

        slideVal = 0.0f;
    }

    // Attack Method
    void Attack()
    {
        Vector3 attackPosition = rbPlayer.transform.position +
                                 rbPlayer.transform.forward * attackRange;

        Collider[] hitEnemies = Physics.OverlapSphere(
            attackPosition, attackRadius);

        foreach (Collider enemy in hitEnemies)
        {
            // Find EnemyHealth on the enemy or its parent
            EnemyMovement enemyHealth = enemy.GetComponentInParent<EnemyMovement>();

            if (enemyHealth != null)
            {
                enemyHealth.EnemyHit(attackDamage);
                Debug.Log("Enemy Hit!");
            }
        }
    }

    public void PlayerHit(int Damage)
    {
        healthCurr -= Damage;

        Debug.Log("Hit for " + Damage + " " + healthCurr + " remaining");

        if (healthCurr <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    // Visualize Attack Range in Unity Editor
    private void OnDrawGizmosSelected()
    {
        if (rbPlayer != null)
        {
            Gizmos.color = Color.red;

            Vector3 attackPosition = rbPlayer.transform.position +
                                     rbPlayer.transform.forward * attackRange;

            Gizmos.DrawWireSphere(attackPosition, attackRadius);
        }
    }
}
