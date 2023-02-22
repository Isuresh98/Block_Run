using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float pathfindingDistance = 10f;
    public float moveSpeed = 5f;

    private Transform player;
    private bool pathfindingEnabled = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= pathfindingDistance)
        {
            pathfindingEnabled = true;
        }
        else
        {
            pathfindingEnabled = false;
        }

        if (pathfindingEnabled)
        {
            // Do pathfinding logic here and move towards player
            // using moveSpeed variable
        }
        else
        {
            // Disable pathfinding and just move towards player
            // using moveSpeed variable
        }
    }
}
