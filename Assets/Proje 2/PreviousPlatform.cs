using System;
using UnityEngine;
using Zenject;

namespace Proje_2
{
    public class PreviousPlatform: MonoBehaviour
    {
        #region Don't Destroy OnLoad
        private static PreviousPlatform _instance;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject); // Destroy duplicate instances
                return;
            }
        
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
        
        public Vector3 startingPos = Vector3.zero;
        
        public void ChangeChilds(Transform[] newChildren) {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }

            foreach (var child in newChildren) {
                child.SetParent(transform, false);
            }
            
            var lastChildPos = transform.GetChild(transform.childCount-1).position;
            startingPos = new Vector3(-lastChildPos.x, 0, -33f);
        }

        public void AdjustPosition() {
            transform.position = startingPos;
            var lastChildPos = transform.GetChild(transform.childCount-1).position;
            transform.position -= new Vector3(lastChildPos.x, 0, 0);
        }
    }
}