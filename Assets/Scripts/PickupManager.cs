using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    PickupSpawner _generator;

    [SerializeField]
    UIScore _score;

    private void Start()
    {
        GeneratePickup();
    }

    public void PickupPickedUp()
    {
        _score.AddPoint();
        GeneratePickup();
    }

    private void GeneratePickup()
    {
        _generator.GenerateNewPickup();
    }
}
