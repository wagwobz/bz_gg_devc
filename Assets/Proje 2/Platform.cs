using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Proje_2
{
    public class Platform : MonoBehaviour
    {
        bool _stopped = true;
        bool _moving = false;
        public bool startPlatform = false;
        public float startingSide = 1f;
        
        public void Init(float currentSide) {
            _stopped = false;
            startingSide = currentSide;
        }
        void Start() {
            print(transform.localPosition);
        }
        
        void Update() {
            if (!_moving && !_stopped && !startPlatform) {
                StartCoroutine(MovePlatform());
            }
        }

        public Vector3 Stop() {
            _stopped = true;
            _moving = false;
            StopAllCoroutines();
            return transform.localPosition;
        }

        IEnumerator MovePlatform() {
            _moving = true;
            var startPos = transform.localPosition;
            var endPos = startPos + Vector3.right * (-6f * startingSide);

            var elapsedTime = 0f;
            const float endTime = 1f;

            while (elapsedTime<endTime) {
                transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / endTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = endPos;
            startPos = endPos;
            endPos = startPos + Vector3.right * (6f * startingSide);
            elapsedTime = 0f;
            
            while (elapsedTime<endTime) {
                transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / endTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = endPos;
            _moving = false;
        }

        
    }
}
