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
    [SerializeField] AudioClip clickSound;


    Book book;
    public  Action<string> onPressed;
    

    public void Init(Book book)
    {
        this.book = book;
        onPressed += book.SetNotesText; 
    }

    public void UpdateText(string addText)
    {
        bookmarkText += addText;
    }

    public void Press()
    {
        book.PlaySound(clickSound);
        onPressed?.Invoke(bookmarkText);
    }

    public void SetName(string name)
    {
         bookmarkName.text = name;
    }
}
