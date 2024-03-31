using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkUpdateEvent : GameEvent
{
    [SerializeField] private string bookmarkName; 
    private Book book;

    public override void Init()
    {
        book = FindObjectOfType<Book>();
    }

    public override void Invoke()
    {
        book.AddBookmark(bookmarkName);
    }
}
