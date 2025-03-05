using UnityEngine;

namespace Proje_2
{
    public class Chibi : MonoBehaviour
    {
        Animator _animator;

        void Awake() {
            _animator = GetComponentInChildren<Animator>();
        }
    }
    
    
}
