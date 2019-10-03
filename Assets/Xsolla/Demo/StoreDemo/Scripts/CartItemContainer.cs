﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xsolla.Core;
using Xsolla.Store;

public class CartItemContainer : MonoBehaviour, IContainer
{
	[SerializeField]
	GameObject cartItemPrefab;

	[SerializeField]
	GameObject cartDiscountPrefab;
	
	[SerializeField]
	GameObject cartControlsPrefab;
	
	[SerializeField]
	Transform itemParent;

	List<CartItemUI> _cartItems;

	GameObject _discountPanel;
	GameObject _cartControls;
	
	StoreController _storeController;

	CartGroupUI _cartGroup;

	void Awake()
	{
		_cartItems = new List<CartItemUI>();
		
		_storeController = FindObjectOfType<StoreController>();
		_cartGroup = FindObjectOfType<CartGroupUI>();

		var itemsTabControl = FindObjectOfType<ItemsTabControl>();
		itemsTabControl .OnClearCart = OnClearCart;
	}

	void OnClearCart()
	{
		ClearCartItems();
		
		_storeController.CartModel.CartItems.Clear();
		
		_cartGroup.ResetCounter();
		
		XsollaStore.Instance.ClearCart(XsollaSettings.StoreProjectId, _storeController.Cart.id, null, _storeController.ShowError);
	}

	void AddCartItem(CartItemModel itemInformation)
	{
		GameObject newItem = Instantiate(cartItemPrefab, itemParent);
		CartItemUI itemUi = newItem.GetComponent<CartItemUI>();
		itemUi.Initialize(itemInformation);
		_cartItems.Add(itemUi);
	}

	public void Refresh()
	{
		ClearCartItems();

		if (!_storeController.CartModel.CartItems.Any())
		{
			return;
		}
		
		foreach (var item in _storeController.CartModel.CartItems)
		{
			AddCartItem(item.Value);
		}

		var discount = _storeController.CartModel.CalculateCartDiscount();
		if (discount > 0.0f)
		{
			AddDiscount(discount);
		}

		var fullPrice = _storeController.CartModel.CalculateCartPrice();

		AddControls(fullPrice - discount);
	}

	void AddDiscount(float discountAmount)
	{
		_discountPanel = Instantiate(cartDiscountPrefab, itemParent);
		_discountPanel.GetComponent<CartDiscountUI>().Initialize(discountAmount); 
	}

	void AddControls(float price)
	{
		_cartControls = Instantiate(cartControlsPrefab, itemParent);
		_cartControls.GetComponent<CartControls>().Initialize(price); 
	}

	void ClearCartItems()
	{
		foreach (var item in _cartItems)
		{
			Destroy(item.gameObject);
		}
		
		_cartItems.Clear();
		
		Destroy(_discountPanel);
		Destroy(_cartControls);
	}

	List<IItemSelection> IContainer.GetItems()
	{
		return _cartItems.OfType<IItemSelection>().ToList();
	}
}