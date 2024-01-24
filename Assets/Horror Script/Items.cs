using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : Interactable
{
   public abstract void Pick(Items items, ref Items itemsInPlayerHand,  ref Interactable usableItem);
   public abstract void Use();
   public abstract void Drop(Items items,  ref Items itemsInPlayerHand, ref Interactable usableItem);
}
