using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class Book : MonoBehaviour
{
    private Animator anim;
    public Action onClosed;
    public Action onOpened;
   
    [SerializeField] private InventoryWindow inventoryPanel;
    [SerializeField] private TMP_Text notesText;
    [SerializeField] public List<Bookmark> bookmarks;
    [SerializeField] private RectTransform bookmarkPanel;

    public Bookmark lastbookmark;
    

    public void Init(Player player,ArrowsManager arrowsManager)
    {
        anim = GetComponent<Animator>();

        onOpened += player.EnterIn<InactionState>;
        onOpened += inventoryPanel.UpdateItemsList;
        onOpened += arrowsManager.DisableAllArrows;

        onClosed += arrowsManager.ReUpdateArrows;
        onClosed += player.EnterIn<IdleState>;
        lastbookmark = bookmarks.Last();

        InitBookmarks();

    }

    public void AddBookmark(string newBookmarkName)
    {
        Bookmark bookmark = Instantiate(Resources.Load("bookmark"),parent:bookmarkPanel).GetComponent<Bookmark>();
        bookmark.SetName(newBookmarkName);
        bookmark.Init(this);
        lastbookmark = bookmark;
        bookmarks.Add(bookmark);

    }

    public void InitBookmarks()
    {
        foreach (Bookmark bookmark in bookmarks)
        {
            bookmark.Init(this);
        }
    }
     

    public void SetNotesText(string text)
    {
        notesText.text = text;
    }
    
    public void UpdateLastBookmarkText(string addText)
    {
        lastbookmark.UpdateText(addText);
    }

    public async void Close()
    {
        anim.SetTrigger("Close");
         
        await Task.Delay((int)(anim.GetCurrentAnimatorStateInfo(0).length*1000));
        onClosed?.Invoke();
        gameObject.SetActive(false); 
             
    }

    public void Open() => onOpened?.Invoke();




}
