using UnityEngine;

public class Swap_Player : MonoBehaviour
{
    public int Shield=3;
    public int levelUp = 3;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private float minDistanceForSwipe = 20f;
    [SerializeField]
    private float movementSpeed = 5f;
    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Shield <= 0)
        {
            Shield = 0;
            print("Game Over");
        }
        if (levelUp <= 0)
        {
            levelUp = 0;
            print("Game Win");
        }




        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
               
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                rb.velocity = Vector2.zero; // Stop movement when finger is lifted off the screen
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        if (Vector2.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe)
        {
            Vector2 direction = fingerUpPosition - fingerDownPosition;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
        }
    }

    void MoveRight()
    {
        rb.velocity = new Vector2(movementSpeed, 0);
    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-movementSpeed, 0);
    }

    void MoveUp()
    {
        rb.velocity = new Vector2(0, movementSpeed);
    }

    void MoveDown()
    {
        rb.velocity = new Vector2(0, -movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero; // Stop movement when finger is lifted off the screen
        }
        if (collision.gameObject.CompareTag("EnemyFollow"))
        {
            Shield -= 1;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Star"))
        {
            levelUp -= 1;
            Destroy(collision.gameObject);
        }
    }
}
