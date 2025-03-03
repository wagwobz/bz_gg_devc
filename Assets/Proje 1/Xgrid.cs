using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Proje_1
{
    public class Xgrid : MonoBehaviour
    {
        public int size = 5;

        float _startingPosValue;
        Vector3 _startPos;

        [SerializeField] XgridCell cellPrefab;
        [SerializeField] List<XgridCell> cells = new List<XgridCell>();

        GameManager _gameManager;

        [Inject]
        void Construct(GameManager gameManager) {
            _gameManager = gameManager;
        }

        void Start() {
            ReCalculateGrid();
        }

        [ContextMenu("Create Grid")]
        public void ReCalculateGrid() {
            DestroyCells();
            _startingPosValue = size / 2f - 0.5f;
            _startPos = new Vector3(-_startingPosValue, 0, -_startingPosValue);
            CreateCells(size, _startPos);
            Camera.main.orthographicSize = size;
        }

        [ContextMenu("Clear Grid")]
        public void DestroyCells() {
            for (int i = 0; i < cells.Count; i++) {
                if (cells[i]) DestroyImmediate(cells[i].gameObject);
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
                cell.SetCoordinates(x, z, i);
                cells.Add(cell);
            }
        }

        public void ControlMatch(int index) {
            var x = index % size;
            var y = index / size;

            var matchedCells = new List<XgridCell>();
            var currentCell = GetCellByPosition(x, y);
            matchedCells.Add(currentCell);
            var markedAdj = ControlAll(x, y);

            //maximum level is 2 
            if (markedAdj.Count > 0) {
                for (int i = 0; i < markedAdj.Count; i++) {
                    matchedCells.Add(markedAdj[i]);
                    var nextMarked = ControlAll(markedAdj[i].x, markedAdj[i].y);
                    if (nextMarked.Count > 0) {
                        for (int j = 0; j < nextMarked.Count; j++) {
                            if (!matchedCells.Contains(nextMarked[j])) matchedCells.Add(nextMarked[j]);
                            var secondNextMarked = ControlAll(nextMarked[j].x, nextMarked[j].y);
                        }
                    }
                }
            }

            if (matchedCells.Count > 2) {
                Debug.Log($"Matched {matchedCells.Count}");
                _gameManager.Matched();
                for (int i = 0; i < matchedCells.Count; i++) {
                    matchedCells[i].UnmarkX();
                }
            }
        }


        (bool, XgridCell) ControlPosition(int x, int y) {
            if (DoesPositionExist(x, y)) {
                var cell = GetCellByPosition(x, y);
                if (cell.markedX) {
                    return (true, cell);
                }
            }

            return (false, null);
        }

        List<XgridCell> ControlAll(int x, int y) {
            var right = ControlPosition(x + 1, y);
            var left = ControlPosition(x - 1, y);
            var up = ControlPosition(x, y + 1);
            var down = ControlPosition(x, y - 1);

            var asd = new List<XgridCell>();
            if (right.Item1) asd.Add(right.Item2);
            if (left.Item1) asd.Add(left.Item2);
            if (up.Item1) asd.Add(up.Item2);
            if (down.Item1) asd.Add(down.Item2);

            return asd;
        }

        bool DoesPositionExist(int x, int y) {
            return x >= 0 && x < size && y >= 0 && y < size;
        }

        XgridCell GetCellByPosition(int x, int y) {
            for (int i = 0; i < size * size; i++) {
                if (cells[i].x == x && cells[i].y == y) {
                    return cells[i];
                }
            }

            return null;
        }
    }
}