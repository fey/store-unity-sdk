﻿using System;
using UnityEngine;
using Xsolla.Core;

public class ItemsTabControl : MonoBehaviour
{
	const string StoreButtonText = "Store";
	const string CartButtonText = "Cart";

	[SerializeField]
	MenuButton storeButton;
	[SerializeField]
	MenuButton inventoryButton;
	[SerializeField]
	VirtualCurrencyContainer virtualCurrencyBalance;
	public VirtualCurrencyContainer VirtualCurrencyBalance { get => virtualCurrencyBalance; set => virtualCurrencyBalance = value; }

	public void Init()
	{
		var groupsController = FindObjectOfType<GroupsController>();
		var itemsController = FindObjectOfType<ItemsController>();
		
		storeButton.gameObject.SetActive(true);
		inventoryButton.gameObject.SetActive(true);
		
		storeButton.Select(false);
		
		storeButton.onClick = ((s) =>
		{
			inventoryButton.Deselect();

			var selectedGroup = groupsController.GetSelectedGroup();
			if (selectedGroup != null)
			{
				itemsController.ActivateContainer(selectedGroup.Id);
			}
		});
		
		inventoryButton.onClick = ((s) =>
		{
			storeButton.Deselect();
			itemsController.ActivateContainer(Constants.InventoryContainerName);
		});
	}

	public void ActivateStoreTab(string groupID)
	{
		storeButton.Text = (groupID == Constants.CartGroupName) ? CartButtonText : StoreButtonText;
		storeButton.Select(false);
		inventoryButton.Deselect();
	}
}