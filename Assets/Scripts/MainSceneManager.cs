using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MainSceneManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnClickButton(string sceneName)
        {
            Common.CommonFunction.MoveScene(sceneName);
        }
    }
}