using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Proje_2
{
    [DefaultExecutionOrder(-1)]
    public class PlatformController : MonoBehaviour
    {
        #region DI
        GameManager _gameManager;
        Chibi _chibi;
        AudioManager _audioManager;
        
        [Inject]
        void Construct(GameManager gameManager,Chibi chibi, AudioManager audioManager) {
            _gameManager = gameManager;
            _chibi = chibi;
            _audioManager = audioManager;
        }
        #endregion
        
        [SerializeField] GameObject platformPrefab;
        [SerializeField] Platform startPlatform;
        [SerializeField] float perfectTolerance = 0.05f;
        
        Platform _currentPlatform;
        Platform _previousPlatform;
        
        float _currentSide = -1f;
        bool _gameOn = false;
        
        readonly Vector3 _startOffset = Vector3.right * 3f;
        readonly Vector3 _forwardOffset = Vector3.forward * 3f;
        
        bool _moving;

        void Update() {
            if (Input.GetMouseButtonDown(0) && !_gameManager.finish && _gameOn) {
                StopCurrentPlatform();
            }
        }
        
        public void StartPlatforms() {
            _gameOn = true;
            NextPlatform(startPlatform);
        }

        void StopCurrentPlatform() {
            var localPos = _currentPlatform.Stop();
            var diff = localPos.x - _previousPlatform.transform.localPosition.x;
            
            print(diff);
            
            if (Mathf.Abs(diff)/3f <= perfectTolerance) {
                diff = 0f;
                _audioManager.PlaySound(true);
            }
            else if (Mathf.Abs(diff)/3f > _previousPlatform.transform.localScale.x) {
                //Cinemachine stop follow
                //fail
                _gameManager.PlatformFail();
                return;
            }
            else {
                _audioManager.PlaySound(false);
            }
            _gameManager.PlatformSuccess();
            Split(diff);
            
        }
        
        //I adjusted platform scale on X and Z to 3f(due to proportions of Chibi) so we had to multiply/divide by 3f to make calculations correct 
        void Split(float difference) {
            var direction = difference > 0 ? 1f : -1f;
            var newXScale = _previousPlatform.transform.localScale.x - Mathf.Abs(difference) / 3f;
            var fallingPartXScale = _currentPlatform.transform.localScale.x * 3f - newXScale * 3f;
            
            var newXPosition = _previousPlatform.transform.localPosition.x + difference / 2f;
            _currentPlatform.transform.localScale = new Vector3(newXScale, 1f, 1f);
            _currentPlatform.transform.localPosition = new Vector3(newXPosition, 
                _currentPlatform.transform.localPosition.y, _currentPlatform.transform.localPosition.z);

            var edge = _currentPlatform.transform.localPosition.x + newXScale * 3f / 2f * direction;
            
            var fallingPartXPos = edge + fallingPartXScale / 2f * direction;

            if (!Mathf.Approximately(fallingPartXScale, 0f)) {
                FallingPart(fallingPartXScale, fallingPartXPos);
            }
            
            _chibi.Center(_currentPlatform.transform.position);
            
            if (!_gameManager.finish) {
                NextPlatform(_currentPlatform);
            }
        }

        void FallingPart(float fallingBlockXSize, float fallingPlatformXPos) {
            var fallingPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
            fallingPlatform.transform.parent = transform;
            fallingPlatform.transform.localScale = new Vector3(fallingBlockXSize, 0.4f, 3f);
            fallingPlatform.transform.position = new Vector3(fallingPlatformXPos, -0.1f, _currentPlatform.transform.localPosition.z);
            fallingPlatform.transform.DOMoveY(-10f,5f).SetEase(Ease.OutQuint).OnComplete(() => {
                Destroy(fallingPlatform);
            });
        }

        void NextPlatform(Platform previousPlatform) {
            _previousPlatform = previousPlatform;
            _currentSide *= -1;
            var previousScale = _previousPlatform.transform.localScale;
            var position = _previousPlatform.transform.localPosition + _forwardOffset + _startOffset * _currentSide;
            _currentPlatform = Instantiate(platformPrefab, position, Quaternion.identity, transform)
                .GetComponent<Platform>();
            _currentPlatform.transform.localScale = previousScale;
            _currentPlatform.Init(_currentSide);
        }

        
    }
}