using UnityEngine;
using UnityEngine.UI;

public class AnyKeyButton : MonoBehaviour
{
    [SerializeField] private Button _btn;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            TriggerButtonAction();
        }
    }

    private void TriggerButtonAction()
    {
        _btn.onClick.Invoke();
    }
}