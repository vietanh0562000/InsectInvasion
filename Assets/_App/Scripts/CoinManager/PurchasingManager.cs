using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
   public void OnPressDown(int i)
   {
      switch (i)
      {
         case 0:
            CurrencyManager._instance.AddCoins(10000);
            IAPManager.Instance.BuyProductID(IAPKey.PACK1);
            break;
         case 1:
            CurrencyManager._instance.AddCoins(50000);
            IAPManager.Instance.BuyProductID(IAPKey.PACK2);
            break;
         case 2:
            CurrencyManager._instance.AddCoins(1000000);
            IAPManager.Instance.BuyProductID(IAPKey.PACK3);
            break;
         case 3:
            CurrencyManager._instance.AddCoins(10000000);
            IAPManager.Instance.BuyProductID(IAPKey.PACK4);
            break;
      }
   }

   public void Sub(int i)
   {
      GameDataManager.Instance.playerData.SubDiamond(i);
   }
}
