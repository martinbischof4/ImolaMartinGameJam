using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunArea : MonoBehaviour
{
    [SerializeField]
    private bool veryFunArea;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if(veryFunArea)
            {
                var enemy = FindObjectOfType<EnemyController>();
                enemy.speedIncrement *= 35;
                if (enemy.speed < 2)
                {
                    enemy.speed = 3f;
                }
            }

            gameObject.SetActive(false);
        }
    }
}
