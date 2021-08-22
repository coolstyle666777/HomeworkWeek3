using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private GameObject _controlsText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            _controlsText.SetActive(!_controlsText.activeSelf);
        }
    }

    public void ShowWinScreen(bool trueEnd)
    {
        _winPanel.SetActive(true);
        if (trueEnd)
        {
            _winText.text = "You win! Now you are free!";
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void SetCoinsText(int collected, int needed)
    {
        _coinsText.text = "Coins " + collected + "/" + needed;
    }

    public void SetDistanceText(float distance)
    {
        _distanceText.text = Mathf.RoundToInt(distance) + "m";
    }

    public void SetInfoText(int engine, int liftForce)
    {
        _infoText.text = $"Engine:{engine}%\r\nLiftForce:{liftForce}%";
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}