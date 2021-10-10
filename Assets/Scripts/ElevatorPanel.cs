using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] MeshRenderer _LEDMeshRenderer;
    [SerializeField] int _CoinsRequired = 4;
    [SerializeField] Elevator _target;

    private void Awake()
    {
        if(_target == null)
        {
            Debug.LogError("The target elevator has not been set");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // get the number of coins on the player
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError(gameObject.name + " couldn't find the Player script");
            }
            
            if (Input.GetButton("Fire1"))
            {
                if (player.CoinAmount >= _CoinsRequired)
                {
                    if (_LEDMeshRenderer == null)
                    {
                        Debug.LogError("Something went wrong with getting the LED Mesh Renderer");
                    }
                    else
                    {
                        _LEDMeshRenderer.material.color = Color.green;

                        // call elevator
                        _target.CallElevator();
                    }
                }
                else
                {
                    Debug.Log("Not enough coins");
                }
            }
        }
    }

}
