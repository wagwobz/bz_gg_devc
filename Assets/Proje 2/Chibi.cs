using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Proje_2
{
    public class Chibi : MonoBehaviour
    {
        #region DI
        GameManager _gameManager;
        
        [Inject]
        void Construct(GameManager gameManager) {
            _gameManager = gameManager;
        }
        #endregion
        
        static readonly int Dance = Animator.StringToHash("dance");
        static readonly int Start = Animator.StringToHash("start");
        static readonly int Fail = Animator.StringToHash("fail");
        Animator _animator;

        [SerializeField] float speed = 3f;
        bool _moving = false;

        void Awake() {
            _animator = GetComponentInChildren<Animator>();
        }

        void Update() {
            if (_moving) {
                Move();
                GroundControl();
            }
        }

        void Move() {
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        }

        void GroundControl() {
            Physics.Raycast(transform.position- Vector3.forward + Vector3.up, transform.TransformDirection(Vector3.down), out RaycastHit hit);
            if (hit.collider == null) {
                ChibiFall();
            }
            else {
                print(hit.collider.gameObject.name);
            }
        }

        public void StartMoving() {
            _moving = true;
            _animator.SetTrigger(Start);
        }

        public void ChibiFall() {
            _moving = false;
            _animator.SetTrigger(Fail);
            transform.DOMoveY(-10, 2f).OnComplete(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        public void Center(Vector3 position) {
            transform.DOKill();
            transform.DOMoveX(position.x, 0.5f);
        }

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Finish")) {
                _moving = false;
                _animator.SetTrigger(Dance);
                _gameManager.StartLevelEnd();
            }
        }
    }
    
    
}
