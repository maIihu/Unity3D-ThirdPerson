using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button enterAreaBtn;
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button quitBtn;

    private void Awake()
    {
        enterAreaBtn.onClick.AddListener(OnEnterAreaButtonClick);
        optionBtn.onClick.AddListener(OnButtonOptionClick);
        quitBtn.onClick.AddListener(OnButtonQuitClick);
    }

    private void OnButtonQuitClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void OnButtonOptionClick()
    {
        Debug.Log("Option");
    }

    private void OnEnterAreaButtonClick()
    {
        Debug.Log("EnterAreaButtonClick");
        SceneManager.LoadScene("MainGame");
    }
}
