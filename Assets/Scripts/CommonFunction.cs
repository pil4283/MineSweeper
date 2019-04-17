using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public static class CommonFunction
    {
        public delegate void OKButtonClick();
        public static event OKButtonClick okButtonClick;

        public delegate void CancelButtonClick();
        public static event CancelButtonClick cancelButtonClick;

        /// <summary>
        /// 알림창의 OK버튼 클릭 이벤트
        /// </summary>
        public static void OnClickOKButton()
        {
            try
            {
                okButtonClick();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 알림창의 Cancel버튼 클릭 이벤트
        /// </summary>
        public static void OnClickCancelButton()
        {
            try
            {
                cancelButtonClick();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 씬 이동
        /// </summary>
        /// <param name="sceneName"></param>
        public static void MoveScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// 2차원배열에서 x,y가 범위 안에 있는지 검사
        /// TODO : 활용방법 찾아보기?
        /// </summary>
        public static bool IsInRange(int firstX, int firstY, int x, int y, int range)
        {
            
            return true;
        }

        /// <summary>
        /// 입력한 값이 이메일인지 확인
        /// </summary>
        /// <param name="mailAddr"></param>
        /// <returns></returns>
        public static bool IsEmail(string mailAddr)
        {
            
            return false;
        }
    }

    /// <summary>
    /// 싱글턴 매니저
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if(!_instance)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                    if(!_instance)
                    {
                        //Todo : 오류처리
                    }
                }
                return _instance;
            }
        }
    }
}