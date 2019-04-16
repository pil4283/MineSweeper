using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public static class CommonFunction
    {
        public delegate void OnClickOKButton();
        public static event OnClickOKButton okButtonClick;

        public delegate void OnClickCancelButton();
        public static event OnClickCancelButton cancelButtonClick;

        /// <summary>
        /// 알림창의 OK버튼 클릭 이벤트
        /// </summary>
        public static void ClickOKButton()
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
        public static void ClickCancelButton()
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
}