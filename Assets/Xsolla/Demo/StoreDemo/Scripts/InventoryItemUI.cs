using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Xsolla.Store;

public class InventoryItemUI : MonoBehaviour, IItemSelection
{
	[SerializeField]
	Image itemImage;
	[SerializeField]
	GameObject loadingCircle;
	[SerializeField]
	Text itemName;
	[SerializeField]
	Text itemDescription;
	[SerializeField]
	Text itemQuantity;
	
	Coroutine _loadingRoutine;
	InventoryItem _itemInformation;
	StoreController _storeController;

	Sprite _itemImage;

	DateTime startLoading;
	DateTime startInit;

	private void Awake()
	{
		startInit = DateTime.Now;
	}

	public void Initialize(InventoryItem itemInformation)
	{
		startLoading = DateTime.Now;
		_itemInformation = itemInformation;

		itemName.text = _itemInformation.name;
		itemDescription.text = _itemInformation.description;
		itemQuantity.text = _itemInformation.quantity.ToString();

		if (_itemImage == null && !string.IsNullOrEmpty(_itemInformation.image_url))
		{
			if (StoreController.ItemIcons.ContainsKey(_itemInformation.image_url))
			{
				loadingCircle.SetActive(false);
				itemImage.sprite = StoreController.ItemIcons[_itemInformation.image_url];
			}
			else
			{
				_loadingRoutine = StartCoroutine(LoadImage(_itemInformation.image_url));
			}
		}
	}

	IEnumerator LoadImage(string url)
	{
		using (var www = new WWW(url))
		{
			yield return www;
			
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, 1.5f));
			
			var sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));

			_itemImage = sprite;
			
			loadingCircle.SetActive(false);
			itemImage.sprite = sprite;

			if (!StoreController.ItemIcons.ContainsKey(url))
			{
				StoreController.ItemIcons.Add(url, sprite);
			}

			float fromStart = Time.realtimeSinceStartup;
			DateTime now = DateTime.Now;
			TimeSpan init = now - startInit;
			TimeSpan load = now - startLoading;
			itemName.text = "s:" + fromStart.ToString("F2") + "_i:" + init.TotalSeconds.ToString("F2") + "_l: " + load.TotalSeconds.ToString("F2");
			StartCoroutine(DeleteTimings(60.0F));
		}
	}

	IEnumerator DeleteTimings(float time)
	{
		yield return new WaitForSeconds(time);
		itemName.text = _itemInformation.name;
	}

	public void Focus()
	{
		gameObject.AddComponent<ItemSelection>();
	}

	public void Unfocus()
	{
		Component c = gameObject.GetComponent<ItemSelection>();
		if (c != null)
			Destroy(c);
	}
}