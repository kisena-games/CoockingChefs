using UnityEngine;

public interface ICanPickup
{
    public Transform HoldPosition { get; }
    public KitchenObject CurrentKitchenObject { get; }
    public bool IsCurrentKitchenObjectExists { get; }

    public void SetCurrentKitchenObject(KitchenObject obj);
    public void ClearCurrentKitchenObject();
}
