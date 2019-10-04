using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Xsolla.Core;

public class XboxInput : MonoBehaviour
{
    public enum MenuButton
    {
        GroupDown,
        GroupUp,
        StoreTab,
        InventoryTab,
        ItemLeft,
        ItemRight,
        ItemDown,
        ItemUp
    };

    public delegate void ButtonPressedDelegate(MenuButton button);
    public event ButtonPressedDelegate ButtonPressedEvent;

	string groupAxis = "Vertical";
    string itemAxisX = "Axis4";
	string itemAxisY = "Axis5";
    string storeAxis = "Axis9";
    string inventoryAxis = "Axis10";

    void Start()
    {
        StartCoroutine("ItemControl");
        StartCoroutine("GroupControl");
        StartCoroutine("TabControl");
    }

    IEnumerator ItemControl()
    {
        float timeout = 0.01F;
        while (true)
        {
            yield return new WaitForSeconds(timeout);

            float x = Input.GetAxis(itemAxisX);
            float y = Input.GetAxis(itemAxisY);
            float absX = Mathf.Abs(x);
            float absY = Mathf.Abs(y);

            if ((absX > 0.7F) || (absY > 0.7F))
            {
                timeout = 0.3F;

                if (absX > absY)
                    ButtonPressedEvent?.Invoke(x > 0 ? MenuButton.ItemRight : MenuButton.ItemLeft);
                else
                    ButtonPressedEvent?.Invoke(y > 0 ? MenuButton.ItemDown : MenuButton.ItemUp);
            }
            else
                timeout = 0.01F;
        }
    }

    IEnumerator GroupControl()
    {
        float timeout = 0.01F;
        while (true)
        {
            yield return new WaitForSeconds(timeout);
            float group = Input.GetAxis(groupAxis);

            if (Mathf.Abs(group) > 0.7F)
            {
                ButtonPressedEvent?.Invoke(group > 0 ? MenuButton.GroupDown : MenuButton.GroupUp);
                timeout = 0.3F;
            }else
                timeout = 0.01F;
        }
    }

    IEnumerator TabControl()
    {
        float timeout = 0.01F;
        while (true)
        {
            yield return new WaitForSeconds(timeout);
            float store = Mathf.Abs(Input.GetAxis(storeAxis));
            float inventory = Mathf.Abs(Input.GetAxis(inventoryAxis));

            if ((store > 0.7F) || (inventory > 0.7F))
            {
                if (store > 0.2F)
                    ButtonPressedEvent?.Invoke(MenuButton.StoreTab);
                else
                    ButtonPressedEvent?.Invoke(MenuButton.InventoryTab);
                timeout = 0.3F;
            }
            else
                timeout = 0.01F;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
