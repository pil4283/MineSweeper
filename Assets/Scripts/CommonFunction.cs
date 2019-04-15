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
        /// 씬 이동
        /// </summary>
        /// <param name="sceneName"></param>
        public static void MoveScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// 2차원배열에서 x,y가 범위 안에 있는지 검사
        /// </summary>
        public static bool IsInRange(int firstX, int firstY, int x, int y, int range)
        {
            
            return true;
        }
    }
}