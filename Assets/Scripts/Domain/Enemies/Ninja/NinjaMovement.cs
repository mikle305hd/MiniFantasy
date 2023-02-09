﻿using Additional;
using Domain.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Domain.Enemies.Ninja
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(NinjaAnimator))]
    public class NinjaMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NinjaAnimator _animator;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<NinjaAnimator>();
        }

        private void Start()
        {
            FollowCharacter();
        }

        private void Update()
        {
            if (IsMoving())
                _animator.UpdateMoving(_agent.velocity.magnitude);
            else
                _animator.StopMoving();

        }

        private bool IsMoving() =>
            _agent.velocity.magnitude > Constants.MinVelocity;

        private void FollowCharacter() =>
            GetComponent<AgentFollow>().Target = FindObjectOfType<CharacterMovement>().transform;
    }
}