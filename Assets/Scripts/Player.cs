using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = 10.0f;
    [SerializeField] private float _jumpHeight = 15.0f;
    [SerializeField] private Transform _spawnPoint;

    private CharacterController _controller;
    private float _yVelocity;
    private bool _canDoubleJump;
    private bool _canWallJump;
    private int _lives = 3;
    private int _coinAmount;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Vector3 _surfaceNormal;

    public event Action<int> OnPlayerCoinCollected;
    public event Action<int> OnPlayerLivesChange;
    public int CoinAmount { get; private set; }
    
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

        // apply gravity
        if (_controller.isGrounded)
        {
            _canWallJump = false;

            // calculate direction
            _direction = new Vector3(horizontalInput, 0, 0);
            // velocity (direction with speed)
            _velocity = _direction * _speed;
            
            if (Input.GetButtonDown("Jump"))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        } 
        else
        {
            if (Input.GetButtonDown("Jump") && _canDoubleJump && !_canWallJump)
            {
                _canDoubleJump = false;
                _yVelocity = _jumpHeight;
            } 
            else if (Input.GetButtonDown("Jump") && _canWallJump)
            {
                // velocity = surface normal of the wall
                _yVelocity = _jumpHeight;
                _velocity = _surfaceNormal * _speed;
                
            }



            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        // move player
        if (_controller.enabled)
            _controller.Move(_velocity * Time.deltaTime);
    }

    public void CoinCollected()
    {
        CoinAmount++;
       
        if (OnPlayerCoinCollected != null)
            OnPlayerCoinCollected(CoinAmount);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killzone")
        {
            _lives--;

            // trigger event and pass lives amount
            if (OnPlayerLivesChange != null)
                OnPlayerLivesChange(_lives);

            if (_lives <= 0)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Respawn();
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if not grounded and touching a wall
        if (!_controller.isGrounded && hit.transform.tag == "Wall")
        {
            _canWallJump = true;
            _surfaceNormal = hit.normal;
        }
            
    }

    private void Respawn()
    {
        _controller.enabled = false;

        _velocity.y = 0.0f;
        transform.position = _spawnPoint.position;

        StartCoroutine(EnableCharacterController());
    }

    IEnumerator EnableCharacterController()
    {
        yield return new WaitForSeconds(0.5f);
        _controller.enabled = true;
    }
}
