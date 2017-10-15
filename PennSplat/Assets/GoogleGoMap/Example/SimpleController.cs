using UnityEngine;
using System.Collections.Generic;

public class SimpleController : MonoBehaviour
{
	public float speed = 6.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;
	public Vector3 lastPosition;
	public List<Vector3> positions;

	void Start(){
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
		lastPosition = controller.transform.position;
	}

	void Update() 
	{
		
		// Use input up and down for direction, multiplied by speed
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;


		// Move Character Controller
		if(moveDirection.magnitude > 0.001)
			controller.Move(moveDirection * Time.deltaTime);

		if (controller.transform.position != lastPosition) {
			positions.Add (controller.transform.position);
			lastPosition = controller.transform.position;
		}

	}
}