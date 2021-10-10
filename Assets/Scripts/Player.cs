using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = 10.0f;
    [SerializeField] private float _jumpHeight = 15.0f;

    private CharacterController _controller;
    private float _yVelocity;
    private bool _canDoubleJump;

    public event EventHandler OnPlayerCoinCollected;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("Character controller componment is missing on the player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // get input
        float horizontalInput = Input.GetAxis("Horizontal");
        // calculate direction
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        // velocity (direction with speed)
        Vector3 velocity = direction * _speed;

        // apply gravity
        if (_controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
            
        } 
        else
        {
            if (Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _canDoubleJump = false;
                _yVelocity = _jumpHeight;
            }

            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        // move player
        _controller.Move(velocity * Time.deltaTime);
    }

    public void CoinCollected()
    {
        if(OnPlayerCoinCollected != null) OnPlayerCoinCollected(this, EventArgs.Empty);
    }
}
