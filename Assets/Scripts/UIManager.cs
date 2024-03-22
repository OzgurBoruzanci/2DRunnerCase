using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    private void OnEnable()
    {
        EventManager.GameOver += GameOver;
    }
    private void OnDisable()
    {
        EventManager.GameOver -= GameOver;
    }
    public void GameStart()
    {
        _panel.SetActive(false);
        GameManager.Instance.PlayGame();
    }

    private void GameOver()
    {
        _panel.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
    }
}
