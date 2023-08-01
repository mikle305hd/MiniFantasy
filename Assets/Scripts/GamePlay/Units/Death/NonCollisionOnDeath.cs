using UnityEngine;

namespace GamePlay.Units.Death
{
    public class NonCollisionOnDeath : MonoBehaviour
    {
        private Collider[] _colliders;

        private void Awake()
        {
            _colliders = GetComponents<Collider>();
            GetComponent<Death>().Happened += DisableColliders;
        }

        private void DisableColliders()
        {
            foreach (Collider col in _colliders) 
                col.enabled = false;
        }
    }
}