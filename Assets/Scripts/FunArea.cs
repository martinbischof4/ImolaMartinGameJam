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
                FindObjectOfType<EnemyController>().speedIncrement *= 15;
            gameObject.SetActive(false);
        }
    }
}
