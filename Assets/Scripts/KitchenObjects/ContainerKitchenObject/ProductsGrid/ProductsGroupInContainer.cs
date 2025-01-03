using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductsGroupInContainer : MonoBehaviour
{
    [SerializeField] private GameObject prefabProductUI;

    private List<ProductInContainerUI> _currentProductUIArray;

    public void Initialize(int numStartProductUIs)
    {
        _currentProductUIArray = new List<ProductInContainerUI>();

        for (int i = 0; i < numStartProductUIs; i++)
        {
            SpawnDefaultProductUI();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void SpawnDefaultProductUI()
    {
        ProductInContainerUI productUI = Instantiate(prefabProductUI, transform).GetComponent<ProductInContainerUI>();
        _currentProductUIArray.Add(productUI);
    }

    public void AddProduct(ProductSO productSO, bool isNeedDefaultProductUI)
    {
        _currentProductUIArray[^1].SetProduct(productSO);

        if (isNeedDefaultProductUI)
        {
            SpawnDefaultProductUI();
        }
    }

    public void ClearProducts()
    {
        foreach (ProductInContainerUI productUI in _currentProductUIArray)
        {
            Destroy(productUI.gameObject);
        }

        _currentProductUIArray.Clear();
        SpawnDefaultProductUI();
    }
}
