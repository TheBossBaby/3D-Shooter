using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM.Camera
{
    public class TopDownCameraController : MonoBehaviour
    {
        #region Public Variables
        public Transform m_target;
        public float m_height=50f;
        public float m_distance=50f;
        public float m_angle = 45f;
        public float m_smoothSpeed = 0.5f;
        #endregion

        private Vector3 _refVelocity;

        #region UnityMethods
        private void Start()
        {
            HandleCamera();
        }

        private void Update()
        {
            HandleCamera();
        }
        #endregion

        protected virtual void HandleCamera()
        {
            if(!m_target)
            {
                return;
            }

            var worldPosition = (Vector3.forward * -m_distance) + (Vector3.up * m_height);
            //Debug.DrawLine(m_target.position, worldPosition, Color.red);

            Vector3 rotatedVector = Quaternion.AngleAxis(m_angle, Vector3.up) * worldPosition;
            //Debug.DrawLine(m_target.position, worldPosition, Color.green);

            var flatTargetPosition = m_target.position;
            //flatTargetPosition.y = 0f;
            var finalPosition = flatTargetPosition + rotatedVector;
            //Debug.DrawLine(m_target.position, finalPosition, Color.blue);
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref _refVelocity, m_smoothSpeed);
            transform.LookAt(m_target.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            if (m_target)
            {
                Gizmos.DrawLine(transform.position, m_target.position);
                Gizmos.DrawSphere(m_target.position, 1.5f);
            }
            Gizmos.DrawSphere(transform.position, 1.5f);
        }
    }
}
