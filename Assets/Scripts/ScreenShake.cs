using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO
{
    public class ScreenShake : MonoBehaviour
    {
        public float shakeDuration = 1.0f;      // Desired duration of the shake effect
        public float shakeMagnitude = 0.7f;     // A measure of magnitude for the shake
        public float dampingSpeed = 1.0f;       // A measure of how quickly the shake effect should evaporate

        private float duration = 0f;            // Internal duration of the shake effect

        private new Transform transform;        // Transform of the GameObject you want to shake
        private Vector3 initialPosition;        // The initial position of the GameObject

        void Awake()
        {
            // Store camera transform
            if (transform == null)
            {
                transform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        void OnEnable()
        {
            // Store initial camera position
            initialPosition = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (duration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                duration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                duration = 0f;
                transform.localPosition = initialPosition;
            }
        }

        // Public functions
        public void TriggerShake()
        {
            duration = shakeDuration;
        }
    }
}
