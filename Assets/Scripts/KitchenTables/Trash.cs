using Unity.VisualScripting;
using UnityEngine;

public class Trash : KitchenTable
{
    public override void OnPickupOrDrop(PlayerInventory inventory)
    {
        if (inventory.IsCurrentKitchenObjectExists)
        {
            KitchenObject kitchenObject = inventory.CurrentKitchenObject;

            if (kitchenObject is ContainerKitchenObject containerKitchenObject)
            {
                if (!containerKitchenObject.IsEmpty)
                {
                    containerKitchenObject.Clear();
                }
            }
            else if (kitchenObject is Product)
            {
                kitchenObject.DestroyKitchenObject();
            }
        }
    }
}
