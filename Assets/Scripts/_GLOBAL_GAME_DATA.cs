using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GLOBAL_GAME_DATA", order = 1)]
public class _GLOBAL_GAME_DATA : ScriptableObject
{
    public string objectName = "_GLOBAL_GAME_DATA";

    public int level = 0;
    public const int levelCount = 1;
    public bool[] levelSuccess = new bool[levelCount];
}