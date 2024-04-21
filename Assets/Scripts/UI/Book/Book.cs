using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine; 
using UnityEngine.UI;


public class Book : MonoBehaviour
{
    private Animator anim;
    public Action onClosed;
    public Action onOpened;

    [SerializeField] private InventoryWindow inventoryPanel;
    [SerializeField] private TMP_Text notesText;
    [SerializeField] public List<Bookmark> bookmarks;
    [SerializeField] private RectTransform bookmarkPanel;
    [SerializeField] private Animator diaryButtonAnim;

    Dictionary<string, TMP_Text> tasks=new Dictionary<string, TMP_Text>();
    [SerializeField] private TMP_Text taskPrefab;
    [SerializeField] private RectTransform taskPanel;

    public Bookmark lastbookmark;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private Button diaryButton;


    public void Init(Player player, ArrowsManager arrowsManager)
    {
        anim = GetComponent<Animator>();

        onOpened += player.EnterIn<InactionState>;
        onOpened += inventoryPanel.UpdateItemsList;
        onOpened += arrowsManager.DisableAllArrows;

        onClosed += arrowsManager.ReUpdateArrows;
        onClosed += player.EnterIn<IdleState>;
        
        lastbookmark = bookmarks.Last();

        audioPlayer = GetComponent<AudioSource>();
        diaryButton= diaryButtonAnim.GetComponent<Button>();
        InitBookmarks();

    }

    public void AddBookmark(string newBookmarkName)
    {
        Bookmark bookmark = Instantiate(Resources.Load("bookmark"), parent: bookmarkPanel).GetComponent<Bookmark>();
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

    public void AddTask(List<string> taskNames)
    {
        for(int i = 0; i < taskNames.Count; i++)
        {
            TMP_Text newTask = Instantiate(taskPrefab, parent: taskPanel);
            newTask.text = "•" + taskNames[i];
            tasks.Add(taskNames[i], newTask);
        }
       
    }

    public void RemoveTask(string taskName)
    {
        TMP_Text toDestroy;
        tasks.TryGetValue(taskName, out toDestroy);
         
       
        tasks.Remove(taskName);
        Destroy(toDestroy.gameObject);
    }

    public void DisableDiaryButton()
    {
        diaryButton.image.raycastTarget = false;
        //diaryButton.interactable = false;
    }

    public void  EnableDiaryButton()
    {
        diaryButton.gameObject.SetActive(true);
        diaryButton.image.raycastTarget = true;

        //diaryButton.interactable = true;
    }

    public void SetNotesText(string text)
    {
        notesText.text = text;
    }

    public void UpdateLastBookmarkText(string addText)
    {
        diaryButtonAnim.SetTrigger("write"); 
        lastbookmark.UpdateText(addText);
    }

    public void PlaySound(AudioClip sound)
    {
        audioPlayer.clip = sound;
        audioPlayer.Play();
    }

    public async void Close()
    {
        PlaySound(openSound);
        anim.SetTrigger("Close");
        DisableDiaryButton();

        await Task.Delay((int)(anim.GetCurrentAnimatorStateInfo(0).length * 1000));
        onClosed?.Invoke();
        gameObject.SetActive(false);
        EnableDiaryButton();

    }

    public void Open()  
    {
        Debug.Log("Hello from book opening");
        PlaySound(openSound);
        onOpened?.Invoke();
    }




}
