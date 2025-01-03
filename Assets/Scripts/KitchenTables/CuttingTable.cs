using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CuttingTable : KitchenTable
{
    private const float cuttingFixedCoeff = 0.01f;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    [SerializeField] private float cuttingSpeed = 15.0f;
    [SerializeField] private ProgressBarUI progressBar;

    //public Action OnKnifeCuttingAction;
    public float CuttingProgress { get; private set; }
    public bool IsCutting => CuttingProgress > 0.0f;

    public override void OnPickupOrDrop(PlayerInventory inventory)
    {
        if (!IsCutting)
        {
            base.OnPickupOrDrop(inventory);
        }
    }

    public override void Interact()
    {
        if (IsCurrentKitchenObjectExists)
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputKitchenObjectSO(CurrentKitchenObject.KitchenObjectSO);

            if (outputKitchenObjectSO != null)
            {
                if (!IsCutting)
                {
                    progressBar.Show();
                }

                StartCoroutine(StartCutting(outputKitchenObjectSO));
            }
        }
    }

    private KitchenObjectSO GetOutputKitchenObjectSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO recipeSO in cuttingRecipeSOArray)
        {
            if (recipeSO.input == inputKitchenObjectSO)
            {
                return recipeSO.output;
            }
        }

        return null;
    }

    public override void OnPlayerLeaveInteractZone()
    {
        StopCutting();
    }

    public void StopCutting()
    {
        if (IsCutting)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator StartCutting(KitchenObjectSO outputKitchenObjectSO)
    {
        while (CuttingProgress < 1.0f)
        {
            CuttingProgress += Time.deltaTime * cuttingSpeed * cuttingFixedCoeff;
            progressBar.SetProgress(CuttingProgress);

            yield return null;
        }

        CuttingProgress = 0.0f;
        progressBar.Hide();
        CurrentKitchenObject.DestroyKitchenObject();
        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
    }
}

