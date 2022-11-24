using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    PlayerController player;
    [SerializeField]
    private float speed;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (player.movingLeft)
            transform.Translate(Vector2.right * speed);
        else if(player.movingRight)
            transform.Translate(Vector2.left * speed);
    }
}
