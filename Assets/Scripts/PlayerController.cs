using System;
using System.Collections;
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
        Move();
        Jump();
        CheckForGround();

        if(Input.GetKeyDown(KeyCode.Escape))
            GameOver();
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
        var ray = Physics2D.BoxCast(transform.position, _collider.size, 0, input, moveCheckDistance, ~_playerMask);
        if (ray)
        {
            return;
        }
        if (input.x > 0)
        {
            movingRight = true;
        }
        else if (input.x < 0)
        {
            movingLeft = true;
        }

        _animator.SetBool("MovingLeft", movingLeft);
        _animator.SetBool("MovingRight", movingRight);
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
            StartCoroutine(DelayGameOverScreen(1));
        }
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("score", _score.score);
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator DelayGameOverScreen(float seconds)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
        GameOver();
    }
}
