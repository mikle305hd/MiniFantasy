﻿using UnityEngine;

namespace Domain.Units.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        [Header("Clips names")] [Space(3)]
        [SerializeField] private string _meleeAttackClipName = "OneHandMeleeAttack";
        [SerializeField] private string _getHitClipName = "GettingHit";

        // Parameters
        private static readonly int _dieHash = Animator.StringToHash("Die");
        private static readonly int _getHitHash = Animator.StringToHash("GetHit");
        private static readonly int _meleeAttackHash = Animator.StringToHash("MeleeAttack");
        private static readonly int _speedHash = Animator.StringToHash("Speed");
        private static readonly int _isMovingHash = Animator.StringToHash("IsMoving");
        
        // Anim states speed multipliers
        private static readonly int _attackSpeedHash = Animator.StringToHash("AttackSpeed");
        private static readonly int _getHitSpeedHash = Animator.StringToHash("GetHitSpeed");

        // Clips lengths
        private float _meleeAttackClipLength;
        private float _getHitClipLength;
        
        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            InitClipsLengths();
        }

        public void PlayMeleeAttack() =>
            _animator.SetTrigger(_meleeAttackHash);

        public void PlayHit() =>
            _animator.SetTrigger(_getHitHash);

        public void PlayDie() =>
            _animator.SetTrigger(_dieHash);

        public void UpdateMoving(float speedCoefficient)
        {
            _animator.SetBool(_isMovingHash, true);
            _animator.SetFloat(_speedHash, speedCoefficient);
        }

        public void StopMoving() =>
            _animator.SetBool(_isMovingHash, false);

        public void SetAttackDuration(float duration) 
            => SetAnimStateDuration(_attackSpeedHash, _meleeAttackClipLength, duration);

        public void SetGetHitDuration(float duration) 
            => SetAnimStateDuration(_getHitSpeedHash, _getHitClipLength, duration);

        private void InitClipsLengths()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            
            foreach(AnimationClip clip in clips)
            {
                switch(clip.name)
                {
                    case var value when value == _meleeAttackClipName:
                        _meleeAttackClipLength = clip.length;
                        break;
                    case var value when value == _getHitClipName:
                        _getHitClipLength = clip.length;
                        break;
                }
            }
        }

        private void SetAnimStateDuration(int paramHash, float clipLength, float duration)
        {
            float multiplier = clipLength / duration;
            _animator.SetFloat(paramHash, multiplier);
        }
    }
}