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
	SimpleButton clearCartButton;

	public Action OnClearCart {
		set {
			clearCartButton.onClick = value;
		}
	}

	public void Init()
	{
		var groupsController = FindObjectOfType<GroupsController>();
		var itemsController = FindObjectOfType<ItemsController>();

		storeButton.gameObject.SetActive(true);
		inventoryButton.gameObject.SetActive(true);

		storeButton.Select(false);

		storeButton.onClick = ((s) => {
			inventoryButton.Deselect();

			var selectedGroup = groupsController.GetSelectedGroup();

			if (selectedGroup != null) {
				itemsController.ActivateContainer(selectedGroup.Id);

				if (selectedGroup.Id == Constants.CartGroupName) {
					clearCartButton.gameObject.SetActive(true);
				}
			}
		});

		inventoryButton.onClick = ((s) => {
			storeButton.Deselect();

			itemsController.ActivateContainer(Constants.InventoryConatainerName);

			clearCartButton.gameObject.SetActive(false);
		});
	}

	public void ActivateStoreTab(string groupID)
	{
		if (groupID != Constants.CartGroupName) {
			storeButton.Text = StoreButtonText;
			clearCartButton.gameObject.SetActive(false);
		} else {
			storeButton.Text = CartButtonText;
			clearCartButton.gameObject.SetActive(true);
		}

		storeButton.Select(false)	;
		inventoryButton.Deselect();
	}

	public void SelectStoreTab()
	{
		this.storeButton.Select();
	}
	public void SelectInventoryTab()
	{
		this.inventoryButton.Select();
	}
}
