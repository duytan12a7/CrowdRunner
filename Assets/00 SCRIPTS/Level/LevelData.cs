using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level" , menuName = "Scriptable Objects/New Level")]
public class LevelData : ScriptableObject
{
    public Chunk[] chunks;
}
