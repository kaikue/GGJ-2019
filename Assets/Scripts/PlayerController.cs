using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1;

    Animator animator;

    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_INJ = 2;

    string _currentDirection = "left";
    int _currentAnimationState = STATE_IDLE;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                changeDirection("left");
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                changeDirection("right");
            }
            changeState(STATE_WALK);
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
             changeState(STATE_WALK);
        }
        else
        {
            if (true)
            {
                changeState(STATE_IDLE);
            }
            else
            {
                changeState(STATE_INJ);
            }
        }
    }

    void changeState(int state)
    {
        if(_currentAnimationState == state)
            return;

        switch (state)
        {
            case STATE_WALK:
                animator.SetInteger("state", STATE_WALK);
                break;
            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;
            case STATE_INJ:
                animator.SetInteger("State", STATE_INJ);
                break;
        }
        _currentAnimationState = state;


    }

    void changeDirection(string direction)
    {
        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
                transform.Rotate(0, 180, 0);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                transform.Rotate(0, -180, 0);
                _currentDirection = "left";
            }
        }
    }
}
