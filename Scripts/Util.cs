using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    private static Util _Instance = null;
    public static Util Instance // Need a gemeobject
    {
        get
        {
            if (_Instance == null)
            {
                GameObject obj = new GameObject("Util");
                _Instance = obj.AddComponent<Util>();
            }
            return _Instance;
        }
    }
    public class TimeTask
    {
        public Action callback; 
        public float delayTime; 
        public float destTime; 
        public int count; // Repeat times
    }
    List<TimeTask> timeTaskList = new List<TimeTask>();  
    // Callback in certain time
    public void AddTimeTask(Action _callback, float _delayTime, int _count = 1)
    {
        timeTaskList.Add(new TimeTask()
        {
            callback = _callback,
            delayTime = _delayTime,
            destTime = Time.realtimeSinceStartup + _delayTime,
            count = _count
        });
    }
    private void Update()
    {
        for (int i = 0; i < timeTaskList.Count; i++) 
        {
            TimeTask task = timeTaskList[i];
            if (Time.realtimeSinceStartup >= task.destTime) 
            {
                task.callback?.Invoke();
                if (task.count == 1) // Remove the task if count is 1
                    timeTaskList.RemoveAt(i);
                else if (task.count > 1) // count - 1 if more than 1 counts
                    task.count--;
                task.destTime += task.delayTime;
            }
        }
    }
}

