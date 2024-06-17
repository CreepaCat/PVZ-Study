using PVZ.Level;
using PVZ.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PVZ.UI
{
    public class LevelProgressUI : MonoBehaviour
    {
        [SerializeField] Image progress = null;
        [SerializeField] TextMeshProUGUI levelLabel = null;
        [SerializeField] Image zombieHead = null;
        [SerializeField] Image waveFlagPrefab = null;
        [SerializeField] RectTransform bg = null;

        [SerializeField]
        float percentageTest = 0.5f;

        int currentWaveZombieSpawned = 0;
        int currentWaveZombieTotal = 0;
        LevelManager levelManager;

        void Start()
        {
            levelManager = GameObject.FindObjectOfType<LevelManager>();
            levelManager.OnNewWave += OnNewWave;
            // SetProgressPercent(percentageTest);
            SpawnManager spawnManager = GameObject.FindObjectOfType<SpawnManager>();
            spawnManager.OnZombieNumChange += OnZombieNumChange;

            // currentWaveZombieSpawned = levelManager.currentWaveZombieTotalNum;

            currentWaveZombieTotal = levelManager.currentWaveZombieTotalNum; //当前波预计生成僵尸总数

            SetFlagPercent();


        }

        void Update()
        {
            // UpdateProgress();
        }

        public void SetProgressPercent(float percent)
        {
            progress.fillAmount = percent;
            //设置僵尸头icon位置
            RectTransform headRT = zombieHead.GetComponent<RectTransform>();
            Vector3 headPosition = headRT.anchoredPosition3D;

            float endOffset = 0;
            if (percent == 1.0f)
            {
                endOffset = headRT.sizeDelta.x * 0.5f;
            }
            zombieHead.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-bg.rect.width * percent + endOffset, headPosition.y, headPosition.z);
        }

        private void SetFlagPercent()
        {
            //按波次将flag显示到对应进度
            // LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            LevelInfoItem levelInfoItem = levelManager.GetCurrentLevelInfoItem();

            levelLabel.text = levelInfoItem.levelName;

            for (var i = 0; i < levelInfoItem.progressPercent.Length; i++)
            {
                //  Debug.Log("放置旗子");
                PlaceFlags(levelInfoItem.progressPercent[i]);
            }
        }

        private void PlaceFlags(float percent)
        {
            Image waveFlag = Instantiate(waveFlagPrefab, bg.transform);

            RectTransform flagRT = waveFlag.GetComponent<RectTransform>();
            Vector3 flagPosition = flagRT.anchoredPosition3D;

            float endOffset = 0;
            if (percent == 1.0f)
            {
                endOffset = flagRT.sizeDelta.x * 0.5f;
            }
            flagRT.anchoredPosition3D = new Vector3(-bg.rect.width * percent + endOffset, flagPosition.y, flagPosition.z);
        }

        //根据当前波剩余僵尸数量/当前波总僵尸数量 得出当前波的进程，并更新UI
        void UpdateProgress()
        {
            LevelInfoItem levelInfoItem = levelManager.GetCurrentLevelInfoItem();
            int currentWaveID = levelManager.currentWaveID;

            float currentWavePercentageInfo = levelInfoItem.progressPercent[levelManager.currentWaveID - 1]; //当前波的百分比占比
            /// Debug.Log("当前波次百分比配置" + currentWavePercentageInfo);
            /// Debug.Log("当前波僵尸总数：" + currentWaveZombieTotal);
            /// Debug.Log("当前波已生成僵尸：" + currentWaveZombieSpawned);

            float lastWavePercentage = 0;
            if (currentWaveID > 1) //波数ID从1开始
            {
                lastWavePercentage = levelInfoItem.progressPercent[levelManager.currentWaveID - 2];

            }
            ///Debug.Log("上一波次波次百分占比" + lastWavePercentage);

            //当前波次实际占比：配置百分比- 上一波配置百分比
            //int/int结果会自动四舍五入为int类型，想保留小数应先转化为float
            float currentWavePerInTotal = ((float)currentWaveZombieSpawned / (float)currentWaveZombieTotal) * (currentWavePercentageInfo - lastWavePercentage);

            //总进程 = 前面进程之和 + 当前进程
            float percentageInTotalWave = currentWavePerInTotal + lastWavePercentage;
            ///Debug.Log("当前进程百分比" + percentageInTotalWave);

            SetProgressPercent(percentageInTotalWave);
        }

        //当生成新僵尸事件回调
        void OnZombieNumChange(int numToChange)
        {
            currentWaveZombieSpawned += numToChange;
            UpdateProgress();
        }

        //下一波事件回调
        void OnNewWave()
        {
            //本波僵尸数 = 当前总僵尸数 - 之前波僵尸数
            currentWaveZombieTotal = levelManager.currentWaveZombieTotalNum - currentWaveZombieTotal;
            currentWaveZombieSpawned = 0;
        }


    }
}


