using Unity.VisualScripting;
using UnityEngine;

public class CoockingTable : KitchenTable
{
    [SerializeField] private DeviceSO startDeviceSO;
    [SerializeField] private ParticleSystem fireParticleSystem;

    public bool IsFireOn { get; private set; }

    protected override void OnStart()
    {
        SpawnDevice();
    }

    private void SpawnDevice()
    {
        if (startDeviceSO != null)
        {
            KitchenObject.SpawnKitchenObject(startDeviceSO, this);
        }
    }

    public override void Interact()
    {
        Device device = (Device)CurrentKitchenObject;

        if (!IsFireOn)
        {
            IsFireOn = true;
            fireParticleSystem.Play();

            if (device != null)
            {
                device.StartCoocking();
            }
        }
        else
        {
            IsFireOn = false;  
            fireParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            if (device != null)
            {
                device.StopCoocking();
            }
        }
    }

    public override void OnPickupOrDrop(PlayerInventory inventory)
    {
        if (CurrentKitchenObject is Device device)
        {
            if (inventory.CurrentKitchenObject is Product product)
            {

            }
            else if (!inventory.IsCurrentKitchenObjectExists)
            {

            }
        }
        else
        {

        }

        if (!IsCurrentKitchenObjectExists && inventory.CurrentKitchenObject is Device device2)
        {

        }

        //if (IsCurrentKitchenObjectExists && !inventory.IsCurrentKitchenObjectExists)
        //{
        //    CurrentKitchenObject.SetKitchenObjectParent(inventory);
        //}
        //else if (!IsCurrentKitchenObjectExists && inventory.IsCurrentKitchenObjectExists)
        //{
        //    inventory.CurrentKitchenObject.SetKitchenObjectParent(this);
        //}
        //else if (IsCurrentKitchenObjectExists && inventory.IsCurrentKitchenObjectExists)
        //{
        //    if (inventory.CurrentKitchenObject is Product inventoryProduct)
        //    {
        //        OnPickupAndMixProduct(inventoryProduct);
        //    }
        //    else if (inventory.CurrentKitchenObject is Device device1 && CurrentKitchenObject is Product currentProduct)
        //    {
        //        device1.MixAndCookProduct((ProductSO)currentProduct.KitchenObjectSO);
        //        CurrentKitchenObject.DestroyKitchenObject();
        //    }
        //}
    }
}
