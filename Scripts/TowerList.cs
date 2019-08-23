using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerList : MonoBehaviour
{
    public Camera mainCamera;
    public Transform atkRange;
    // Start is called before the first frame update
    void Start()
    {
        IconElement[] icons = GetComponentsInChildren<IconElement>();
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].Init(mainCamera, atkRange);
        }
    }
}
