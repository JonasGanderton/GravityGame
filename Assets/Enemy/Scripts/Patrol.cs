using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private GameObject patrolRoute;
    [SerializeField] private float speed = 1;
    private Transform[] _waypoints;
    private int _currentWaypointIndex;

    private void Awake()
    {
        _waypoints = patrolRoute.GetComponentsInChildren<Transform>();
        // Method above includes parent transform which isn't a patrol waypoint
        _currentWaypointIndex = 1;
    }

    private void FixedUpdate()
    {
        Transform currentWaypoint = _waypoints[_currentWaypointIndex];
        if (Vector2.Distance(transform.position, currentWaypoint.position) < 0.01f)
        {
            transform.position = currentWaypoint.position; // Move final distance towards waypoint
            
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = 1; // Skip 0th transform as it is from parent.
            }
            currentWaypoint = _waypoints[_currentWaypointIndex];
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);
        }
    }
}