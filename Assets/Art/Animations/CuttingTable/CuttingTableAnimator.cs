using UnityEngine;

public class CuttingTableAnimator : MonoBehaviour
{
    private const string KNIFE_CUTTING = "KnifeCutting";

    [SerializeField] private CuttingTable cuttingTable;

    private Animator _animator;

    private void OnEnable()
    {
        //cuttingTable.OnKnifeCuttingAction += OnOpenClose;
    }

    private void OnDisable()
    {
        //cuttingTable.OnKnifeCuttingAction -= OnOpenClose;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnOpenClose()
    {
        _animator.SetBool(KNIFE_CUTTING, true);
    }
}
