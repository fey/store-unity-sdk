using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemContainer : MonoBehaviour, IContainer
{
	[SerializeField]
	GameObject itemPrefab;

	[SerializeField]
	Transform itemParent;

	List<ItemUI> _items;

	void Awake()
	{
		_items = new List<ItemUI>();
	}

	public void AddItem(Xsolla.Store.StoreItem itemInformation)
	{
		var item = Instantiate(itemPrefab, itemParent).GetComponent<ItemUI>();
		item.Initialize(itemInformation);
		
		_items.Add(item);
	}

	public List<ItemUI> GetItems()
	{
		return _items;
	}

	public void Refresh()
	{
		foreach (var item in _items)
		{
			item.Refresh();
		}
	}

	List<IItemSelection> IContainer.GetItems()
	{
		return _items.OfType<IItemSelection>().ToList();
	}
}