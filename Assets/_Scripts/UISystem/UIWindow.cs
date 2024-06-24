using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PVZ.UISystem
{
    public abstract class UIWindow : MonoBehaviour
    {

        public event Action<UIWindow, WindowResult> CloseHandle;
        public enum WindowResult
        {
            //窗口关闭类型枚举
            None, //默认关闭，不做其它处理
            Yes,   //点击了确认的关闭
            No,   //点击了取消或X的关闭
        }

        public virtual System.Type Type => GetType();


        private void Close(WindowResult windowResult = WindowResult.None)
        {
            //调用UIManager关闭此窗口
            UIManager.Instance.Close(Type);
            //发布关闭事件
            CloseHandle?.Invoke(this, windowResult);
            //关闭后清除注册事件
            CloseHandle = null;
        }

        //三种不同的窗口关闭方式
        public virtual void OnClickClose()
        {
            this.Close();
        }
        public virtual void OnYesClick()
        {
            this.Close(WindowResult.Yes);
        }
        public virtual void OnNoClick()
        {
            this.Close(WindowResult.No);
        }

    }
}


