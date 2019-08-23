﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsMethod
{
    private static ToolsMethod _Instance;
    public static ToolsMethod Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new ToolsMethod();
            return _Instance;
        }
    }

    public Transform FindChildByName(Transform currentTF, string childName)
    {
        Transform childTF = currentTF.Find(childName);
        if (childTF != null)
            return childTF;
        for (int i = 0; i < currentTF.childCount; i++)
        {
            childTF = FindChildByName(currentTF.GetChild(i), childName);
            if (childTF != null)
                return childTF;
        }
        return null;
    }
}
