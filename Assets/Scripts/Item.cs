using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemID;
    public int spawnIndex;
    public ShelfSpawner spawner;

    private bool selected = false;

    private void OnMouseDown()
    {
        if (selected) return;
        selected = true;

        GameManager.Instance.selectionBar.AddItem(this);

        spawner.ClearSlot(spawnIndex);
    }
}
