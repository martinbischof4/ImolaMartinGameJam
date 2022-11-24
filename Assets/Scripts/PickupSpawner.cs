using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _pickupPositions;
    [SerializeField]
    private GameObject _pickupPrefab;

    private int _previousPosIndex = -1;

    public void GenerateNewPickup()
    {
        int pickupPosIndex;
        do
        {
            pickupPosIndex = Random.Range(0, _pickupPositions.Count);
        } while (pickupPosIndex == _previousPosIndex);

        _previousPosIndex = pickupPosIndex;

        Instantiate(_pickupPrefab, _pickupPositions[pickupPosIndex], Quaternion.identity, transform);
    }
}
