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

	protected void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		actions = GetComponent<IActions> ();
		animator = GetComponent<Animator> ();
		dead = false;
		inJump = false;
	}
	
	protected void Update ()
    {
		if (dead) {
			
		} else if (health <= 0 && !dead) {
			Kill ();
		}
			
		animator.SetFloat ("Speed", Mathf.Abs(rb.velocity.x));
		animator.SetFloat ("Vertical_Speed", rb.velocity.y);

		Debug.Log (animator);
	}

	public void Kill() {
		dead = true;
		animator.SetBool ("Dead", true);
		deadTime = Time.time;
		Destroy (this.gameObject, 10.0f);
	}

	public void Move (float direction)
	{
		Vector3 velo = rb.velocity;
		velo.x = direction * maxSpeed;
		rb.velocity = velo;

		if (direction > 0.0f) {
			this.direction = 1.0f;
		} else if (direction < 0.0f) {
			this.direction = -1.0f;
		};

		Vector3 scale = transform.localScale;
		scale.x = this.direction;
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
