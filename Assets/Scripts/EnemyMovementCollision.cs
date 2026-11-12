using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovementCollision : MonoBehaviour
{
    public Collider trigger;
    public GameObject enem;
    public EnemyMovement enemMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        enemMove = enem.GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            enemMove.Jump();
        }
    }
}
