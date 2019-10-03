using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xsolla.Store;

public class InventoryItemContainer : MonoBehaviour, IContainer
{
	[SerializeField]
	GameObject itemPrefab;

	[SerializeField]
	Transform itemParent;

	List<InventoryItemUI> _items;
	
	StoreController _storeController;

	void Awake()
	{
		_items = new List<InventoryItemUI>();
		
		_storeController = FindObjectOfType<StoreController>();
	}

	public void AddItem(InventoryItem itemInformation)
	{
		var newItem = Instantiate(itemPrefab, itemParent);
		InventoryItemUI inventoryItemUI = newItem.GetComponent<InventoryItemUI>();
		inventoryItemUI.Initialize(itemInformation);
		_items.Add(inventoryItemUI);
	}

	public void Refresh()
	{
		ClearInventoryItems();

		foreach (var item in _storeController.inventory.items)
		{
			AddItem(item);
		}
	}
	
	void ClearInventoryItems()
	{
		foreach (var item in _items)
		{
			Destroy(item.gameObject);
		}
		
		_items.Clear();
	}

	List<IItemSelection> IContainer.GetItems()
	{
		return _items.OfType<IItemSelection>().ToList();
	}
}