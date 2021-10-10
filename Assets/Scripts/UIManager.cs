using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmpCoinText;
    [SerializeField] private TextMeshProUGUI _tmpLivesText;
    [SerializeField] private int _coinCounter;

    private void Awake()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerCoinCollected += Player_OnPlayerCoinCollected;
        player.OnPlayerLivesChange += HandleOnPlayerLivesChange;

        UpdateCoinsDisplay();
    }

    private void HandleOnPlayerLivesChange(int lives)
    {
        UpdateLivesDisplay(lives);
    }

    private void Player_OnPlayerCoinCollected(object sender, System.EventArgs e)
    {
        _coinCounter++;
        UpdateCoinsDisplay();
    }


    private void UpdateCoinsDisplay()
    {
        _tmpCoinText.text = "Coins " + _coinCounter.ToString();
    }

    private void UpdateLivesDisplay(int amount)
    {
        _tmpLivesText.text = "Lives " + amount.ToString();
    }
}
