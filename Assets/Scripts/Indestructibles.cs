﻿using UnityEngine;

/**
 * Variables that must stay alive throughout the game's life
 */
public class Indestructibles : MonoBehaviour
{
    public static int LastLevel = 1;
    public static Vector2 respawnPos;
    public static Vector2[] defaultSpawns =
    {
        new Vector2(-14.59f,-6.79f), // Level 1
        new Vector2(25.92f,0.61f) // Level 2
    };
}
