using UnityEngine;

[RequireComponent(typeof(Outline))]
public class KitchenTable : BaseHolder, IInteractable
{
    private Outline interactOutline;

    private void Awake()
    {
        interactOutline = GetComponent<Outline>();
        interactOutline.OutlineMode = Outline.Mode.OutlineVisible;
        HideOutline();
        OnAwake();
    }

    protected virtual void OnAwake() { }

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart() { }

    public void ShowOutline()
    {
        interactOutline.enabled = true;
    }

    public void HideOutline()
    {
        interactOutline.enabled = false;
    }

    public virtual void Interact() { }

    public virtual void OnPlayerLeaveInteractZone() { }

    public override void OnPickupAndMixProduct(Product product)
    {
        if (CurrentKitchenObject is Device device)
        {
            bool isSuccess = device.MixAndCookProduct((ProductSO)product.KitchenObjectSO);

            if (isSuccess)
            {
                product.DestroyKitchenObject();
            }
        }
        else if (CurrentKitchenObject is Crockery crockery)
        {
            // добавить взаимодействие на crockery (возможно совместить с device из выше условия)
        }
    }
} 
