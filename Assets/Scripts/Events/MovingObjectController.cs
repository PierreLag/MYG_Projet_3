using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEvents
{
    [SelectionBase]
    public class MovingObjectController : MonoBehaviour
    {
        [SerializeField]
        protected Vector3[] checkpointNodes;
        [SerializeField]
        [Range(0f, 1f)]
        protected float offsetPosition = 0f;
        [SerializeField]
        protected bool isLooping = true;
        [SerializeField]
        protected float speed = 2f;

        protected float totalDistance;
        protected float[] distanceList;
        protected bool isGoingBack;
        protected int latestCheckpoint;
        protected List<Rigidbody> rigidbodiesInContact;

        void Awake()
        {
            rigidbodiesInContact = new List<Rigidbody>();

            totalDistance = 0;
            if (isLooping)
            {
                distanceList = new float[checkpointNodes.Length];
                for (int i = 1; i < checkpointNodes.Length; i++)
                {
                    distanceList[i - 1] = (checkpointNodes[i] - checkpointNodes[i - 1]).magnitude;
                    totalDistance += distanceList[i - 1];
                }
                distanceList[distanceList.Length - 1] = (checkpointNodes[0] - checkpointNodes[checkpointNodes.Length - 1]).magnitude;
                totalDistance += distanceList[distanceList.Length - 1];
            }
            else
            {
                distanceList = new float[checkpointNodes.Length - 1];
                for (int i = 1; i < checkpointNodes.Length; i++)
                {
                    distanceList[i - 1] = (checkpointNodes[i] - checkpointNodes[i - 1]).magnitude;
                    totalDistance += distanceList[i - 1];
                }
                totalDistance *= 2;
            }

            float remainingOffset = totalDistance * offsetPosition;
            if (isLooping)
            {
                int i;
                for (i = 0; remainingOffset >= distanceList[i]; i++)
                {
                    remainingOffset -= distanceList[i];
                }

                if (i == checkpointNodes.Length - 1)
                    transform.localPosition = Vector3.MoveTowards(checkpointNodes[i], checkpointNodes[0], remainingOffset);
                else
                    transform.localPosition = Vector3.MoveTowards(checkpointNodes[i], checkpointNodes[i + 1], remainingOffset);
                latestCheckpoint = i;
            }
            else
            {
                int i;
                isGoingBack = false;
                for (i = 0; i < distanceList.Length && remainingOffset >= distanceList[i]; i++)
                {
                    remainingOffset -= distanceList[i];
                }
                if (i == distanceList.Length)
                {
                    isGoingBack = true;
                    for (i = distanceList.Length - 1; remainingOffset >= distanceList[i]; i--)
                    {
                        remainingOffset -= distanceList[i];
                    }
                }

                if (isGoingBack)
                {
                    transform.localPosition = Vector3.MoveTowards(checkpointNodes[i + 1], checkpointNodes[i], remainingOffset);
                    latestCheckpoint = i + 1;
                }
                else
                {
                    transform.localPosition = Vector3.MoveTowards(checkpointNodes[i], checkpointNodes[i + 1], remainingOffset);
                    latestCheckpoint = i;
                }
            }
        }

        void FixedUpdate()
        {
            Vector3 movementVector = new Vector3();
            Vector3 newPosition = new Vector3();

            if (isLooping)
            {
                if (latestCheckpoint == checkpointNodes.Length - 1)
                {
                    newPosition = Vector3.MoveTowards(transform.localPosition, checkpointNodes[0], Time.fixedDeltaTime * speed);
                    movementVector = newPosition - transform.localPosition;
                    transform.localPosition = newPosition;

                    if (transform.localPosition == checkpointNodes[0])
                        latestCheckpoint = 0;
                }
                else
                {
                    newPosition = Vector3.MoveTowards(transform.localPosition, checkpointNodes[latestCheckpoint + 1], Time.fixedDeltaTime * speed);
                    movementVector = newPosition - transform.localPosition;
                    transform.localPosition = newPosition;

                    if (transform.localPosition == checkpointNodes[latestCheckpoint + 1])
                        latestCheckpoint++;
                }
            }
            else
            {
                if (isGoingBack)
                {
                    newPosition = Vector3.MoveTowards(transform.localPosition, checkpointNodes[latestCheckpoint - 1], Time.fixedDeltaTime * speed);
                    movementVector = newPosition - transform.localPosition;
                    transform.localPosition = newPosition;

                    if (transform.localPosition == checkpointNodes[latestCheckpoint - 1])
                    {
                        latestCheckpoint--;

                        if (latestCheckpoint == 0)
                            isGoingBack = false;
                    }
                }
                else
                {
                    newPosition = Vector3.MoveTowards(transform.localPosition, checkpointNodes[latestCheckpoint + 1], Time.fixedDeltaTime * speed);
                    movementVector = newPosition - transform.localPosition;
                    transform.localPosition = newPosition;

                    if (transform.localPosition == checkpointNodes[latestCheckpoint + 1])
                    {
                        latestCheckpoint++;

                        if (latestCheckpoint == checkpointNodes.Length - 1)
                            isGoingBack = true;
                    }
                }
            }

            foreach (Rigidbody rigidbody in rigidbodiesInContact)
            {
                rigidbody.MovePosition(rigidbody.position + movementVector);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbodiesInContact.Add(rigidbody);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbodiesInContact.Remove(rigidbody);
            }
        }
    }
}