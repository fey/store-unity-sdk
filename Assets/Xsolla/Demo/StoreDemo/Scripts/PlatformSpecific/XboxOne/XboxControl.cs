using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Xsolla.Core;

public class XboxControl : MonoBehaviour
{
    enum Tab
    {
        Store,
        Inventory
    };
    Tab tab;

    GroupsController groupController;
    ItemsTabControl tabController;
    ItemsController itemsController;

    XboxInput input;
    int selectedIndex;
    IItemSelection selectedItem;

    private void Awake()
    {
        selectedItem = null;
        tab = Tab.Store;
        input = gameObject.AddComponent<XboxInput>();
        input.ButtonPressedEvent += Input_ButtonPressedEvent;
    }

    private void Input_ButtonPressedEvent(XboxInput.MenuButton button)
    {
        switch(button)
        {
            case XboxInput.MenuButton.GroupDown:
                {
                    SelectGroupDown();
                    break;
                }
            case XboxInput.MenuButton.GroupUp:
                {
                    SelectGroupUp();
                    break;
                }
            case XboxInput.MenuButton.InventoryTab:
                {
                    Unfocus();
                    tabController.SelectInventoryTab();
                    tab = Tab.Inventory;
                    break;
                }
            case XboxInput.MenuButton.StoreTab:
                {
                    Unfocus();
                    tabController.SelectStoreTab();
                    tab = Tab.Store;
                    break;
                }
            case XboxInput.MenuButton.ItemUp:
                {
                    if (selectedIndex > 2)
                        selectedIndex -= 3;
                    else
                        selectedIndex = 0;
                    break;
                }
            case XboxInput.MenuButton.ItemDown:
                {
                    selectedIndex += 3;
                    break;
                }
            case XboxInput.MenuButton.ItemLeft:
                {
                    if(selectedIndex > 0)
                        selectedIndex--;
                    break;
                }
            case XboxInput.MenuButton.ItemRight:
                {
                    selectedIndex++;
                    break;
                }
            default:
                {
                    Debug.Log("Unhandled button pressed");
                    break;
                }
        }
    }

    void Start()
    {
        groupController = FindObjectOfType<GroupsController>();
        tabController = FindObjectOfType<ItemsTabControl>();
        itemsController = FindObjectOfType<ItemsController>();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        UpdateSelectedItem();
    }
    
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
        if (selectedItem != item)
        {
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

    void GetGroups(ref IGroup previous, ref IGroup selected, ref IGroup next)
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
        IGroup previous, selected, next;
        previous = selected = next = null;
        GetGroups(ref previous, ref selected, ref next);
        if (previous != null)
        {
            previous.Select();
            selectedIndex = 0;
        }
    }

    void SelectGroupDown()
    {
        IGroup previous, selected, next;
        previous = selected = next = null;
        GetGroups(ref previous, ref selected, ref next);
        if (next != null)
        {
            next.Select();
            selectedIndex = 0;
        }
    }
}
