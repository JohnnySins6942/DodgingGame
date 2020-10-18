using UnityEngine;

public class Shop : MonoBehaviour
{
    public ShopItem[] items; 
    public bool IsMainShop;
    private void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopData.Items.Add(items[i]);
        }
        ShopData.MoveItems();
    }

    public void Buy(int index)
    {
        if (items[index].isBrought == false)
        {
            items[index].isBrought = true;
            ShopData.MoveItems();
            items[index].DisplayUI();
        }
    }
    public void Equip(int index)
    {
        if(items[index].isEquipped == false)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i].isBrought == true)
                {
                    items[index].isEquipped = false;
                    items[index].DisplayUI();
                }
            }
        }
        items[index].isEquipped = true;
        ShopData.MoveEquipped(index);
        print(ShopData.selectedItem.Name);
        items[index].DisplayUI();
    }
    private void OnApplicationQuit()
    {

    }
}
