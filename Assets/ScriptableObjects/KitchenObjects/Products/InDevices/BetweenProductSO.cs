using UnityEngine;

[CreateAssetMenu(fileName = "BetweenProductSO", menuName = "Scriptable Objects/BetweenProductSO")]
public class BetweenProductSO : ScriptableObject
{
    public ProductSO input;
    public ProductSO output;
}
