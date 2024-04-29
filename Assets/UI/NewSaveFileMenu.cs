using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewSaveFileMenu : MonoBehaviour
{
    public Button okButton;
    public TMP_InputField nameField;
    public GameObject mainMenu;
    private string _playerName;

    void Start()
    {
        _playerName = "";
        nameField.onValueChanged.AddListener(OnValueChanged);
        okButton.onClick.AddListener(ConfirmName);
    }

    void ConfirmName()
    {
        if (_playerName.Length > 3)
        {
            SaveSystem.NewSave(_playerName);
            mainMenu.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnValueChanged(string newValue)
    {
        _playerName = newValue;
    }
}