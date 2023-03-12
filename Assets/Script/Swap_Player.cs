using UnityEngine;
using UnityEngine.UI;
public class Swap_Player : MonoBehaviour
{
    //timer set
   

    [SerializeField] private float currentTime; // The current time left

    private GameObject SheldPannel;


    public int Shield;
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

    private GameObject upTall;
    private GameObject dounTall;
    private GameObject leftTall;
    private GameObject rightTall;

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
    private AudioSource audioSource;
    public AudioClip StarHitSound;
    public AudioClip EnemyHitSound;
    public AudioClip CoinHitSound;
    public AudioClip SheldHitSound;
    public AudioClip BGSound;

    void Start()
    {

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

        //Animation
        upTall = GameObject.FindGameObjectWithTag("UpTall");
        dounTall = GameObject.FindGameObjectWithTag("DounTall");
        leftTall = GameObject.FindGameObjectWithTag("LeftTall");
        leftTall = GameObject.FindGameObjectWithTag("LeftTall");
        rightTall = GameObject.FindGameObjectWithTag("RightTall");
        SheldPannel = GameObject.FindGameObjectWithTag("SheldP");



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
        leftTall.SetActive(false);
        rightTall.SetActive(false);
        upTall.SetActive(false);
        dounTall.SetActive(false);

        SheldPannel.SetActive(false);
        GameOver = false;

        Playanim.SetBool("IsSheld", true);
        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //timer
        currentTime -= Time.deltaTime;
        
        int timeInt = Mathf.RoundToInt(currentTime);
        Shield = timeInt;


        if (currentTime <= 0f)
        {
           

            SheldPannel.SetActive(false);

            // Time is up
            currentTime = 0f;
          
        }

        if (hitCount == 1)
        {
            Playanim.SetBool("IsSheld", false);
            hitCount = 1;
        }

        if (hitCount == 2)
        {
            hitCount = 2;
            GameOver = true;
            Destroy(gameObject, 2f);
        }

        if (levelUp == 3)
        {
            gameManager.starCount(0);


        }
        else if(levelUp == 2)
        {
            gameManager.starCount(1);
           

        }
        else if (levelUp == 1)
        {
            gameManager.starCount(2);
           
            

        }
        else if (levelUp == 0)
        {
            gameManager.starCount(3);
            
            

        }


        LevelcoinText2.text = displayLevelCoin.ToString();

        LevelcoinText.text = displayLevelCoin.ToString();


        
       



        gameManager.Sheild = Shield;
        if (GameOver)
        {
            
            Shield = 0;
            print("Game Over");
            gameState = GameState.GameOver;
            if (gameoverSound != null)
            {
                audioSource.PlayOneShot(gameoverSound);
            }

        }

        if (levelUp <= 0&&End)
        {
            levelUp = 0;
            print("Game Win");        
            gameState = GameState.Win;
            if (GameWinSound != null)
            {
                audioSource.PlayOneShot(GameWinSound);
            }

        }


        if (gameState == GameState.GameOver)
        {
            // Game over logic
            rb.velocity = Vector2.zero; // Stop player movement
            Destroy(gameObject, 5f);
            gameManager.Menu(menu = false);
            DamegeVFX.SetActive(true);
            cameraShake.shakeDuration = 0.7f;
            cameraShake.ShakeCamera();
            camColor.ColorAndIntensity();
        }
        if (gameState == GameState.Win)
        {
            End =true;
            levelUp = 0;
            rb.velocity = Vector2.zero; // Stop player movement
            endVFX.SetActive(true);
            Destroy(gameObject,4f);
            gameManager.Menu(menu=true);
            camColor.ColorAndIntensity();


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

        leftTall.SetActive(true);
        rightTall.SetActive(false);
        upTall.SetActive(false);
        dounTall.SetActive(false);

    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-movementSpeed, 0);
        leftTall.SetActive(false);
        rightTall.SetActive(true);
        upTall.SetActive(false);
        dounTall.SetActive(false);

    }

    void MoveUp()
    {
        rb.velocity = new Vector2(0, movementSpeed);
        leftTall.SetActive(false);
        rightTall.SetActive(false);
        upTall.SetActive(false);
        dounTall.SetActive(true);
    }

    void MoveDown()
    {
        rb.velocity = new Vector2(0, -movementSpeed);
        leftTall.SetActive(false);
        rightTall.SetActive(false);
        upTall.SetActive(true);
        dounTall.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dastVFX.SetActive(true);
            rb.velocity = Vector2.zero; // Stop movement when finger is lifted off the screen
            
            HelthUpVFX.SetActive(false);
            DamegeVFX.SetActive(false);
            StarVFX.SetActive(false);

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

        if (collision.gameObject.CompareTag("Path"))
        {
            Destroy(collision.gameObject);
        }
           
        if (collision.gameObject.CompareTag("Star"))
        {
            
            cameraShake.ShakeCamera();
            levelUp -= 1;
            Destroy(collision.gameObject);
            StarVFX.SetActive(true);
            if (GameWinSound != null)
            {
                audioSource.PlayOneShot(StarHitSound);
            }


        }

        if (collision.gameObject.CompareTag("coin"))
        {
            gameManager.CoinCollectamount += 1;
            Destroy(collision.gameObject);
             displayLevelCoin+= 1;
            if (GameWinSound != null)
            {
                audioSource.PlayOneShot(CoinHitSound);
            }
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
            Playanim.SetBool("IsSheld", false);
            
            DamegeVFX.SetActive(true);
            hitCount++;
            if (GameWinSound != null)
            {
                audioSource.PlayOneShot(EnemyHitSound);
            }


        }

        if (collision.gameObject.CompareTag("HelthUp"))
        {
            Playanim.SetBool("IsSheld", true);
            hitCount = 0;
            SheldPannel.SetActive(true);
            currentTime = 10f;
            Destroy(collision.gameObject);

            HelthUpVFX.SetActive(true);
            if (GameWinSound != null)
            {
                audioSource.PlayOneShot(SheldHitSound);
            }

        }
        
    }
  


   



    public enum GameState
    {
        Playing,
        GameOver,
        Win
    }
}
