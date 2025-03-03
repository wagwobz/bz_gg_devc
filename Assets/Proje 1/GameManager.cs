using UnityEngine;
using Zenject;

namespace Proje_1
{
    public class GameManager : MonoBehaviour
    {
        #region Dependency Injection
        UiManager _uiManager;
        Xgrid _xgrid;
        
        [Inject]
        private void Construct(UiManager uiManager, Xgrid grid) {
            _uiManager = uiManager;
            _xgrid = grid;
        }
        #endregion

        public int matchCount = 0;

        public void Matched() {
            matchCount++;
            _uiManager.UpdateMatchCountText(matchCount);
        }
    }
}