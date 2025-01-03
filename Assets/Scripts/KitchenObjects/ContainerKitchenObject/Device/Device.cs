using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Device : ContainerKitchenObject
{
    private const float coockingFixedCoeff = 0.01f;

    [SerializeField] private float CoockingSpeed = 15.0f;
    [SerializeField] private ProgressBarUI progressBar;

    public float CoockingProgress { get; private set; }
    public bool IsCoocking => CoockingProgress > 0.0f;

    private bool isStartCoockingCoroutine = false;

    protected override void OnAwake()
    {
        _coockingRecipeSOArray = ((DeviceSO)KitchenObjectSO).coockingRecipeSOArray;
        _relevantRecipeSOList = _coockingRecipeSOArray.ToList();
        _currentProductsSOList = new List<ProductSO>();

        productsGrid.Initialize(1);
    }

    public override bool MixAndCookProduct(ProductSO productSO)
    {
        if (productSO == null)
        {
            return false;
        }

        List<CoockingRecipeSO> recipesForProduct = new List<CoockingRecipeSO>();
        int maxNumProductsInRelevantRecipes = 0;

        for (int i = 0; i < _relevantRecipeSOList.Count; i++)
        {
            foreach (ProductSO inputProductSO in _relevantRecipeSOList[i].input)
            {
                if (inputProductSO == productSO && !_currentProductsSOList.Contains(productSO))
                {
                    int NumProductsInInput = _relevantRecipeSOList[i].input.Length;
                    if (NumProductsInInput > maxNumProductsInRelevantRecipes)
                    {
                        maxNumProductsInRelevantRecipes = NumProductsInInput;
                    }

                    recipesForProduct.Add(_relevantRecipeSOList[i]);
                    
                    break;
                }
            }
        }

        if (recipesForProduct.Count == 0)
        {
            return false;
        }

        if (recipesForProduct.Count < _relevantRecipeSOList.Count)
        {
            _relevantRecipeSOList = recipesForProduct.ToList();
        }

        AddProduct(productSO);
        _currentProductsSOList.Add(productSO);
        productsGrid.AddProduct(productSO, maxNumProductsInRelevantRecipes > _currentProductsSOList.Count);

        return true;
    }

    private void AddProduct(ProductSO productSO)
    {
        BetweenProductSO[] betweenProductSOArray = _relevantRecipeSOList[0].between;

        for (int i = 0; i < betweenProductSOArray.Length; i++)
        {
            if (betweenProductSOArray[i].input == productSO)
            {
                Instantiate(betweenProductSOArray[i].output.prefab, dishPosition);
            }
        }
    }

    public void StartCoocking()
    {
        if (!isStartCoockingCoroutine && !IsEmpty)
        {
            if (!IsCoocking)
            {
                progressBar.Show();
            }

            StartCoroutine(StartCoockingCoroutine());
        }
    }

    public void StopCoocking()
    {
        if (isStartCoockingCoroutine)
        {
            StopCoroutine(StartCoockingCoroutine());
            isStartCoockingCoroutine = false;
        }
    }

    protected override void OnClear()
    {
        if (IsCoocking)
        {
            CoockingProgress = 0.0f;
            progressBar.Hide();
        }
    }

    IEnumerator StartCoockingCoroutine()
    {
        isStartCoockingCoroutine = true;
        CoockingRecipeSO currentRecipeSO = _relevantRecipeSOList[0];
        float currentMaxProgress = 1.0f / currentRecipeSO.between.Length * _currentProductsSOList.Count;

        while (CoockingProgress < currentMaxProgress)
        {
            CoockingProgress += Time.deltaTime * CoockingSpeed * coockingFixedCoeff;
            progressBar.SetProgress(CoockingProgress);

            yield return null;
        }

        if (CoockingProgress >= 1.0f)
        {
            productsGrid.Hide();
            Clear();
            Instantiate(currentRecipeSO.output.prefab, dishPosition);
        }

        isStartCoockingCoroutine = false;
    }
}
