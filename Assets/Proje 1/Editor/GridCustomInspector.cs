using UnityEditor;
using UnityEngine;

namespace Proje_1.Editor
{
    [CustomEditor(typeof(Xgrid))]
    public class GridCustomInspector : UnityEditor.Editor
    {
        Xgrid _xGrid;
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            _xGrid = (Xgrid)target;
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Prepare Grid"))
            {
                _xGrid.ReCalculateGrid();
            }

            if (GUILayout.Button("Clear Grid")) {
                _xGrid.DestroyCells();
            }
            GUILayout.EndHorizontal();
            EditorUtility.SetDirty(_xGrid);
            
            
        }
    }
}
