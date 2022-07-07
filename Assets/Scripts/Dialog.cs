using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] TMP_Text _titleText;
    [SerializeField] TMP_Text _contentText;

    public void UpdateDialog(string title, string content) {
        if (_titleText) _titleText.text = title;
        if (_contentText) _contentText.text = content;
    }

    public void Show(bool isShow) {
        gameObject.SetActive(isShow);
    }

}
