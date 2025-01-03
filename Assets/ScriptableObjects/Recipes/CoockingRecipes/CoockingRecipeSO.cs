using UnityEngine;

[CreateAssetMenu(fileName = "CoockingRecipeSO", menuName = "Scriptable Objects/CoockingRecipeSO")]
public class CoockingRecipeSO : ScriptableObject
{
    public ProductSO[] input;
    public BetweenProductSO[] between;
    public ProductSO output;
}
