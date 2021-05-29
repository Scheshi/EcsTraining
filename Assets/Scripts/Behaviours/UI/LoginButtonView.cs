using System;
using UnityEngine;
using UnityEngine.UI;


public class LoginButtonView : MonoBehaviour
{
    [SerializeField] private InputField _input;
    [SerializeField] private Button _button;
    [SerializeField] private GameManager _manager;

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            if (_manager.TryConnect(_input.text))
            {
                _button.interactable = false;
                Destroy(gameObject);
            }
        });
    }

    private void OnDestroy()
    {
        _input.text = String.Empty;
        _button.onClick.RemoveAllListeners();
    }
}
