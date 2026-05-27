using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace CustomCamera
{
    [RequireComponent(typeof(Camera))]
    public class CameraBehaviour : MonoBehaviour
    {
        #pragma warning disable
        
        [SerializeField] private List<Transform> targets;

        [SerializeField] private Vector3 offset;

        [SerializeField] private Vector3 tilt;

        [SerializeField] private float smoothTime = .5f;
        
        [SerializeField] private float minZoom = 40f;

        [SerializeField] private float maxZoom = 10f;

        [SerializeField] private float zoomLimiter = 50f;

        private Vector3 _velocity;
        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (targets.Count == 0)
                return;

            CameraMove();
            CameraZoom();
        }
        

        private void CameraMove()
        {
            RemoveNullFromTargets();
            
            Vector3 centerPoint = GetCenterdPoint();

            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, smoothTime);
            transform.eulerAngles = new Vector3 (tilt.x, tilt.y, tilt.z);
        }

        

        private void CameraZoom()
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newZoom, Time.deltaTime);
        }

        private float GetGreatestDistance()
        {
            var bounds = new Bounds(targets[0].position,Vector3.zero);
            foreach (var t in targets)
            {
                bounds.Encapsulate(t.position);
            }

            return bounds.size.x;

        }

        private Vector3 GetCenterdPoint()
        {
            if (targets.Count == 1)
            {
                return targets[0].position;
            }

            var bounds = new Bounds(targets[0].position, Vector3.zero);
            foreach (var t in targets)
            {
                bounds.Encapsulate(t.position);
            }

            return bounds.center;
        }
        
        
        private void RemoveNullFromTargets()
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    targets.Remove(targets[i]);
                }
            }
        }
    }
}