using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    private PickupManager _pickupManager;

    private Animator _animator;

    [SerializeField]
    UIScore _score;

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

    private bool isGameOver = false;

    public bool movingLeft { get; private set; }
    public bool movingRight { get; private set; }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider= GetComponent<BoxCollider2D>();
        _rigidbody= GetComponent<Rigidbody2D>();

        _pickupManager = FindObjectOfType<PickupManager>();

        _playerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (isGameOver)
        {
            movingLeft= false;
            movingRight= false;
            return;
        }

        Move();
        Jump();
        CheckForGround();
    }

    private void Move()
    {
        movingLeft = false;
        movingRight = false;
        Vector2 input = Vector2.zero;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            input += Vector2.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            input += Vector2.left;
        }
        var ray = Physics2D.BoxCast(transform.position, _collider.size, 0, input, groundedCheckDistance, ~_playerMask);
        if (ray)
        {
            return;
        }
        if (input.x > 0)
        {
            movingRight = true;
            _animator.SetBool("MovingLeft", false);
            _animator.SetBool("MovingRight", true);
        }
        else if (input.x < 0)
        {
            movingLeft = true;
            _animator.SetBool("MovingLeft", true);
            _animator.SetBool("MovingRight", false);
        }
        else
        {
            _animator.SetBool("MovingLeft", false);
            _animator.SetBool("MovingRight", false);
        }
        transform.Translate(input * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if(_canJump && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _score.AddPoint();
            _pickupManager.PickupPickedUp();
        }

        if (collision.gameObject.layer == 7)
        {
            isGameOver = true;
            PlayerPrefs.SetInt("score", _score.score);
            SceneManager.LoadScene("GameOver");
        }
    }
}
