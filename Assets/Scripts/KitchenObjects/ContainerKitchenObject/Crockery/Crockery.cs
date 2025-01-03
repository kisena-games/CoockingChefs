using UnityEngine;

public class Crockery : ContainerKitchenObject
{
    public void OnPlateTaken()
    {
        productsGrid.Initialize(1);
    }
}
