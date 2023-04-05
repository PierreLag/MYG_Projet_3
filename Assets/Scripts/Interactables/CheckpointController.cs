using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Level
{
    [RequireComponent(typeof(Collider))]
    public class CheckpointController : MonoBehaviour
    {
        [Serializable]
        protected class TriggeredEvent : UnityEvent <CheckpointController> { }

        [SerializeField]
        protected TriggeredEvent OnPlayerPassesThrough;
        [SerializeField]
        protected TriggeredEvent OnCheckpointChange;
        [SerializeField]
        protected Transform respawnPoint;

        public Transform GetRespawnPoint()
        {
            return respawnPoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerScripts.PlayerController>(out PlayerScripts.PlayerController player))
            {
                OnPlayerPassesThrough.Invoke(this);
            }
        }

        public void DeactivateCheckpoint()
        {
            OnCheckpointChange.Invoke(this);
        }
    }
}