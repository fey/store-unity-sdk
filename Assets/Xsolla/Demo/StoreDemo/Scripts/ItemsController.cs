﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xsolla.Core;
using Xsolla.Store;

public class ItemsController : MonoBehaviour
{
	[SerializeField]
	GameObject itemsContainerPrefab;

	[SerializeField]
	GameObject cartContainerPrefab;

	[SerializeField]
	GameObject inventoryContainerPrefab;

	[SerializeField]
	Transform content;
	
	Dictionary<string, GameObject> _containers = new Dictionary<string, GameObject>();

	public void CreateItems(StoreItems items)
	{
		foreach (var item in items.items)
		{
			if (item.groups.Any())
			{
				foreach (var group in item.groups)
				{
					if (!_containers.ContainsKey(group))
					{
						AddContainer(itemsContainerPrefab, group);
					}

					var groupContainer = _containers[group];
					groupContainer.GetComponent<ItemContainer>().AddItem(item);
				}
			}
			else
			{
				if (!_containers.ContainsKey(Constants.UngroupedGroupName))
				{
					AddContainer(itemsContainerPrefab, Constants.UngroupedGroupName);
				}

				var groupContainer = _containers[Constants.UngroupedGroupName];
				groupContainer.GetComponent<ItemContainer>().AddItem(item);
			}
		}

		AddContainer(cartContainerPrefab, Constants.CartGroupName);
		
		AddContainer(inventoryContainerPrefab, Constants.InventoryConatainerName);
	}

	void AddContainer(GameObject itemContainerPref, string containerName)
	{
		var newContainer = Instantiate(itemContainerPref, content);
		newContainer.SetActive(false);
		_containers.Add(containerName, newContainer);
	}

	public List<IItemSelection> GetItemsByGroupId(string groupId)
	{
		if (!_containers.ContainsKey(groupId))
			return new List<IItemSelection>();
		IContainer container = _containers[groupId].GetComponent<IContainer>();
		return container.GetItems();
	}

	public void ActivateContainer(string groupId)
	{
		foreach (var container in _containers.Values)
		{
			container.SetActive(false);
		}

		if (_containers.ContainsKey(groupId))
		{
			_containers[groupId].SetActive(true);
			_containers[groupId].GetComponent<IContainer>().Refresh();
		}
	}

	public void RefreshActiveContainer()
	{
		var activeContainer = _containers.Values.First((container => container.activeSelf));
		if (activeContainer != null)
		{
			activeContainer.GetComponent<IContainer>().Refresh();
		}
	}
}