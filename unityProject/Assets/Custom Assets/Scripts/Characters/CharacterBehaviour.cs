using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour
{
	public int health;
	public float maxSpeed;
	public float maxHeight;
	public float direction;
	public Animator animator;

	private bool dead;
	private float deadTime;

	private Rigidbody rb;
	private IActions actions;


	protected void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		actions = GetComponent<IActions> ();
		dead = false;
	}
	
	protected void Update ()
    {
		if (dead) {
			
		} else if (health <= 0 && !dead) {
			Kill ();
		}

		animator.SetFloat ("Speed", Mathf.Abs(rb.velocity.x));

		Debug.Log (animator);
	}

	public void Kill() {
		dead = true;
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

	public void Jump (float magnitude)
	{
		Vector3 velo = rb.velocity;
		velo.y += magnitude * maxHeight;
		rb.velocity = velo;
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
