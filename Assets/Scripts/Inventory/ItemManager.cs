using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : ObjectLibrary<ShopItem>
{
    public override ShopItem GetElementData(string currencyId)
    {
        foreach(var currency in Elements)
            if(currency.GetId().Equals(currencyId))
                return currency;

        return null;
    }
}
