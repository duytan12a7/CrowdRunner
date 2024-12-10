using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    private static RoadManager instance;
    public static RoadManager Instance => instance;

    [Header(" Elements ")]
    public RoadChunk initialChunk;
    public RoadChunk finishChunk;
    private RoadChunk previousChunk;
    public LevelSequence[] levelSequences;

    [Header(" Settings ")]
    private Vector3 finishPos;
    private Vector3 spawnPos;
    private Transform finishLine;
    List<RoadChunk> levelChunks = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnLevel();

        finishLine = GameObject.FindWithTag(Global.FINISH_TAG).transform;
    }

    private void SpawnLevel()
    {
        ClearLevel();

        levelChunks.Clear();

        spawnPos = Vector3.zero;

        int currentLevel = GetIntLevel();

        if (currentLevel >= levelSequences.Length)
            SpawnLevelSequence(Random.Range(0, levelSequences.Length));
        else
            SpawnLevelSequence(currentLevel);
    }

    private void SpawnLevelSequence(int currentLevel)
    {
        for (int i = 0; i < levelSequences[currentLevel].chunks.Length; i++)
        {
            RoadChunk chunkToSpawn = levelSequences[currentLevel].chunks[i];
            Instantiate(chunkToSpawn, spawnPos, Quaternion.identity, transform);

            spawnPos.z += chunkToSpawn.length;
            previousChunk = chunkToSpawn;
            levelChunks.Add(chunkToSpawn);
        }

        // We can then spawn the finish chunk
        Instantiate(finishChunk, spawnPos, Quaternion.identity, transform);

        levelChunks.Add(finishChunk);

        // Store the finish pos for progression use
        finishPos = spawnPos;
    }


    private void ClearLevel()
    {
        while (transform.childCount > 0)
        {
            Transform t = transform.GetChild(0);
            t.SetParent(null);
            Destroy(t.gameObject);
        }
    }

    public Vector3 GetFinishLinePosition() => finishPos;

    public float GetFinishLineZ() => finishLine.position.z;

    public static Vector3 GetFinishPosition() => instance.GetFinishLinePosition();

    public int GetIntLevel() => PlayerPrefs.GetInt("level", 0);

}

[System.Serializable]
public struct LevelSequence
{
    public RoadChunk[] chunks;
}
