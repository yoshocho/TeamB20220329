using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TitleSetting : MonoBehaviour
{
    [SerializeField]
    GameObject _titleCanvas;
    [SerializeField]
    GameObject _selectCanvas;
    [SerializeField]
    Button _startButton;
    [SerializeField]
    Button _backButton;

    private void Start()
    {
        _startButton.onClick.AddListener(Select);
        _backButton.onClick.AddListener(Title);
    }
    void Select()
    {
        _titleCanvas.gameObject.SetActive(false);
        _selectCanvas.gameObject.SetActive(true);
    }
    void Title()
    {
        _titleCanvas.gameObject.SetActive(true);
        _selectCanvas.gameObject.SetActive(false);
    }
}
