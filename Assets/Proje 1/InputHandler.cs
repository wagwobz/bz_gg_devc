using System;
using UnityEngine;
using Zenject;

namespace Proje_1
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;
        Camera _camera;

        Xgrid _xGrid;
        [Inject]
        void Construct(Xgrid xgrid) {
            _xGrid = xgrid;
        }

        void Awake() {
            _camera = Camera.main;
        }

        void TryMark() {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) return;
            if (hit.collider.TryGetComponent(out XgridCell cell)) {
                cell.MarkX();
                _xGrid.ControlMatch(cell.index);
            }
        }

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                TryMark();
            }
        }
    }
}