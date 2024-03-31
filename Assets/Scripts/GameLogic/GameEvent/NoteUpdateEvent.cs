using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteUpdateEvent : GameEvent
{
    [SerializeField,TextArea(5,20)] private string newNote;
    [SerializeField]private Book book;

    public override void Init()
    { 
         book=FindObjectOfType<Book>();
    }

    public override void Invoke()
    { 
        book.lastbookmark.UpdateText(newNote);
    }
}
