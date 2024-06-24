using System;
using Core;
using PVZ.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PVZ.UISystem
{

    /// <summary>
    /// 场景主UI的脚本，用来开关其它UI
    /// </summary>
    public class UIMain : MonoBehaviour
    {


        static UIMain _instance = null;

        public static UIMain Instance => _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            _instance = null;
        }


        public Button UITestBtn = null;
        public Button UITest2Btn = null;

        void Awake()
        {
            //mono单例
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        void Start()
        {
            UITestBtn.onClick.AddListener(OnClickUITestButton);
            UITest2Btn.onClick.AddListener(OnClickUITest2Button);
        }

        //======一个窗口的两种开关方式=======

        //方式一：用一个按钮控制窗口显示
        void OnClickUITestButton()
        {
            UIManager.Instance.OnButtonClick<UITest>();
            // uITest.CloseHandle += OnCloseUITest;
        }

        void OnClickUITest2Button()
        {
            UIManager.Instance.OnButtonClick<UITest2>();
            // uITest.CloseHandle += OnCloseUITest;
        }


        //方式二：点击显示，注册关闭事件回调
        void OnShowUITest()
        {
            UITest uITest = UIManager.Instance.Show<UITest>();
            uITest.CloseHandle += OnCloseUITest;
        }

        private void OnCloseUITest(UIWindow sender, UIWindow.WindowResult result)
        {
            // throw new NotImplementedException();
            Debug.Log("关闭窗口" + sender.gameObject.name + "，关闭结果" + result.ToString());
        }


    }
}


