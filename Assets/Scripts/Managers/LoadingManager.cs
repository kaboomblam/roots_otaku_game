using System.Collections;
using System.Collections.Generic;
using OtakuGameJam.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OtakuGameJam
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadingCanvas;

        // Start is called before the first frame update
        void Start()
        {
            _loadingCanvas.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Scene Management

        public void LoadScene(SceneIndex index)
        {
            var scene = SceneManager.LoadSceneAsync((int)index);

            // show loading ui incase scene takes a while to load
            StartCoroutine(LoadingScene(scene));

        }

        IEnumerator LoadingScene(AsyncOperation scene)
        {
            do
            { // do atleast once
                Debug.Log("Loading scene... progress ${scene.progress}%");
                yield return new WaitForSeconds(1f);
            } while (!scene.isDone);
        }

        #endregion
    }
}
