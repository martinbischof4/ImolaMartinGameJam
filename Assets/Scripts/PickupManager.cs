using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    PickupSpawner _generator;

    private void Start()
    {
        GeneratePickup();
    }

    public void PickupPickedUp()
    {
        GeneratePickup();
    }

    private void GeneratePickup()
    {
        _generator.GenerateNewPickup();
    }
}
