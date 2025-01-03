using System;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [field: SerializeField] public KitchenObjectSO KitchenObjectSO { get; private set; }
    public ICanPickup ObjectParent { get; protected set; }

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake() { }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, ICanPickup parent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);

        return kitchenObject;
    }

    public void SetKitchenObjectParent(ICanPickup parent)
    {
        if (ObjectParent != null)
        {
            ObjectParent.ClearCurrentKitchenObject();
        }

        ObjectParent = parent;

        if (ObjectParent.IsCurrentKitchenObjectExists)
        {
            Debug.LogError("IPickupable ObjectParent already has a KitchenObject");
        }

        ObjectParent.SetCurrentKitchenObject(this);

        transform.parent = ObjectParent.HoldPosition;
        transform.rotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    public void ClearKitchenObjectParent()
    {
        ObjectParent = null; 
    }

    public void DestroyKitchenObject()
    {
        ObjectParent.ClearCurrentKitchenObject();
        Destroy(gameObject);
    }
}
