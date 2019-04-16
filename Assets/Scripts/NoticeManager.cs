using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 알림창 매니저 클래스
/// </summary>
public class NoticeManager : MonoBehaviour
{
    /// <summary>
    /// 알림패널 
    /// </summary>
    public GameObject noticePanel;
    /// <summary>
    /// 알림창의 확인(또는 긍정적인 반응)버튼
    /// </summary>
    public Button okButton;
    /// <summary>
    /// 알림창의 취소(또는 부정적인 반응)버튼
    /// </summary>
    public Button cancelButton;
    /// <summary>
    /// 확인버튼의 텍스트, 기본-확인
    /// </summary>
    public Text okButtonText;
    /// <summary>
    /// 취소버튼의 텍스트, 기본-취소
    /// </summary>
    public Text cancelButtonText;
    /// <summary>
    /// 알림창의 제목 텍스트
    /// </summary>
    public Text noticeTitleText;

    private void Awake()
    {
        
    }

    /// <summary>
    /// 알림창 활성화
    /// </summary>
    public void ActiveNotice()
    {

    }
}
