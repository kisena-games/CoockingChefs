using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [Header("Input Hamdler")]
    [SerializeField] private InputHandler inputHandler;

    private Animator _animator;
    private Dictionary<PlayerAnimationType, int> _hashStorage = new Dictionary<PlayerAnimationType, int>();

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        foreach (PlayerAnimationType animType in Enum.GetValues(typeof(PlayerAnimationType)))
        {
            _hashStorage.Add(animType, Animator.StringToHash(animType.ToString()));
        }
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _animator.SetBool(_hashStorage[PlayerAnimationType.Move], inputHandler.IsMove);
    }
}

public enum PlayerAnimationType
{
    Move
}
