using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
