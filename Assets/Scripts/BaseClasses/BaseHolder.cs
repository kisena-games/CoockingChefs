using Unity.VisualScripting;
using UnityEngine;

public class BaseHolder : MonoBehaviour, ICanPickup
{
    [field: SerializeField] public Transform HoldPosition { get; private set; }
    public virtual KitchenObject CurrentKitchenObject { get; protected set; }
    public bool IsCurrentKitchenObjectExists => CurrentKitchenObject != null;

    public void SetCurrentKitchenObject(KitchenObject obj)
    {
        CurrentKitchenObject = obj;
    }

    public void ClearCurrentKitchenObject()
    {
        CurrentKitchenObject = null;
    }

    public virtual void OnPickupOrDrop(PlayerInventory inventory)
    {
        if (IsCurrentKitchenObjectExists && !inventory.IsCurrentKitchenObjectExists)
        {
            CurrentKitchenObject.SetKitchenObjectParent(inventory);
        }
        else if (!IsCurrentKitchenObjectExists && inventory.IsCurrentKitchenObjectExists)
        {
            inventory.CurrentKitchenObject.SetKitchenObjectParent(this);
        }
        else if (IsCurrentKitchenObjectExists && inventory.IsCurrentKitchenObjectExists)
        {
            if (inventory.CurrentKitchenObject is Product inventoryProduct)
            {
                OnPickupAndMixProduct(inventoryProduct);
            }
            else if (inventory.CurrentKitchenObject is Device device && CurrentKitchenObject is Product currentProduct)
            {
                device.MixAndCookProduct((ProductSO)currentProduct.KitchenObjectSO);
                CurrentKitchenObject.DestroyKitchenObject();
            }
        }
    }

    public virtual void OnPickupAndMixProduct(Product product) { }
}
