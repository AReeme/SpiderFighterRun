using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMovement : MonoBehaviour
{

	[Header("Player Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float crouchHeight = 0.5f;

	[Header("Player Audio")]
	[SerializeField] private AudioSource jumpAudio;
	[SerializeField] private AudioSource crouchAudio;


    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

	//new
	private bool isCrouching = false;

	//new*
	public PlayerInputActions playerControls;
	private InputAction jump;
	private InputAction crouch;

	private void Awake()
	{
		playerControls = new PlayerInputActions();
	}

	//new*
	private void OnEnable()
	{
		jump = playerControls.Jump.JumpAction;
		jump.Enable();
		crouch = playerControls.Crouch.CrouchAction;
		crouch.Enable();

		jump.performed += Jumping;
		crouch.performed += Crouching;
	}

	//New*
	private void OnDisable()
	{
		jump.Disable();
		crouch.Disable();
	}

	private void Update()
	{
		isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

		// Directly check the state of the crouch button
		if (isGrounded && crouch.ReadValue<float>() > 0)
		{
			isCrouching = true;
			GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);
		}
		else
		{
			isCrouching = false;
			GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
		}
	}
	//new functions
	public void Jumping(InputAction.CallbackContext context)
	{
		if (isGrounded)
		{
			isJumping = true;
			rb.velocity = Vector2.up * jumpForce;
		}

		if (isJumping)
		{
			if (jumpTimer < jumpTime)
			{
				rb.velocity = Vector2.up * jumpForce;

				jumpTimer += Time.deltaTime;

				jumpAudio.Play();
			}
			else
			{
				isJumping = false;
			}
		}
		isJumping = false;
		jumpTimer = 0;
	}

	public void Crouching(InputAction.CallbackContext context)
	{
		Debug.Log("Crouching performed: " + context.performed);

		if (context.performed)
		{
			if (isGrounded)
			{
				isCrouching = true;
			}
			crouchAudio.Play();
		}
		else if (context.canceled)
		{
			if (isGrounded)
			{
				Debug.Log("Crouching canceled: " + context.canceled);
				isCrouching = false;
				GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
			}
		}
	}
}
