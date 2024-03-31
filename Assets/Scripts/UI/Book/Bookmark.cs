using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bookmark : MonoBehaviour
{
    [SerializeField] private TMP_Text bookmarkName;
    [SerializeField] private TMP_Text note;
    [TextArea(5, 40)] public string bookmarkText;

    public  Action<string> onPressed;
    

    public void Init(Book book)
    {
        onPressed += book.SetNotesText; 
    }

    public void UpdateText(string addText)
    {
        bookmarkText += addText;
    }

    public void Press()
    {
        onPressed?.Invoke(bookmarkText);
    }

    public void SetName(string name)
    {
         bookmarkName.text = name;
    }
}
