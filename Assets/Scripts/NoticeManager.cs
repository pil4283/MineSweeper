﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Common;

/// <summary>
/// 알림창 매니저 클래스
/// </summary>
public class NoticeManager : Singleton<NoticeManager>
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
    /// <summary>
    /// 알림문
    /// </summary>
    public Text noticeText;

    /// <summary>
    /// 알림창 활성화
    /// </summary>
    public void ActiveNotice(string noticeTitleText, string noticeText, string okButtonText = "확인", string cancelButtonText = "취소")
    {
        noticePanel.SetActive(true);
        this.noticeTitleText.text = noticeTitleText;
        this.noticeText.text = noticeText;
        this.okButtonText.text = okButtonText;
        this.cancelButtonText.text = cancelButtonText;

    }

    public void DisableNotice()
    {

        noticePanel.SetActive(false);
    }
    /// <summary>
    /// 확인창 클릭
    /// </summary>
    public void OnOKButtonClick()
    {
        //확인이벤트 발생
        CommonFunction.OnClickOKButton();
    }
    /// <summary>
    /// 취소창 클릭
    /// </summary>
    public void OnCancelButtonClick()
    {
        //취소이벤트 발생
        CommonFunction.OnClickCancelButton();
    }
}
