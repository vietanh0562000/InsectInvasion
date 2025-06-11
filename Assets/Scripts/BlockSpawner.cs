using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {
    public static BlockSpawner _instance;

    [SerializeField] GameObject[] blockPrefabs;

    public int blocksToSpawn, blocksDestroyed;
    public float timeBetweenSpawns = 1f;

    public List<Block> blocks;

    private void Awake() {
        _instance = this;
    }

    public void StartWave() {
        blocksToSpawn = Mathf.Min(15 + LevelManager._instance.currentLevel * 3, 40);
        blocksDestroyed = 0;

        blocks = new List<Block>();

        coroutine = StartCoroutine(SpawnCoroutine());
    }

    public Coroutine coroutine;
    IEnumerator SpawnCoroutine() {
        for (int i = 0; i < blocksToSpawn; i++) {
            Spawn();

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void Spawn() {
        GameObject g = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)]);
        g.transform.position = GetBlockPosition();

        var b = g.GetComponent<Block>();
        b.hp = GetHp();

        blocks.Add(b);
    }

    int GetHp() {
        int lvl = LevelManager._instance.currentLevel;
        float multiplier = 1 + lvl / 10f;
        return 3 + (int)Random.Range(lvl * 5 * 0.7f * multiplier, lvl * 5 * multiplier);
    }

    public void BlockDestroyed(Block b, bool planet) {
        blocks.Remove(b);

        blocksDestroyed++;
        LevelManager._instance.SetFill(blocksDestroyed / (float)blocksToSpawn);

        if (planet)
            return;

        if (blocksDestroyed >= blocksToSpawn) {
            LevelManager._instance.LevelCompleted(true);
        }
    }

    Vector3 GetBlockPosition() {
        int n = Random.Range(0, 4);

        float x = 3.9f, y = 6f;

        if (n == 0) {
            //Left side
            return new Vector3(-x, Random.Range(y, -y), 0);
        } else if (n == 1) {
            //Right side
            return new Vector3(x, Random.Range(y, -y), 0);
        } else if (n == 2) {
            //Up side
            return new Vector3(Random.Range(-x, x), y, 0);
        } else {
            //Bottom side
            return new Vector3(Random.Range(-x, x), -y, 0);
        }
    }
}
