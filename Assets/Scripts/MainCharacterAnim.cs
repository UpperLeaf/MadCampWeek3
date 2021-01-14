using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnim : MonoBehaviour
{
    private PlayerState playerState;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update

    private void Start()
    {
        playerState = GetComponent<PlayerController>().playerState;
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("isWalking", playerState.isWalking);
        _animator.SetBool("isIdling", playerState.isIdling);
        _animator.SetBool("isAttacking", playerState.isAttacking);
        _animator.SetBool("isJumping", playerState.isJumping);
    }
}
