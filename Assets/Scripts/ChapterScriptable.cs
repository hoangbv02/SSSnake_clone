using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChapterScriptable", menuName = "ScriptableObjects/ChapterScriptable")]
[System.Serializable]
public class Level
{
    public GameObject[] enemyPrefabs;
    public int numberEnemy;
    public int timeSpawn;
}
[System.Serializable]
public class Chapter
{
    public Level[] levels;
}
public class ChapterScriptable : ScriptableObject
{
    public Chapter[] chapters;
    public GameObject[] powerupPrefabs;
}
