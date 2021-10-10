using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmpCoinText;
    [SerializeField] private int _coinCounter;

    private void Awake()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerCoinCollected += Player_OnPlayerCoinCollected;
    }

    private void Player_OnPlayerCoinCollected(object sender, System.EventArgs e)
    {
        _coinCounter++;
        _tmpCoinText.text = "Coins " + _coinCounter.ToString();
    }
}
