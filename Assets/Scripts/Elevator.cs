using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 5.0f;

    Vector3 _nextPoint;

    private void Awake()
    {
        _nextPoint = transform.position;
    }

    public void CallElevator()
    {
        if (transform.position == _pointA.position)
        {
            _nextPoint = _pointB.position;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPoint, _speed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButton("Fire1"))
            {
                if (transform.position == _pointB.position)
                    _nextPoint = _pointA.position;
                else if (transform.position == _pointA.position)
                    _nextPoint = _pointB.position;

            }
        }
    }
}
