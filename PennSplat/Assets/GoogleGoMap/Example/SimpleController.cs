using UnityEngine;
using UnityEngine.Networking;

public class SimpleController : NetworkBehaviour, IButtonControllerHandler
{
	public float speed = 6.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;

    public ButtonController buttonControl;

    public Camera camera;

	void Start(){
        if (!isLocalPlayer)
        {
            return;
        }
        // Store reference to attached component
        controller = GetComponent<CharacterController>();

        buttonControl = GameObject.Find("Buttons").GetComponent<ButtonController>();
        if (buttonControl != null)
        {
            buttonControl.Init(this);
        }

	}

    /*
    void Awake()
    {
        if (!isLocalPlayer)
        {
            camera.enabled = false;
        }
    }
    */
    void Awake()
    {
        if (!isLocalPlayer)
        {
            camera.enabled = false;
        }
    }

    public override void OnStartLocalPlayer()
    {
        camera.enabled = true;
    }

    void Update() 
	{

        if (!isLocalPlayer)
        {
            return;
        }


        // Use input up and down for direction, multiplied by speed
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;


        // Move Character Controller
        if (moveDirection.magnitude > 0.001)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }


	}


    void IButtonControllerHandler.moveUp()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        moveDirection = new Vector3(0, 0, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void IButtonControllerHandler.moveDown()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        moveDirection = new Vector3(0, 0, -1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void IButtonControllerHandler.moveRight()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        moveDirection = new Vector3(1, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void IButtonControllerHandler.moveLeft()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        moveDirection = new Vector3(-1, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

}