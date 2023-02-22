using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 5f;
    public float followDistance = 10f; // The maximum distance the enemy can follow the player
    public float stopDistance = 2f; // The distance at which the enemy stops following the player
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= followDistance && distanceToPlayer > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
