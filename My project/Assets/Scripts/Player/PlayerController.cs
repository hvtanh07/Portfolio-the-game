using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
	private Animator anim;
    private Collider2D col;

    public float PlayerSpeed;
    float horizontalMove = 0f;
	private bool m_FacingRight = true;
    public bool automove = false;

    [Header("Jump")]
	[Space]
    [Range(1,10)]
    public float jumpVel;
	public float fallMultiplier;
    public float lowJumpMultiplier;
	[SerializeField] public bool isGrounded;
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
        horizontalMove = Input.GetAxisRaw("Horizontal") * PlayerSpeed;
        if(automove)
            horizontalMove = PlayerSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadious, whatIsGround);
		anim.SetBool("Grounded", isGrounded);

        
        if(Input.GetKeyDown(KeyCode.S)){
            if(standingPlatform != null){
                Debug.Log("Called");
                StartCoroutine(DisableCollision());
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.velocity = Vector2.up * jumpVel;
			anim.SetTrigger("Jump");
		}
        
        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if( rb.velocity.y > 0 && !Input.GetButton("Jump")){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        anim.SetFloat("AirSpeedY", rb.velocity.y);
    }

    private IEnumerator DisableCollision(){
        BoxCollider2D platformCollider = standingPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(col,platformCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(col,platformCollider, false);
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

    private void OnTriggerEnter2D(Collider2D other) {       
        if (other.gameObject.CompareTag("Platform")){
            standingPlatform = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {       
        if (other.gameObject.CompareTag("Platform")){
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
