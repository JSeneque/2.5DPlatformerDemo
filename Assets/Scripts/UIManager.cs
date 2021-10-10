using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmpCoinText;
    [SerializeField] private TextMeshProUGUI _tmpLivesText;
    

    private void Awake()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerCoinCollected += HandleOnPlayerCoinCollected;
        player.OnPlayerLivesChange += HandleOnPlayerLivesChange;

        UpdateCoinsDisplay(0);
    }

    private void HandleOnPlayerLivesChange(int lives)
    {
        UpdateLivesDisplay(lives);
    }

    private void HandleOnPlayerCoinCollected(int coins)
    {
        UpdateCoinsDisplay(coins);
    }


    private void UpdateCoinsDisplay(int coins)
    {
        _tmpCoinText.text = "Coins " + coins.ToString();
    }

    private void UpdateLivesDisplay(int amount)
    {
        _tmpLivesText.text = "Lives " + amount.ToString();
    }
}
