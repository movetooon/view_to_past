using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHandler : MonoBehaviour
{
    [SerializeField] List<GameTask> Tasks;
   
    private bool waitingForTaskDone = false;
    private int currentTasksCount = 0;
    private int doneTasksCount = 0;
    //private List<bool> taskCache;
    public Action onTasksDone;

    public void Init()
    {
        //taskCache = new List<bool>(Tasks.Count);
        foreach (var task in Tasks)
        {
            task.Init();
        }
    }

    public bool checkTasksDone()
    {
        int tasksDone = 0;
        for(int i = doneTasksCount; i < (doneTasksCount + currentTasksCount); i++)
        {
            Debug.Log(Tasks[i].CheckDone());
            if (Tasks[i].CheckDone() == true) tasksDone++;
        }

        Debug.Log(tasksDone);
        if (tasksDone == currentTasksCount)
        {
            for (int i = doneTasksCount; i < (doneTasksCount + currentTasksCount); i++)
            {
                Tasks[i].Complete();
            }
            return true;
        }
        return false;
    }

    public void SetCurrentTasksCount(int tasksCount)
    {
        currentTasksCount= tasksCount;
    }
}
