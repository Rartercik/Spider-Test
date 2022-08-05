using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    [RequireComponent(typeof(Canvas))]
    public class Restarter : MonoBehaviour
    {
        [SerializeField] private float _secondsBeforeGameOver;

        private Canvas _canvas;

        private void Start()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void StartShowingEnding()
        {
            StartCoroutine(ShowEndingIn(_secondsBeforeGameOver));
        }

        private IEnumerator ShowEndingIn(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Time.timeScale = 0;
            _canvas.enabled = true;
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}