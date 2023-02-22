using UnityEngine;
using Pathfinding;

public class EnemyFollowPath : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float nextWaypointDistance = 3f;

    private Path _path;
    private int _currentWaypoint = 0;
    private Seeker _seeker;
    private Rigidbody2D _rb;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, player.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_path == null) return;

        if (_currentWaypoint >= _path.vectorPath.Count) return;

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        _rb.AddForce(force);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }
    }
}
