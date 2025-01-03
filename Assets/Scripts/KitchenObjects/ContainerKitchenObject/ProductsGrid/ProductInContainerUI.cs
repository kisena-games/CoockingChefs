using UnityEngine;
using UnityEngine.UI;

public class ProductInContainerUI : MonoBehaviour
{
    [SerializeField] private Sprite startImage;

    private Image imageSource;
    private ProductSO productSO;

    public bool IsProductSONull => productSO == null;

    private void Awake()
    {
        imageSource = GetComponent<Image>();
        imageSource.sprite = startImage;
    }

    public void SetProduct(ProductSO so)
    {
        productSO = so;
        imageSource.sprite = productSO.sprite;
    }
}
