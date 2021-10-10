using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player script missing on Player");
            }
            else
            {
                player.CoinCollected();
            }

            // destroy the coin
            Destroy(this.gameObject);
        }
    }
}
