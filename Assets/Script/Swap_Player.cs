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
    private GameManager gameManager;
    private bool End = false;
    private bool menu;
    public GameState gameState = GameState.Playing;
    private Animator animator;
    private GameObject dastVFX;
    private GameObject endVFX;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();

        dastVFX = GameObject.FindGameObjectWithTag("dastVFX");
        endVFX = GameObject.FindGameObjectWithTag("End");
        dastVFX.SetActive(false);
        endVFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (rb.velocity ==Vector2.zero)
        {
            animator.SetBool("isUp", false);
            animator.SetBool("isdoun", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
           
        }
        else
        {
            
        }



        gameManager.Sheild = Shield;
        if (Shield <= 0)
        {
            Shield = 0;
            print("Game Over");
            gameState = GameState.GameOver;
           
        }
        if (levelUp <= 0&&End)
        {
            levelUp = 0;
            print("Game Win");        
            gameState = GameState.Win;
           

        }

        if (gameState == GameState.GameOver)
        {
            // Game over logic
            rb.velocity = Vector2.zero; // Stop player movement
            Destroy(gameObject, 2f);
            gameManager.Menu(menu = false);
        }
        if (gameState == GameState.Win)
        {
            End =true;
            levelUp = 0;
            rb.velocity = Vector2.zero; // Stop player movement
            endVFX.SetActive(true);
            Destroy(gameObject);
            gameManager.Menu(menu=true);
            
        }



        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                animator.SetBool("isUp", false);
                animator.SetBool("isdoun", false);
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", false);

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
        animator.SetBool("isRight", true);


    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-movementSpeed, 0);
        animator.SetBool("isLeft", true);

    }

    void MoveUp()
    {
        rb.velocity = new Vector2(0, movementSpeed);
        animator.SetBool("isUp", true);
    }

    void MoveDown()
    {
        rb.velocity = new Vector2(0, -movementSpeed);
        animator.SetBool("isdoun", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dastVFX.SetActive(true);
            rb.velocity = Vector2.zero; // Stop movement when finger is lifted off the screen
            animator.SetBool("isUp", false);
            animator.SetBool("isdoun", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
        }
       

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dastVFX.SetActive(false);

            animator.SetBool("isUp", false);
            animator.SetBool("isdoun", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Star"))
        {
            levelUp -= 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("coin"))
        {
            gameManager.CoinCollectamount += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("EndBox"))
        {
            if (levelUp == 0)
            {
                
                End = true;
                
            }
        }
        if (collision.gameObject.CompareTag("EnemyFollow"))
        {
            Shield -= 1;

        }
    }

    public enum GameState
    {
        Playing,
        GameOver,
        Win
    }
}
