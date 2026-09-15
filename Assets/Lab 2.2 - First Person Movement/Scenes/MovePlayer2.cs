using UnityEngine;

public class MovePlayer2 : MonoBehaviour
{
	public CharacterController Cc;
	public Transform cameraTransform;
	public float Gravity;
	public float WalkSpeed;
	public float JumpSpeed;

	private float yspeed;
	private Vector3 startPosition;
	private bool canDoubleJump;

	private void Start()
	{
		startPosition = transform.position;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) );
		cameraTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));


		if (Cc.isGrounded)
		{
			yspeed = -1;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				yspeed = JumpSpeed;
			}
			canDoubleJump = true;
		}
		else
		{
			if (canDoubleJump && Input.GetKeyDown(KeyCode.Space)) // canDoubleJump
			{
				yspeed = JumpSpeed;
				canDoubleJump = false;
			}

			if (yspeed > 0)
			{
				if (Input.GetKeyUp(KeyCode.Space))
				{
					yspeed *= 0.1f;
				}
			}

			yspeed += Gravity * Time.deltaTime;
		}

		Vector3 move = Vector3.zero;
		// Apply walk vector
		move += Input.GetAxis("Vertical") * transform.forward;
		move += Input.GetAxis("Horizontal") * transform.right;
		move = move.normalized * WalkSpeed;
		// Apply gravity
		move += new Vector3(0, yspeed, 0);

		Cc.Move(move * Time.deltaTime);

		HandleRaycasting();
	}

	/// <summary>
	/// Do raycast stuff
	/// </summary>
	private void HandleRaycasting()
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			// Raycast
			if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit))
			{
				Debug.Log(hit.collider.gameObject.name);
				Debug.DrawLine(cameraTransform.position + new Vector3(0, -0.1f, 0), hit.point, Color.red, 1f);
				// Hit button?
				Button hitButton = hit.collider.gameObject.GetComponent<Button>();
				if (hitButton != null)
				{
					hitButton.Press();
				}
			}
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		ResetTrigger hitTrigger = hit.gameObject.GetComponent<ResetTrigger>();
		if (hitTrigger != null)
		{
			transform.position = startPosition;
			yspeed = -1;
		}
	}
}

