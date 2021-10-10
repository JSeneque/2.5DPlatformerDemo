using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    [SerializeField] float _speed;

    private Vector3 _nextPoint;

    private void Start()
    {
        _nextPoint = _pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate next position
        if (transform.position == _pointB.position)
        {
            _nextPoint = _pointA.position;
        }
        else if (transform.position == _pointA.position)
        {
            _nextPoint = _pointB.position;
        }


        transform.position = Vector3.MoveTowards(transform.position, _nextPoint, _speed * Time.deltaTime);
    }
}
