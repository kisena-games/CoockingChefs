using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CrockeryTable : KitchenTable
{
    [SerializeField] CrockerySO startCrockerySO;
    [SerializeField] float crockeryOffset = 0.05f;
    [SerializeField] int crockeryNum = 1;

    private List<Crockery> crockeryList;

    protected override void OnAwake()
    {
        crockeryList = new List<Crockery>();
    }

    protected override void OnStart()
    {
        SpawnCrockeries();
    }

    private void SpawnCrockeries()
    {
        if (startCrockerySO != null)
        {
            for (int i = 0; i < crockeryNum; i++)
            {
                Crockery crockery = Instantiate(startCrockerySO.prefab, HoldPosition).GetComponent<Crockery>();
                crockery.transform.localPosition = new Vector3(0, crockeryOffset * i, 0);
                crockeryList.Add(crockery);
            }
        }
    }

    public override void OnPickupOrDrop(PlayerInventory inventory)
    {
        if (crockeryList.Count != 0 && !inventory.IsCurrentKitchenObjectExists)
        {
            Crockery crockery = crockeryList[crockeryList.Count - 1];
            crockery.OnPlateTaken();
            crockery.SetKitchenObjectParent(inventory);
            crockeryList.Remove(crockery);
        }
        else if (inventory.IsCurrentKitchenObjectExists)
        {
            if (inventory.CurrentKitchenObject is Crockery crockery)
            {
                
            }
        }
    }
}
