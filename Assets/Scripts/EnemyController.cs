using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float speed;

    [SerializeField]
    public float speedIncrement;

    private Rigidbody2D _rigidbody2D;

    private PlayerController _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _rigidbody2D= GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        speed += speedIncrement;

        if(speed < 2)
        {
            _spriteRenderer.sprite = sprites[0];
        }
        else if (speed < 4)
        {
            _spriteRenderer.sprite = sprites[1];
        }
        else
        {
            _spriteRenderer.sprite = sprites[2];
        }
    }
}
