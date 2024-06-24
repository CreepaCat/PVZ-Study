using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PVZ.UI
{
    public class LoadingBarUI : MonoBehaviour
    {

        [SerializeField] float loadingTime = 2f;
        //float currentTimer = 0f;
        [SerializeField] Slider progressSlider = null;

        float progress = 0;
        [SerializeField] GameObject handler = null;
        [SerializeField] Button enterButton = null;
        [SerializeField] bool isUseRealTime = false;

        AsyncOperation operation;
        void Start()
        {
            enterButton.onClick.AddListener(OnEnter);
            enterButton.gameObject.SetActive(false);

            handler.transform.DORotate(new Vector3(0, 0, -360), 1, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

            if (isUseRealTime)
            {
                operation = SceneManager.LoadSceneAsync("Menu");
                operation.allowSceneActivation = false;
            }

        }

        void Update()
        {
            Loading();

        }

        private void Loading()
        {
            if (isUseRealTime)
            {
                //异步加载
                progress = operation.progress / 0.9f;
            }
            else
            {
                //预设加载
                progress += Time.deltaTime / loadingTime;

            }

            progress = Mathf.Clamp01(progress);
            progressSlider.value = progress;

            if (progress >= 1.0)
            {
                enterButton.gameObject.SetActive(true);
            }
        }

        void OnEnter()
        {
            if (isUseRealTime)
            {
                operation.allowSceneActivation = true;
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }

            DOTween.Clear(); //清除dotween动画
        }
    }
}


