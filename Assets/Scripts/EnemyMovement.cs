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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yPos = enemRB.gameObject.transform.position.y;
        enemRB.gameObject.transform.position = Vector3.MoveTowards(enemRB.gameObject.transform.position, playerRB.gameObject.transform.position, enemSpeed);
        enemRB.transform.LookAt(new Vector3(playerRB.gameObject.transform.position.x, yPos, playerRB.gameObject.transform.position.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contacted Player");
            playerMove.PlayerHit(enemDam);
        }
    }
}
