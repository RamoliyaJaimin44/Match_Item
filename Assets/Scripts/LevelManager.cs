using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int currentLevel = 1;
    public GameObject[] allItemPrefabs;
    public ShelfSpawner shelfSpawner;
    public TextMeshProUGUI levelText;

    private bool isTransitioning = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetGame();
        StartLevel(currentLevel);
    }

    public void ResetGame()
    {
        currentLevel = 1;
        isTransitioning = false;
        Time.timeScale = 1;
    }

    public void StartLevel(int level)
    {
        isTransitioning = false;

        List<GameObject> levelItems = new List<GameObject>();

        int uniqueCount = GetUniquePairsForLevel(level);

        if (uniqueCount > allItemPrefabs.Length)
        {
            uniqueCount = allItemPrefabs.Length;
        }

        for (int i = 0; i < uniqueCount; i++)
        {
            GameObject prefab = allItemPrefabs[i];

            levelItems.Add(prefab);
            levelItems.Add(prefab);
            levelItems.Add(prefab);
        }

        Shuffle(levelItems);

        shelfSpawner.SetLevelItems(levelItems);

        levelText.text = "Level: " + level;
    }

    private int GetUniquePairsForLevel(int level)
    {
        if (level == 1) return 5;
        if (level == 2) return 10;
        if (level >= 3) return 15;

        return 10 + (level - 1) * 10;
    }

    public void LevelCompleted()
    {
        if (isTransitioning) return;

        isTransitioning = true;

        currentLevel++;

        StartCoroutine(StartNextLevelDelayed());
    }

    IEnumerator StartNextLevelDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        StartLevel(currentLevel);
    }

    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            GameObject temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }
}
