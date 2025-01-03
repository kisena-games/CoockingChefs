using System;
using UnityEngine;

public class ContainerTable : KitchenTable
{
    [SerializeField] private ProductSO productSO;

    public Action OnOpenCloseAction;

    public override void OnPickupOrDrop(PlayerInventory inventory)
    {
        if (!inventory.IsCurrentKitchenObjectExists)
        {
            OnOpenCloseAction?.Invoke();

            KitchenObject.SpawnKitchenObject(productSO, inventory);
        }
        else
        {
            KitchenObject kitchenObject = inventory.CurrentKitchenObject;

            if (kitchenObject is Device device)
            {
                if (device.MixAndCookProduct(productSO))
                {
                    OnOpenCloseAction?.Invoke();
                }
            }
        }
    }
}
