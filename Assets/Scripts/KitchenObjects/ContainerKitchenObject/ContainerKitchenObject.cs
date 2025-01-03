using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerKitchenObject : KitchenObject
{
    [SerializeField] protected Transform dishPosition;
    [SerializeField] protected ProductsGroupInContainer productsGrid;

    protected CoockingRecipeSO[] _coockingRecipeSOArray;

    protected List<CoockingRecipeSO> _relevantRecipeSOList;
    protected List<ProductSO> _currentProductsSOList;

    public bool IsEmpty => _currentProductsSOList.Count == 0;

    public virtual bool MixAndCookProduct(ProductSO productSO) { return false; }

    public void Clear()
    {
        Product[] productsInContainer = dishPosition.GetComponentsInChildren<Product>();
        foreach (var product in productsInContainer)
        {
            Destroy(product.gameObject);
        }

        _currentProductsSOList.Clear();
        _relevantRecipeSOList = _coockingRecipeSOArray.ToList();
        productsGrid.ClearProducts();
        OnClear();
    }

    protected virtual void OnClear() { }
}
