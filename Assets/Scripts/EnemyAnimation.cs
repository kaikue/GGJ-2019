﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public float walkSpeed = 1;

    Animator animator;

    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_DEAD = 4;

    string _currentDirection = "left";
    int _currentAnimationState = STATE_IDLE;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

            EnemyActions ea = GetComponentInParent<EnemyActions>();

            if (ea.CanTargetPlayer())
            {
                changeState(STATE_IDLE);
            }
            else
            {
                changeState(STATE_WALK);
            }
            if (ea.bulletSpawnPoint.transform.position.x > ea.player.transform.position.x)
            {
                changeDirection("left");
            }
            else
            {
                changeDirection("right");
            }

    }

    void changeState(int state)
    {
        if (_currentAnimationState == state)
            return;

        switch (state)
        {
            case STATE_WALK:
                animator.SetInteger("state", STATE_WALK);
                break;
            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;
            case STATE_DEAD:
                animator.SetInteger("state", STATE_DEAD);
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
