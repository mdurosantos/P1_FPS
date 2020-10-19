using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Reload
{
    private static bool reloading = false;

    public static bool Reloading()
    {
        return reloading;
    }

    public static void ChangeReloadState()
    {
        reloading = !reloading;
    }
}
