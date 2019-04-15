using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class MineBlock : MonoBehaviour
{
    public Tile tile = new Tile();

    public bool isBlind { get; set; }
    public int mineNumber;
    public int x;
    public int y;
    public int type;
    
    //TODO : spriterenderer의 color로 색상 커스텀기능
    //공백 이미지 - 아직 아무런 조작도 하지 않은 상태
    public SpriteRenderer publicSprite;

    //가려진 이미지(지뢰, 숫자)
    public SpriteRenderer blindSprite;

    private void Start()
    {
        publicSprite = GetComponent<SpriteRenderer>();
        blindSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        x = tile.x;
        y = tile.y;
        type = tile.type;
        mineNumber = tile.mineNumber;
    }

    /// <summary>
    /// 타일을 열고 번호가 있다면 표시
    /// </summary>
    public void OpenBlock()
    {
        tile.isBlind = false;
        //publicSprite.sprite = null;
        publicSprite.enabled = false;
        blindSprite.enabled = true;
    }
}
