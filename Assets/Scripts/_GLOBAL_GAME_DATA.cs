using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GLOBAL_GAME_DATA", order = 1)]
public class _GLOBAL_GAME_DATA : ScriptableObject
{
    public string objectName = "_GLOBAL_GAME_DATA";

    public int level = 0;
    public bool levelComplete = false;
    public const int levelCount = 2;
    public bool[] levelSuccess;
}