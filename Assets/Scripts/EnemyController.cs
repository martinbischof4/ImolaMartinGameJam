using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float speedIncrement;

    private Rigidbody2D _rigidbody2D;

    private PlayerController _player;


    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _rigidbody2D= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        speed += speedIncrement;
    }
}
