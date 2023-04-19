using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level {
    [RequireComponent(typeof(Collider))]
    public class LevelAreaController : MonoBehaviour
    {
        static protected LevelAreaController s_instance = null;

        protected CheckpointController lastCheckpoint;
        protected Collider m_collider;

        private void Awake()
        {
            s_instance = this;
            m_collider = GetComponent<Collider>();
        }

        public static void UpdateLastCheckpoint (CheckpointController checkpoint)
        {
            if (s_instance.lastCheckpoint != null)
                s_instance.lastCheckpoint.DeactivateCheckpoint();

            s_instance.lastCheckpoint = checkpoint;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerScripts.PlayerController>(out PlayerScripts.PlayerController player))
            {
                other.transform.position = lastCheckpoint.GetRespawnPoint().position;
                StartCoroutine(player.PushPlayer(Vector3.zero, s_instance.gameObject));
            }
            if (other.gameObject.layer == 9)    // The Obstacle Layer, used for moving objects interfering with the player and that may go out of bounds.
            {
                Destroy(other.gameObject);
            }
        }
    }
}