using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._01.Member.CDH.Code.Cores
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private string gameSceneName;
        [SerializeField] private RectTransform loadingBar;
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] private int frameDuration = 10;

        private void Start()
        {
            LoadNextSceneAsync();
        }

        private async void LoadNextSceneAsync()
        {
            float minLoadingTime = 0.5f;
            float startTime = Time.time;

            AsyncOperation op = SceneManager.LoadSceneAsync(gameSceneName);
            op.allowSceneActivation = false;

            string[] dp = { "", ".", "..", "..." };
            int cnt = 0;

            while (!op.isDone)
            {
                cnt++;
                float progress = Mathf.Clamp01(op.progress / 0.9f);
                loadingBar.localScale = new Vector3(progress, 1f, 1f);
                if (cnt % frameDuration == 0)
                {
                    if (cnt == 4 * frameDuration)
                        cnt = 0;
                    loadingText.text = "Loading" + dp[cnt / frameDuration];
                }

                // 로딩 끝났지만 최소 시간 안 지났으면 대기
                if (progress >= 1f && Time.time - startTime >= minLoadingTime)
                {
                    op.allowSceneActivation = true;
                }

                await Awaitable.NextFrameAsync();
            }
        }
    }
}
