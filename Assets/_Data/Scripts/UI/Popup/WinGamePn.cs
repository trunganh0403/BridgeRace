using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGamePn : BasePanel
{
    private static WinGamePn instance;
    public static WinGamePn Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (WinGamePn.instance != null) Debug.LogError("Only 1 WinGamePn allow to exist");
        WinGamePn.instance = this;
    }
}