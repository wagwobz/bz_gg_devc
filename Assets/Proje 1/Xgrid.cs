using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Proje_1
{
    public class Xgrid : MonoBehaviour
    {
        public int size = 5;

        float _startingPosValue;
        Vector3 _startPos;

        [SerializeField] XgridCell cellPrefab;
        [SerializeField] List<XgridCell> cells = new List<XgridCell>();

        [ContextMenu("Create Grid")]
        public void ReCalculateGrid() {
            DestroyCells();
            _startingPosValue = size / 2f - 0.5f;
            _startPos = new Vector3(-_startingPosValue, 0, -_startingPosValue);
            CreateCells(size, _startPos);
        }

        [ContextMenu("Clear Grid")]
        void DestroyCells() {
            for (int i = 0; i < cells.Count; i++) {
                DestroyImmediate(cells[i].gameObject);
            }
            cells.Clear();
        }

        void CreateCells(int gridSize, Vector3 startingPos) {
            for (int i = 0; i < gridSize * gridSize; i++) {
                var x = i % gridSize;
                var z = i / gridSize;
                var pos = startingPos + new Vector3(x, 0, z);
                var cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                cell.gameObject.name = "Cell" + i;
                cells.Add(cell);
            }
        }
    }
}