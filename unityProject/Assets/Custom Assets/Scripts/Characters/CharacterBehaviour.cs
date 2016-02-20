using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{
	public int health;
	public float maxSpeed;
	public float maxHeight;
	public float direction;

	private bool dead;
	private float deadTime;
	private bool inJump;

	private Rigidbody rb;
	private IActions actions;
	private Animator animator;
    private CapsuleCollider cc;
    private SpriteRenderer sr;


	protected void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		actions = GetComponent<IActions> ();
		animator = GetComponent<Animator> ();
        cc = GetComponent<CapsuleCollider>();
        sr = GetComponent<SpriteRenderer > ();
		dead = false;
		inJump = false;
	}
	
	protected void Update ()
    {
		if (dead) {
            Color c = sr.color;
            c.a -= 0.005f;
            sr.color = c;
            rb.velocity = Vector3.up;
            
		} else if (health <= 0 && !dead) {
			Kill ();
        }
			
		animator.SetFloat ("Speed", Mathf.Abs(rb.velocity.x));
		animator.SetFloat ("Vertical_Speed", rb.velocity.y);
	}

	public void Kill() {
		dead = true;
		animator.SetBool ("Dead", true);
        cc.enabled = false;
        
		deadTime = Time.time;
		Destroy (this.gameObject, 10.0f);

		if (tag == "Aventurier") {
			EventManager.AventurierKilled ();
			Debug.Log ("Avent. killed");
		} else if (tag == "Sbire") {
			EventManager.SbireKilled ();
			Debug.Log ("Sbire killed");
		} else {
			Debug.Log ("Unknow killed");
		}
	}

	public virtual string GetTeam() {
		return "Unknow";
	}

    private bool lastDirectionRight=true;
    private bool changeDirection = false;
	public void Move (float direction)
	{
		Vector3 velo = rb.velocity;
		velo.x = direction * maxSpeed;
		rb.velocity = velo;

        changeDirection = false;
		if (direction > 0.0f) {
            if (!lastDirectionRight)
                changeDirection = true;
			this.direction = 1.0f;
            lastDirectionRight = true;
        } else if (direction < 0.0f) {
            if (lastDirectionRight)
                changeDirection = true;
			this.direction = -1.0f;
            lastDirectionRight = false;
        };

		Vector3 scale = transform.localScale;
        if(changeDirection)
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void Jump ()
	{
		if (!inJump) {
			animator.SetTrigger ("Jump");
			inJump = true;
		}
	}

	public void JumpInAnim ()
	{
		Vector3 velo = rb.velocity;
		velo.y += maxHeight;
		rb.velocity = velo;
	}

	public void Land() {
		inJump = false;
	}

	public void Action1 ()
	{
		actions.Action1 ();
	}

	public void Action2 ()
	{
		actions.Action2 ();
	}
}
