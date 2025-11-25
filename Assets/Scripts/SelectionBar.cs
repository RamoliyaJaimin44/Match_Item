using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SelectionBar : MonoBehaviour
{
    public Transform[] slots;
    private List<Item> collected = new List<Item>();

    public void AddItem(Item item)
    {
        int insertIndex = FindInsertPosition(item.itemID);

        collected.Insert(insertIndex, item);

        RearrangeSlots();

        CheckMatch(item.itemID);

        if (collected.Count >= slots.Length && !HasAnyPossibleMatch())
        {
            GameManager.Instance.GameOver();
        }
    }

    private int FindInsertPosition(string itemID)
    {
        int lastMatchIndex = -1;

        for (int i = 0; i < collected.Count; i++)
        {
            if (collected[i].itemID == itemID)
            {
                lastMatchIndex = i;
            }
        }

        if (lastMatchIndex != -1)
        {
            return lastMatchIndex + 1;
        }

        return collected.Count;
    }

    void CheckMatch(string id)
    {
        var sameItems = collected.Where(i => i.itemID == id).ToList();

        if (sameItems.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Item it = sameItems[i];
                collected.Remove(it);
                Destroy(it.gameObject);
            }

            GameManager.Instance.AddScore(1);

            RearrangeSlots();

            CheckIfLevelComplete();
        }
    }

    void RearrangeSlots()
    {
        for (int i = 0; i < collected.Count; i++)
        {
            collected[i].transform.SetParent(slots[i]);
            collected[i].transform.localPosition = Vector3.zero;
        }
    }

    private bool HasAnyPossibleMatch()
    {
        if (collected.Count == 0) return false;

        Dictionary<string, int> itemCounts = new Dictionary<string, int>();

        foreach (var item in collected)
        {
            if (itemCounts.ContainsKey(item.itemID))
            {
                itemCounts[item.itemID]++;
            }
            else
            {
                itemCounts[item.itemID] = 1;
            }
        }

        foreach (var count in itemCounts.Values)
        {
            if (count >= 3)
            {
                return true;
            }
        }

        return false;
    }

    private void CheckIfLevelComplete()
    {
        if (collected.Count == 0 && GameManager.Instance.shelfSpawner.IsLevelComplete())
        {
            LevelManager.Instance.LevelCompleted();
        }
    }
}
