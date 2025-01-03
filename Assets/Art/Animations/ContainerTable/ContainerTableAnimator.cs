using System.Collections.Generic;
using System;
using UnityEngine;

public class ContainerTableAnimator : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerTable containerTable;

    private Animator _animator;

    private void OnEnable()
    {
        containerTable.OnOpenCloseAction += OnOpenClose;
    }

    private void OnDisable()
    {
        containerTable.OnOpenCloseAction -= OnOpenClose;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnOpenClose()
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
