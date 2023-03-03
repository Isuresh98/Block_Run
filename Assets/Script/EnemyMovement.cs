using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyMovement : MonoBehaviour
{
    public AIPath aiPath;
    // Start is called before the first frame update

    Vector2 Driraxtion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceVelocity();
    }

    void FaceVelocity()
    {
        Driraxtion = aiPath.desiredVelocity;

        transform.right = Driraxtion;

       
    }
}
