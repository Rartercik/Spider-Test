using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Text))]
    public class TextDecorator : MonoBehaviour
    {
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
        }

        public void SetCount(int count)
        {
            _text.text = count.ToString();
        }
    }
}