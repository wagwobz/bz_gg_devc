using UnityEngine;

namespace Proje_1
{
    public class XgridCell : MonoBehaviour
    {
        public bool markedX = false;
        [SerializeField] GameObject xGameObject;
        public int x;
        public int y;
        public int index;

        public void SetCoordinates(int x, int y, int index) {
            this.x = x;
            this.y = y;
            this.index = index;
        }

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