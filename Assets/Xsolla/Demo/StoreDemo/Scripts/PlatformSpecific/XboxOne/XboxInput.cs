using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Xsolla.Core;

public class XboxInput : MonoBehaviour
{
	enum Tab
	{
		Store,
		Inventory
	};
	Tab tab;

#if UNITY_XBOX_ONE
	KeyCode storeTabKey = KeyCode.JoystickButton4;
#else
	KeyCode storeTabKey = KeyCode.LeftArrow;
#endif

#if UNITY_XBOX_ONE
	KeyCode inventoryTabKey = KeyCode.JoystickButton5;
#else
	KeyCode inventoryTabKey = KeyCode.RightArrow;
#endif

#if UNITY_XBOX_ONE
	string groupAxis = "Vertical";
#else
	string groupAxis = "Vertical";
#endif

#if UNITY_XBOX_ONE
	string itemAxisX = "Horizontal2";
#else
	string itemAxisX = "Horizontal2";
#endif

#if UNITY_XBOX_ONE
	string itemAxisY = "Vertical2";
#else
	string itemAxisY = "Vertical2";
#endif

	GroupsController groupController;
	ItemsTabControl tabController;
	ItemsController itemsController;

	int selectedIndex;
	IItemSelection selectedItem;

	private void Awake()
	{
		tab = Tab.Store;
	}
	// Start is called before the first frame update
	void Start()
    {
		selectedItem = null;
		groupController = FindObjectOfType<GroupsController>();
		tabController = FindObjectOfType<ItemsTabControl>();
		itemsController = FindObjectOfType<ItemsController>();
		StartCoroutine("GroupsCoroutine");
		StartCoroutine("ItemsCoroutine");
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}

	// Update is called once per frame
	void Update()
    {
		UpdateSelectedItem();
		MoveThroughtTabs();
	}

	IEnumerator GroupsCoroutine()
	{
		float timeout = 0.001F;
		while(true) {
			yield return new WaitForSeconds(timeout);
			float v = Input.GetAxis(groupAxis);
			if (Mathf.Abs(v) > 0.01F) {
				if (v > 0) {
					SelectGroupUp();
					selectedIndex = 0;
				} else {
					SelectGroupDown();
					selectedIndex = 0;
				}
				timeout = 0.3F;
			} else {
				timeout = 0.001F;
			}
		}
	}

	IEnumerator ItemsCoroutine()
	{
		float timeout = 0.001F;
		while (true) {
			yield return new WaitForSeconds(timeout);
			float h = Input.GetAxis(itemAxisX);
			float v = Input.GetAxis(itemAxisY);
			if ((Mathf.Abs(h) > 0.01F) || (Mathf.Abs(v) > 0.01F)) {
				if(Mathf.Abs(h) > 0.01F) {
					if (h > 0) {
						selectedIndex++;
					} else {
						if (selectedIndex > 0)
							selectedIndex--;
					}
				}
				if (Mathf.Abs(v) > 0.01F) {
					if (v < 0) {
						selectedIndex += 3;
					} else {
						if (selectedIndex > 2)
							selectedIndex -= 3;
					}
				}
				timeout = 0.3F;
			} else {
				timeout = 0.001F;
			}
		}
	}

	void MoveThroughtTabs()
	{
		if (Input.GetKeyUp(storeTabKey)) {
			Unfocus();
			tabController.SelectStoreTab();
			tab = Tab.Store;
		}
		if (Input.GetKeyUp(inventoryTabKey)) {
			Unfocus();
			tabController.SelectInventoryTab();
			tab = Tab.Inventory;
		}
	}

	//void MoveThroughtItems()
	//{
	//	if (Input.GetKeyUp(KeyCode.D))
	//		selectedIndex++;
	//	if (Input.GetKeyUp(KeyCode.S))
	//		selectedIndex += 3;

	//	if (selectedIndex > 0) {
	//		if (Input.GetKeyUp(KeyCode.A))
	//			selectedIndex--;
	//	}
	//	if (selectedIndex > 2) {
	//		if (Input.GetKeyUp(KeyCode.W))
	//			selectedIndex -= 3;
	//	}
	//}

	void UpdateSelectedItem()
	{
		IGroup group = groupController.GetSelectedGroup();
		if (group == null)
			return;
		string groupId = tab == Tab.Store ? group.Id : Constants.InventoryConatainerName;
		List<IItemSelection> items = itemsController.GetItemsByGroupId(groupId);
		if (selectedIndex >= items.Count)
			selectedIndex = 0;

		IItemSelection item = items.Count > 0 ? items[selectedIndex] : null;
		if (selectedItem != item) {
			Unfocus();
			Focus(item);
		}
	}

	IItemSelection Focus(IItemSelection item)
	{
		selectedItem = item;
		selectedItem?.Focus();
		return selectedItem;
	}

	void Unfocus()
	{
		selectedItem?.Unfocus();
		selectedItem = null;
	}

	void GetGroups(out IGroup previous, out IGroup selected, out IGroup next)
	{
		selected = previous = next = null;

		List<IGroup> groups = groupController.GetGroups();
		int count = groups.Count;
		if (count == 0)
			return;

		selected = groupController.GetSelectedGroup();
		int index = groups.IndexOf(selected);
		previous = (index == 0) ? groups.Last() : groups[index - 1];
		next = groups[(index + 1) % count];
	}

	void SelectGroupUp()
	{
		IGroup previous;
		GetGroups(out previous, out IGroup selected, out IGroup next);
		if(previous != null) {
			previous.Select();
			selectedIndex = 0;
		}
	}

	void SelectGroupDown()
	{
		IGroup next;
		GetGroups(out IGroup previous, out IGroup selected, out next);
		if (next != null) {
			next.Select();
			selectedIndex = 0;
		}
	}
}
