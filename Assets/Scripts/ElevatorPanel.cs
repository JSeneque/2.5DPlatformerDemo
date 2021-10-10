using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] GameObject _LED;

    private MeshRenderer _LEDMeshRenderer;

    private void Awake()
    {
        _LEDMeshRenderer = _LED.GetComponent<MeshRenderer>();

        if (_LEDMeshRenderer == null)
        {
            Debug.LogError("Something went wrong with getting the LED Mesh Renderer");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _LEDMeshRenderer.material.color = Color.green;
            }
                
        }
    }

}
