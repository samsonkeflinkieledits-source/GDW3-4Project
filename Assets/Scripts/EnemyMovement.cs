
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody enemRB;
    public float enemSpeed = 1.0f;
    public int enemDam = 20;
    public int enemHP = 30;

    public Rigidbody playerRB;
    public PlayerMovement playerMove;
    public float yPos = 0.0f;
    public Material mat;

    // Start is called once before the first execution of Update
    void Start()
    {
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        mat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yPos = enemRB.gameObject.transform.position.y;

        Vector3 temp = Vector3.MoveTowards(
            enemRB.gameObject.transform.position,
            playerRB.gameObject.transform.position,
            enemSpeed * Time.fixedDeltaTime
        );

        enemRB.gameObject.transform.position = new Vector3(temp.x, yPos, temp.z);

        enemRB.transform.LookAt(new Vector3(
            playerRB.gameObject.transform.position.x,
            yPos,
            playerRB.gameObject.transform.position.z
        ));
    }

    // Enemy takes damage from player
    public void EnemyHit(int damage)
    {
        enemHP -= damage;
        mat.SetFloat("_ShowDecal", 1.0f);

        Debug.Log("Enemy hit for " + damage +
                  " damage. Remaining HP: " + enemHP);

        if (enemHP <= 0)
        {
            Debug.Log("Enemy Defeated!");
            Destroy(gameObject);
        }
    }

    // Enemy damages player on contact
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contacted Player");
            playerMove.PlayerHit(enemDam);
        }
    }
}
