using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private float groundedCheckDistance;
    [SerializeField]
    private float moveCheckDistance;

    [SerializeField]
    private bool _canJump = true;
    private LayerMask _playerMask;

    private void Awake()
    {
        _collider= GetComponent<BoxCollider2D>();
        _rigidbody= GetComponent<Rigidbody2D>();
        _playerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        Move();
        Jump();
        CheckForGround();
    }

    private void Move()
    {
        Vector2 input = Vector2.zero;
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            input += Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            input += Vector2.left;
        }
        var ray = Physics2D.BoxCast(transform.position, _collider.size, 0, input, groundedCheckDistance, ~_playerMask);
        if (ray)
        {
            return;
        }

        transform.Translate(input * speed * Time.deltaTime);
        //_rigidbody.velocity = input * speed * Time.deltaTime;
        //_rigidbody.MovePosition((Vector2)transform.position + input * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if(_canJump && Input.GetKeyDown(KeyCode.W))
        {
            _canJump = false;
            _rigidbody.AddForce(Vector2.up * jumpHeight);
        }
    }

    private void CheckForGround()
    {
        var ray = Physics2D.BoxCast(transform.position, _collider.size, 0, Vector2.down, groundedCheckDistance, ~_playerMask);
        _canJump = ray ? true : false;
    }
}
