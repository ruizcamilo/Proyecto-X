using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private LayerMask _enemies;
    private Vector2 _direction;

    void Update()
    {
        if (Physics.Raycast(transform.position, _direction, 100, _enemies.value))
        {
            Debug.Log("Hit something");
        }

    }

    public void Set(Vector2 position, Vector2 direction, LayerMask enemies)
    {
        transform.position = position;
        _direction = direction;
        _enemies = enemies;
    }
}
