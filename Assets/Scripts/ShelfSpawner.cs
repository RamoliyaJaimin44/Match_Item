using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ShelfSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Transform> _newspawnPoint;

    private List<GameObject> levelItems;
    private int index = 0;
    private bool hasAddedNewSpawnPoints = false;


    public void SetLevelItems(List<GameObject> items)
    {
        levelItems = items;
        index = 0;

        AddNewSpawnPointsIfNeeded();

        ClearAllSlots();
        SpawnInitial();
    }

    private void AddNewSpawnPointsIfNeeded()
    {
        if (!hasAddedNewSpawnPoints && LevelManager.Instance.currentLevel > 3)
        {
            if (_newspawnPoint != null && _newspawnPoint.Count > 0)
            {
                foreach (Transform newPoint in _newspawnPoint)
                {
                    if (newPoint != null)
                    {
                        spawnPoints.Add(newPoint);
                    }
                }

                hasAddedNewSpawnPoints = true;
            }
        }
    }

    private void ClearAllSlots()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.childCount > 0)
            {
                Destroy(spawnPoint.GetChild(0).gameObject);
            }
        }
    }

    private void SpawnInitial()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (index >= levelItems.Count) return;

            SpawnItem(i);
        }
    }

    private void SpawnItem(int slotIndex)
    {
        if (index >= levelItems.Count) return;

        GameObject prefab = levelItems[index];
        index++;

        GameObject obj = Instantiate(prefab,
            spawnPoints[slotIndex].position,
            Quaternion.identity);

        Item item = obj.GetComponent<Item>();
        item.itemID = prefab.name;
        item.spawner = this;
        item.spawnIndex = slotIndex;

        obj.transform.SetParent(spawnPoints[slotIndex]);
    }

    public void ClearSlot(int slotIndex)
    {
        if (spawnPoints[slotIndex].childCount > 0)
            Destroy(spawnPoints[slotIndex].GetChild(0).gameObject);

        SpawnNewItemInSlot(slotIndex);
    }

    private void SpawnNewItemInSlot(int slotIndex)
    {
        if (index < levelItems.Count)
        {
            SpawnItem(slotIndex);
        }
    }

    private bool AreAllShelfSlotsEmpty()
    {
        foreach (var t in spawnPoints)
        {
            if (t.childCount > 0) return false;
        }
        return true;
    }

    public bool IsLevelComplete()
    {
        return AreAllShelfSlotsEmpty() && index >= levelItems.Count;
    }
}

