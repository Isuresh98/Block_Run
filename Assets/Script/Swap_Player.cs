using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Swap_Player : MonoBehaviour
{
    

    //new moment Create
    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isMovingUp;
    private bool isMovingDown;


    [SerializeField] 
    private LayerMask obstacleLayerMask;
    [SerializeField]
    private float offsetinstop;

    private GameObject SheldPannel;

    public int Shield;
    public int levelUp = 3;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

   
    private float movementSpeed = 40f;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private bool End = false;
    private bool menu;
    public GameState gameState = GameState.Playing;
  
    private GameObject dastVFX;
    private GameObject endVFX;
    
    //UI
    int displayLevelCoin;
    public Text LevelcoinText;
    public Text LevelcoinText2;

    private GameObject HelthUpVFX;
    private GameObject DamegeVFX;
    private GameObject StarVFX;

    //animation



    private Animator Playanim;

    //game over
    private bool GameOver;
    [SerializeField]
    private int hitCount;
    public CameraShake cameraShake;
    public CamColorChange camColor;

    //sound Effect
    public AudioClip gameoverSound;
    public AudioClip GameWinSound;
    public AudioClip StarHitSound;
    public AudioClip EnemyHitSound;
    public AudioClip CoinHitSound;
    public AudioClip SheldHitSound;
    public AudioClip BGSound;
    private AudioSource audioSource;

    //adsmanager
    public bool IntasitialAds;
     bool GameWintru;

    //vibration set
    public int vibrateLimit = 3; // Set the limit for number of vibrations
    private int vibrateCounter = 0; // Keep track of number of vibrations


    void Start()
    {
        Vibration.Vibrate();
        IntasitialAds = false;
        GameWintru = false;

        if (Input.touchCount > 0)
        {
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;

        }

            //sound effect
            audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGSound;
        audioSource.loop = true;
        audioSource.Play();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Playanim = GetComponent<Animator>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        camColor = GameObject.FindGameObjectWithTag("PosVolum").GetComponent<CamColorChange>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        SheldPannel = GameObject.FindGameObjectWithTag("SheldP");
        //Animation




        DamegeVFX = GameObject.FindGameObjectWithTag("DamageVFX");
        dastVFX = GameObject.FindGameObjectWithTag("dastVFX");
        StarVFX = GameObject.FindGameObjectWithTag("StarVFX");
        HelthUpVFX = GameObject.FindGameObjectWithTag("HelthUpVFX");
        endVFX = GameObject.FindGameObjectWithTag("End");
        dastVFX.SetActive(false);
        endVFX.SetActive(false);
        DamegeVFX.SetActive(false);
        StarVFX.SetActive(false);


        LevelcoinText.text = displayLevelCoin.ToString();
        LevelcoinText2.text = displayLevelCoin.ToString();
        HelthUpVFX.SetActive(false);

        //animation
       

        SheldPannel.SetActive(false);
        GameOver = false;

        Playanim.SetBool("IsSheld", true);
        hitCount = 0;

        //adsmanage
       
    }


    // Update is called once per frame
    void Update()
    {


        gameManager.Sheild = Shield;

        if (GameOver && GameWintru==false)
        {

            hitCount = 2;
            Shield = 0;
            print("Game Over");
            gameState = GameState.GameOver;


            if (gameoverSound != null)
            {
                audioSource.PlayOneShot(gameoverSound);
            }

        }

        if (gameState == GameState.GameOver)
        {
            // Game over logic
            hitCount = 2;
            movementSpeed = 0;
            Shield = 0;
            rb.velocity = Vector2.zero; // Stop player movement

            DamegeVFX.SetActive(true);
            cameraShake.shakeDuration = 0.7f;
            cameraShake.ShakeCamera();
            camColor.ColorAndIntensity();
            IntasitialAds = true;
            gameManager.Menu(menu =false);

            Destroy(gameObject, 5f);
            // Check if the vibration limit has been reached
            if (vibrateCounter < vibrateLimit)
            {
                // Call the Vibrate function from the Vibration class
                Vibration.Vibrate();

                // Increment the vibration counter
                vibrateCounter++;
            }
        }


        if (levelUp <= 0 && End)
        {
            // Load the next level
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;


            // Set the "Level2" PlayerPrefs value to 1 for the next level
            PlayerPrefs.SetInt("Level" + nextLevelIndex.ToString(), 1);

            levelUp = 0;
            print("Game Win");
            gameState = GameState.Win;
            if (GameWinSound != null)
            {
                audioSource.PlayOneShot(GameWinSound);
            }

        }
        UIandOverCountLogic();

        GameWin();
   
    }

    private void UIandOverCountLogic()
    {
        if (hitCount == 1)
        {
            SheldPannel.SetActive(false);
            Playanim.SetBool("IsSheld", false);
            hitCount = 1;
            
        }

        if (hitCount >= 2)
        {

            GameOver = true;
            hitCount = 2;
            Destroy(gameObject, 2f);

        }

        if (levelUp == 3)
        {
            levelUp = 3;
            gameManager.starCount(0);


        }
        else if (levelUp == 2)
        {
            levelUp = 2;
            gameManager.starCount(1);


        }
        else if (levelUp == 1)
        {
            levelUp = 1;
            gameManager.starCount(2);



        }
        else if (levelUp == 0)
        {
            levelUp = 0;
            gameManager.starCount(3);



        }


        LevelcoinText2.text = displayLevelCoin.ToString();

        LevelcoinText.text = displayLevelCoin.ToString();
    }

    private void GameWin()
    {
        if (gameState == GameState.Win)
        {
            GameWintru = true;
            End = true;
            levelUp = 0;
            movementSpeed = 0;
            rb.velocity = Vector2.zero; // Stop player movement
            endVFX.SetActive(true);
            Destroy(gameObject, 4f);
            gameManager.Menu(menu = true);
            camColor.ColorAndIntensity();


        }
    }
    public void thochGet()
    {
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

                DetectSwipe();
            }
        }

        Vector3 movement = Vector3.zero;


        if (isMovingRight)
        {
            movement += Vector3.right;
        }
        else if (isMovingLeft)
        {
            movement += Vector3.left;
        }

        if (isMovingUp)
        {
            movement += Vector3.up;
        }
        else if (isMovingDown)
        {
            movement += Vector3.down;
        }


        if (movement != Vector3.zero)
        {
            // Check if there is an obstacle in the direction of movement
            Collider2D obstacle = Physics2D.OverlapCircle(transform.position + movement, offsetinstop, obstacleLayerMask);
            if (obstacle == null)
            {
                transform.Translate(movement * movementSpeed * Time.unscaledDeltaTime);
            }
            else
            {
                // Stop moving if there is an obstacle in the way
                isMovingRight = false;
                isMovingLeft = false;
                isMovingUp = false;
                isMovingDown = false;
            }
        }
    }

    private void FixedUpdate()
   {
        thochGet();

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
        isMovingRight = true;
        isMovingLeft = false;
        isMovingUp = false;
        isMovingDown = false;
        
    }

    void MoveLeft()
    {
        isMovingRight = false;
        isMovingLeft = true;
        isMovingUp = false;
        isMovingDown = false;
      
    }

    void MoveUp()
    {
        isMovingRight = false;
        isMovingLeft = false;
        isMovingUp = true;
        isMovingDown = false;
       
    }

    void MoveDown()
    {
        isMovingRight = false;
        isMovingLeft = false;
        isMovingUp = false;
        isMovingDown = true;
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dastVFX.SetActive(true);
           /// rb.velocity = Vector2.zero; // Stop movement when finger is lifted off the screen
            
            HelthUpVFX.SetActive(false);
            DamegeVFX.SetActive(false);
            StarVFX.SetActive(false);

            // Stop moving if the player collides with an object with a collider
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
            print("hit the ground");
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dastVFX.SetActive(false);

            
            
        }
        
       
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //test add
       


        if (collision.gameObject.CompareTag("Path"))
        {
            Destroy(collision.gameObject);
        }
           
        if (collision.gameObject.CompareTag("Star"))
        {
            levelUp -= 1;
            Vibration.Vibrate(150);
           
            audioSource.PlayOneShot(StarHitSound);
            Destroy(collision.gameObject);
            StarVFX.SetActive(true);
            
        }

        if (collision.gameObject.CompareTag("coin"))
        {
            audioSource.PlayOneShot(CoinHitSound);
            gameManager.CoinCollectamount += 1;
            Destroy(collision.gameObject);
             displayLevelCoin+= 1;
           
               
            
        }
        if (collision.gameObject.CompareTag("EndBox"))
        {
            if (levelUp == 0)
            {
                
                End = true;
                Animator anim = collision.gameObject.GetComponent<Animator>();
                anim.SetBool("isEnd", true);
            }
           
        }
        if (collision.gameObject.CompareTag("EnemyFollow"))
        {
            hitCount++;
            audioSource.PlayOneShot(EnemyHitSound);
            Playanim.SetBool("IsSheld", false);
            
            DamegeVFX.SetActive(true);
            
            

        }

        if (collision.gameObject.CompareTag("HelthUp"))
        {
            audioSource.PlayOneShot(SheldHitSound);
            Playanim.SetBool("IsSheld", true);
            hitCount = 0;
            SheldPannel.SetActive(true);
           
            Destroy(collision.gameObject);

            HelthUpVFX.SetActive(true);
           
           
           

        }
        
    }
  

    public enum GameState
    {
        Playing,
        GameOver,
        Win
    }
}
