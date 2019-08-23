using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Character
{
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        hpObj.rotation = mainCamera.rotation;   // Face the main camera
    }

    public override void Death()
    {
        base.Death();
        GameObject.FindGameObjectWithTag("Fire").SetActive(false);
        GameMain.instance.gameOver = true;
    }
}
