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
                animator.SetBool("isUp", true);
                animator.SetBool("isdoun", false);
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", false);
            }
            else if (Driraxtion.y < 0) // moving down
            {
                animator.SetBool("isUp", false);
                animator.SetBool("isdoun", true);
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", false);
            }
            else if (Driraxtion.x < 0) // moving left
            {
                animator.SetBool("isUp", false);
                animator.SetBool("isdoun", false);
                animator.SetBool("isLeft", true);
                animator.SetBool("isRight", false);
            }
            else if (Driraxtion.x > 0) // moving right
            {
                animator.SetBool("isUp", false);
                animator.SetBool("isdoun", false);
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", true);
            }
        }
        else // no movement
        {
            animator.SetBool("isUp", false);
            animator.SetBool("isdoun", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
        }
    }
}
