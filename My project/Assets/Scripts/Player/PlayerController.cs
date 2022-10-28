using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private Rigidbody2D rb;
    private Button button;
    private Animator anim;
    private Collider2D col;

    public float PlayerSpeed;
    float horizontalMove = 0f;
    private bool m_FacingRight = true;
    public bool automove = false;
    [HideInInspector] public bool mobileOS;

    [Header("Jump")]
    [Space]
    [Range(1, 10)]
    public float jumpVel;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    [SerializeField] private bool isPressed;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    public float checkRadious;
    public LayerMask whatIsGround;
    [SerializeField]
    private GameObject standingPlatform;
    [Header("Effect")]
    [Space]
    [SerializeField] GameObject m_RunStopDust;
    [SerializeField] GameObject m_JumpDust;
    [SerializeField] GameObject m_LandingDust;
    private AudioManager_PrototypeHero audioManager;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = AudioManager_PrototypeHero.instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (!mobileOS)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * PlayerSpeed;
        }
        else
        {
            horizontalMove = joystick.Horizontal * PlayerSpeed;
        }

        if (automove)
            horizontalMove = PlayerSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadious, whatIsGround);
        anim.SetBool("Grounded", isGrounded);


        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || joystick.Vertical < -0.5f)
        {
            if (standingPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }


        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && (mobileOS ? !isPressed : !Input.GetButton("Jump")))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        anim.SetFloat("AirSpeedY", rb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = Vector2.up * jumpVel;
            anim.SetTrigger("Jump");
        }
    }

    public void OnPointerDown()
    {
        Jump();
        isPressed = true;
    }
    public void OnPointerUp()
    {
        isPressed = false;
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D[] platformCollider = standingPlatform.GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D platformCol in platformCollider)
        {
            Physics2D.IgnoreCollision(col, platformCol);
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreCollision(col, platformCol, false);
        }

    }

    public void Move(float speed)
    {
        rb.position += new Vector2(speed, 0f) * Time.deltaTime;

        if (speed > 0 && !m_FacingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (speed < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("a");
        if (other.gameObject.CompareTag("Platform"))
        {
            standingPlatform = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        print("b");
        if (other.gameObject.CompareTag("Platform"))
        {
            standingPlatform = null;
        }
    }

    private void FixedUpdate()
    {
        Move(horizontalMove);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void SpawnDustEffect(GameObject dust, float dustXOffset = 0)
    {
        int facingDirection = m_FacingRight ? 1 : -1;
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * facingDirection, 0.0f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(facingDirection, 1, 1);
        }
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_runStop()
    {
        audioManager.PlaySound("RunStop");
        // Spawn Dust
        float dustXOffset = 0.6f;
        SpawnDustEffect(m_RunStopDust, dustXOffset);
    }

    void AE_footstep()
    {
        audioManager.PlaySound("Footstep");
    }

    void AE_Jump()
    {
        audioManager.PlaySound("Jump");
        // Spawn Dust
        SpawnDustEffect(m_JumpDust);
    }

    void AE_Landing()
    {
        audioManager.PlaySound("Landing");
        // Spawn Dust
        SpawnDustEffect(m_LandingDust);
    }
}
