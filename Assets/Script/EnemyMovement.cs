using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyMovement : MonoBehaviour
{
    public AIPath aiPath;
    // Start is called before the first frame update
    private Animator animator;

    Vector2 Driraxtion;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceVelocity();
    }

    void FaceVelocity()
    {
        Driraxtion = aiPath.desiredVelocity;

       // transform.right = Driraxtion;

        // set the animation movement
        if (Driraxtion.magnitude > 0.1f) // check if there is any movement
        {
            if (Driraxtion.y > 0) // moving up
            {
                
            }
            else if (Driraxtion.y < 0) // moving down
            {
               
            }
            else if (Driraxtion.x < 0) // moving left
            {
                
            }
            else if (Driraxtion.x > 0) // moving right
            {
               
            }
        }
        else // no movement
        {
            
        }
    }
}
