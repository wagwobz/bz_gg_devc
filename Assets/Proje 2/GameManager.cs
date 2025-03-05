using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace Proje_2
{
    public class GameManager : MonoBehaviour
    {
        #region DI
        PlatformController _platformController;
        PreviousPlatform _previousPlatform;
        CinemachineVirtualCamera _virtualCamera;
        CinemachineOrbitalTransposer _orbitalTransposer;
        Chibi _chibi;
        
        [Inject]
        void Construct(PlatformController platformController, PreviousPlatform previousPlatform, CinemachineVirtualCamera virtualCamera, Chibi chibi) {
            _platformController = platformController;
            _previousPlatform = previousPlatform;
            _virtualCamera = virtualCamera;
            _orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            _chibi = chibi;
        }
        #endregion
        
        int _currentPlatformNumber = 0;
        int _maxPlatformNumber = 10;

        bool _gameOn = false;
        bool _gameStart = false;
        public bool finish;
        
        void Start() {
            print(_previousPlatform.gameObject.name);
            _previousPlatform.AdjustPosition();
        }

        void Update() {
            if (!_gameStart && Input.GetMouseButtonDown(0)) {
                GameStart();
            }
        }

        void GameStart() {
            _gameStart = true;
            _gameOn = true;
            _platformController.StartPlatforms();
            _chibi.StartMoving();
        }

        public void PlatformSuccess() {
            _currentPlatformNumber++;
            if (_currentPlatformNumber >= _maxPlatformNumber) {
                _gameOn = false;
                finish = true;
            }
        }

        public void PlatformFail() {
            print("fail");
        }

        IEnumerator Finish() {
            StartCoroutine(Rotate());
            yield return new WaitForSeconds(5f);
            List<Transform> children = new List<Transform>();
            foreach (Transform child in _platformController.transform) {
                children.Add(child);
            }
            _previousPlatform.ChangeChilds(children.ToArray());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public IEnumerator Rotate()
        {
            while (true)
            {
                _orbitalTransposer.m_XAxis.Value +=  100 * Time.deltaTime;
                yield return null;
            }
        }

        public void StartLevelEnd() {
            StartCoroutine(Finish());
        }
    }
}
