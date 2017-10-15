using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonControllerHandler
{
    void moveUp();
    void moveDown();
    void moveRight();
    void moveLeft();
}
public class ButtonController : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    protected IButtonControllerHandler _handler;
    

    public void Init(IButtonControllerHandler handler)
    {
        _handler = handler;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void moveUp()
    {
        if (_handler != null)
        {
            _handler.moveUp();
        }
    }

    public void moveDown()
    {
        if (_handler != null)
        {
            _handler.moveDown();
        }
    }

    public void moveRight()
    {
        if (_handler != null)
        {
            _handler.moveRight();
        }
    }

    public void moveLeft()
    {
        if (_handler != null)
        {
            _handler.moveLeft();
        }
    }
}
