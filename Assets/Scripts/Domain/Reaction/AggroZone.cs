﻿using System.Collections;
using Domain.Units.Follow;
using UnityEngine;

namespace Domain.Reaction
{
    public class AggroZone : ReactionZone
    {
        [SerializeField] private Follower _follower;
        
        [Header("Settings")] [Space(3)]
        [Tooltip("In seconds")] [SerializeField] private float _followingCooldown;

        private Coroutine _aggroCooldown;
        private bool _hasAggroTarget;


        protected override void OnTriggerEntered(Collider entered)
        {
            if (_hasAggroTarget)
                return;

            _hasAggroTarget = true;
            StopAggroCooldown();
            _follower.FollowTo(entered.transform);
        }

        protected override void OnTriggerExited(Collider entered)
        {
            if (!_hasAggroTarget)
                return;

            _hasAggroTarget = false;
            _aggroCooldown = StartCoroutine(StartAggroCooldown());
        }

        private IEnumerator StartAggroCooldown()
        {
            yield return new WaitForSeconds(_followingCooldown);

            _follower.StopFollowing();
        }

        private void StopAggroCooldown()
        {
            if (_aggroCooldown == null)
                return;
            
            StopCoroutine(_aggroCooldown);
        }
    }
}