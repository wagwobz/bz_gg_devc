using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Proje_2
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] GameObject platformPrefab;

        Platform _currentPlatform;
        Platform _previousPlatform;
        [SerializeField] Platform startPlatform;
        float _currentSide = -1f;
        Vector3 _startOffset = Vector3.right * 3f;
        Vector3 _forwardOffset = Vector3.forward * 3f;
        Vector3 _previousPlatformPosition = Vector3.zero;

        bool moving;

        void Start() {
            NextPlatform(startPlatform);
        }

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                StopCurrentPlatform();
            }
        }

        void StopCurrentPlatform() {
            var localPos = _currentPlatform.Stop();
            var diff = localPos.x - _previousPlatform.transform.localPosition.x;

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
            var fallingPlatformXPos = edge + fallingPartXScale / 2f * direction;

            FallingPart(fallingPartXScale, fallingPlatformXPos);
            NextPlatform(_currentPlatform);
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