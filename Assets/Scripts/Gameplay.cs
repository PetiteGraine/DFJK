using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private GameObject[] _letters;
    private int _letterIndex = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highscoreText;
    private int _score;
    private int _highscore;

    [SerializeField] private TextMeshProUGUI _errorsText;
    [SerializeField] private GameObject _playBtn;
    private int _errors = 0;
    private bool _isGameOn = false;

    [SerializeField] private TextMeshProUGUI _feedbackText;
    private Coroutine _feedbackCoroutine;

    private bool _ignoreFirstKeyPress = true;
    
    private void Start()
    {
        _score = 0;
        _errors = 0;

        foreach (var letter in _letters) {
            letter.SetActive(false);
        }
    }

    public void StartGame() {
        _score = 0;
        _errors = 0;
        _scoreText.text = "Score: " + _score;
        _errorsText.text = "Errors: " + _errors;
        _letterIndex = Random.Range(0, _letters.Length);
        _letters[_letterIndex].SetActive(true);
        _isGameOn = true;
    }

    public void EndGame() {
        _letters[_letterIndex].SetActive(false);
        _playBtn.SetActive(true);
        if (_score > _highscore) {
            _highscore = _score;
            _highscoreText.text = "Highscore: " + _highscore;
        }
        _isGameOn = false;
    }

    private void Update()
    {
        if (_ignoreFirstKeyPress && Input.anyKeyDown) {
            _ignoreFirstKeyPress = false;
            return;
        }

        if (Input.anyKeyDown && _isGameOn)
        {
            if (Input.inputString.Length > 0)
            {
                var inputChar = Input.inputString.ToUpper()[0];
                var currentLetter = _letters[_letterIndex];

                if (currentLetter.activeSelf && inputChar == currentLetter.name.ToUpper()[0])
                {
                    _score += 10;
                    _scoreText.text = "Score: " + _score;
                    _feedbackText.text = "Correct!";
                    _feedbackText.color = new Color(193f / 255f, 1f, 64f / 255f);
                }
                else if (currentLetter.activeSelf)
                {
                    _score -= 15;
                    _scoreText.text = "Score: " + _score;
                    _errors += 1;
                    _errorsText.text = "Errors: " + _errors;
                    _feedbackText.text = "Incorrect!";
                    _feedbackText.color = new Color(1f, 83f / 255f, 52f / 255f);
                }

                currentLetter.SetActive(false);

                var availableIndices = new System.Collections.Generic.List<int>();
                for (int i = 0; i < _letters.Length; i++)
                {
                    if (i != _letterIndex)
                    {
                        availableIndices.Add(i);
                    }
                }
                _letterIndex = availableIndices[Random.Range(0, availableIndices.Count)];
                _letters[_letterIndex].SetActive(true);
                ShowFeedbackWithReset(0.5f);
            }
        }
    }

    

    private IEnumerator ShowFeedback(float duration)
    {
        _feedbackText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        _feedbackText.gameObject.SetActive(false);
    }

    public void ShowFeedbackWithReset(float duration)
    {
        if (_feedbackCoroutine != null)
        {
            StopCoroutine(_feedbackCoroutine);
        }
        _feedbackCoroutine = StartCoroutine(ShowFeedback(duration));
    }
}
