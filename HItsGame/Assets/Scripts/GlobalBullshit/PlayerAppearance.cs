using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAppearance
{
    public static GameObject player { get; private set; }

    public static void Init(GameObject player)
    {
        PlayerAppearance.player = player;
    }
}