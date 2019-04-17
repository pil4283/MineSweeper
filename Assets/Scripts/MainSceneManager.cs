using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    public void OnClickMoveSceneButton(string sceneName)
    {
        Common.CommonFunction.MoveScene(sceneName);
    }
}