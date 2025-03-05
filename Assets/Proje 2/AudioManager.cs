using UnityEngine;

namespace Proje_2
{
    public class AudioManager : MonoBehaviour
    {
        AudioSource _audioSource;
        float _pitch = 1f;
        
        void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                PlaySound(true);
            }
            
        }

        public void PlaySound(bool perfect) {
            _pitch = !perfect ? 1f : _pitch+0.15f;
            _audioSource.pitch = _pitch;
            _audioSource.Play();
            
        }
    }
}