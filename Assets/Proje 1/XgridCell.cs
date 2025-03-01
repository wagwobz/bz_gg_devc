using UnityEngine;

namespace Proje_1
{
    public class XgridCell : MonoBehaviour
    {
        public bool markedX = false;
        [SerializeField] GameObject xGameObject;

        [ContextMenu("Mark X")]
        public void MarkX() {
            markedX = true;
            xGameObject.SetActive(true);
        }

        [ContextMenu("Unmark X")]
        public void UnmarkX() {
            markedX = false;
            xGameObject.SetActive(false);
        }
    }
}