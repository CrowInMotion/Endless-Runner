using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private int playerLives;

    private Animator playerAnimator;
    private WeaponManager weaponManager;
    public GameObject gameMananger;
    private GameManager gameManagerScript;

    public TMP_Text playerLivesGUI;

    //public float defaultSpeed;
    public float movementSpeed;
    public float jumpForce;

    public int maxJumps;

    private int curJumps;
    private float inputHorizontal;

    private float keyboardInput;
    private float buttonInput;
    private bool isButtonJump = false;

    [SerializeField] 
    private float leftDrag = 1.5f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManagerScript = gameMananger.GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
        curJumps = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        //movePlayerLateral();
        jump();
        useWeapon();
        reloadWeapon();

        //Check for keyboard input every frame
        keyboardInput = Input.GetAxisRaw("Horizontal");

        if (playerLives == 1)
        {
            playerAnimator.SetBool("isLastLife", true);
        }

        else
        {
            playerAnimator.SetBool("isLastLife", false);
        }
    }

    private void FixedUpdate()
    {
        float movementInput;

        //Keyboard takes priority when checking if left/right GUI buttons are pressed
        if (keyboardInput != 0)
        {
            movementInput = keyboardInput;
        }
        else
        {
            movementInput = buttonInput;
        }

        //Left drag is applied to player input and movementSpeed
        float horizontalVelocity = (movementInput * movementSpeed) - leftDrag;

        rb.linearVelocity = new Vector2(horizontalVelocity, rb.linearVelocityY);
    }

    public int getRemainingLives()
    {
        return playerLives;
    }

    public void changeLife(int lifeChange)
    {
        //if (lifeChange < 0)
        //{
        //    groanAudioClip.Play();
        //}

        playerLives += lifeChange;
    }

    //private void movePlayerLateral()
    //{
    //    inputHorizontal = Input.GetAxisRaw("Horizontal");

    //    rb.linearVelocity = new Vector2(movementSpeed * inputHorizontal, rb.linearVelocityY);

    //    if (inputHorizontal != 0)
    //    {
    //        //playerAnimator.SetBool("isWalking", true);
    //        //flipPlayerSpire(inputHorizontal);
    //    }
    //    else
    //    {
    //        //playerAnimator.SetBool("isWalking", false);
    //    }
    //}

    //For use when the player uses the onscreen buttons to move
    public void buttonMoveLeft()
    {
        buttonInput = -1f;
    }
    public void buttonMoveRight()
    {
        buttonInput = 1f;
    }
    public void buttonStopMoving()
    {
        buttonInput = 0f;
    }
    public void buttonJump()
    {
        isButtonJump = true;
    }


    private void useWeapon()
    {
        if (Input.GetKeyDown(KeyCode.E) && weaponManager.getIsReloading() == false)
        {
            weaponManager.shootWeapon();
        }
    }

    private void reloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R) && weaponManager.getIsReloading() == false)
        {
            weaponManager.reloadWeapon();
        }
    }

    private void flipPlayerSpire(float ih)
    {
        if (ih > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (ih < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1) || isButtonJump)
        {
            if (curJumps != 0)
            {
                curJumps--;
                //playerAnimator.SetBool("isJumping", true);
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                isButtonJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            curJumps = maxJumps;
            //playerAnimator.SetBool("isJumping", false);
            //playerAnimator.SetBool("isWalking", false);
        }

        //Game is over if player collides with the player killer
        if (collision.gameObject.CompareTag("PlayerKiller"))
        {
            Destroy(this.gameObject);
            gameManagerScript.gameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pistol"))
        {
            weaponManager.equipGun(collision.gameObject);
        }
    }
}
