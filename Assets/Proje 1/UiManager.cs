using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Proje_1
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] Button rebuildGridButton;
        [SerializeField] TMP_InputField gridSizeInputField;
        [SerializeField] TextMeshProUGUI matchCountTMP;

        Xgrid _xGrid;
    
        [Inject]
        private void Construct(Xgrid grid) {
            _xGrid = grid;
        }

        void Awake() {
            rebuildGridButton.onClick.AddListener(RecalculateGridSize);
        }

        void Start() {
            gridSizeInputField.text = _xGrid.size.ToString();
        }

        void RecalculateGridSize() {
            _xGrid.size = int.Parse(gridSizeInputField.text);
            _xGrid.ReCalculateGrid();
        }

        public void UpdateMatchCountText(int matchCount) {
            matchCountTMP.text = $"Match Count: {matchCount}";
        }
    }
}


