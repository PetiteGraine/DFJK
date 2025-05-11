using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private bool _isCountdownTimerOn = false;
    private TimeSpan _timePlaying;
    private float _countdownTime;
    [SerializeField] private GameObject _gameplayManager;

    private void Start() {
        _timerText.text = "Time : 00:00.00";
    }

    public void BeginTimer()
    {
        _isCountdownTimerOn = true;
        _countdownTime = 20f;
        StartCoroutine(UpdateTimer());
    }

    private void EndTimer() {
        _isCountdownTimerOn = false;
        _gameplayManager.GetComponent<Gameplay>().EndGame();
    }

    private IEnumerator UpdateTimer() {
        while (_isCountdownTimerOn) {
            _countdownTime -= Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(_countdownTime);
            string timePlayingStr = "Time: " + _timePlaying.ToString("mm':'ss'.'ff");
            _timerText.text = timePlayingStr;
            if (_countdownTime <= 0) {
                EndTimer();
            }
            yield return null;
        }
    }
}
