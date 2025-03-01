using System;
using UnityEngine;

namespace Proje_1
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;
        Camera _camera;

        void Awake() {
            _camera = Camera.main;
        }

        void TryMark() {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) return;
            if (hit.collider.TryGetComponent(out XgridCell cell)) {
                cell.MarkX();
                //call check here or call check in MarkX()
            }
        }

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                TryMark();
            }
        }
    }
}