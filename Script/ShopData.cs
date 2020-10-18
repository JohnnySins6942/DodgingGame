using System.Collections.Generic;
using UnityEngine;

public static class ShopData
{
    public static List<ShopItem> Brought = new List<ShopItem>();
    public static List<ShopItem> Items = new List<ShopItem>();

    public static ShopItem selectedItem;

    public static void MoveItems()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if(Items[i].isBrought == true)
            {
                Brought.Add(Items[i]);
            }
        }
    }
    public static void MoveEquipped(int index)
    {
        for (int i = 0; i < Brought.Count; i++)
        {
            if(i != index)
            {
                Brought[i].isEquipped = false;
            }
            else
            {
                Brought[i].isEquipped = true;
                selectedItem = Brought[i];
                break;
            }
        }
    }
}
