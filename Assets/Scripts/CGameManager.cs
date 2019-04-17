using System.Collections;
using System.Collections.Generic;
using Model;
using Common;
using UnityEngine;

using Random = UnityEngine.Random;

public class CGameManager : MonoBehaviour
{
    /// <summary>
    /// 타일 프리팹
    /// </summary>
    public GameObject tileBlockPrefab;
    GameObject[,] blockArray;

    /// <summary>
    /// 첫 클릭여부
    /// </summary>
    private bool isFirstTouch = true;

    /// <summary>
    /// 이미지 0 : 일반, 1~8 : 지뢰갯수 9 : 지뢰, 10 : 공개된타일(일반이면서 지뢰개수가 없는타일), 11 : 공백타일
    /// </summary>
    public Sprite[] sprites;
    
    private void Start()
    {
        //Todo : 임시코드 맵 제이슨파일을 불러올 수 있게되면 수정
        StaticData.maxXSize = 25;
        StaticData.maxYSize = 25;
        StaticData.mineQty = 99;

        //맵정보를 가져온 뒤 맵생성
        blockArray = new GameObject[StaticData.maxXSize, StaticData.maxYSize];
        CreateMap();

        //맵을 만든 뒤 지뢰 세팅
        //SetMine();
    }

    private void Update()
    {
#if UNITY_EDITOR
        //에디터 확인용
        //Todo : 안드로이드용 버전 만들때 아래거쓰고 스토어에 올릴때 삭제
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] rayHit = Physics2D.RaycastAll(pos, Vector2.zero, 0f);
            foreach(var hit in rayHit)
            {
                if(hit.collider.gameObject.CompareTag("Block"))
                {
                    GameObject hitBlock = hit.collider.gameObject;
                    Tile tile = hitBlock.GetComponent<MineBlock>().tile;
                    
                    //첫번째 클릭은 무조건 지뢰가 아니게 조정
                    if (isFirstTouch)
                    {
                        isFirstTouch = false;
                        SetRandomMine(tile.x, tile.y);
                    }

                    if (tile.isBlind)
                        Check(tile);
                    else
                        return;
                }
            }
        }

        //#elif UNITY_ANDROID
        /*if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
        }*/
#elif UNITY_ANDROID
        //안드

#elif UNITY_IOS
        //아이폰?? 

#endif
    }

    /// <summary>
    /// 맵 생성
    /// </summary>
    /// <param name="xNum"></param>
    /// <param name="yNum"></param>
    private void CreateMap()
    {
        //첫블럭의 위치
        int xStartPos = 0 - StaticData.maxXSize / 2;
        int yStartPos = 0 - StaticData.maxYSize / 2;

        //각 블럭이 생성될 위치
        int xPos = xStartPos;
        int yPos = yStartPos;

        for (int y = 0;y < StaticData.maxYSize;y++)
        {
            for (int x = 0;x < StaticData.maxXSize;x++)
            {
                blockArray[x, y] = Instantiate(tileBlockPrefab, new Vector3(xPos * 1, yPos * -1), Quaternion.identity);

                blockArray[x, y].GetComponent<MineBlock>().tile.x = x;
                blockArray[x, y].GetComponent<MineBlock>().tile.y = y;
                blockArray[x, y].name = "x-" + x + " y-" + y;

                blockArray[x, y].transform.SetParent(transform);

                xPos++;
            }
            xPos = xStartPos;
            yPos++;
        }

        //지뢰는 첫 클릭할 때 설치됨
    }

    /// <summary>
    /// 블럭에 지뢰를 세팅
    /// </summary>
    /// <param name="firstX"></param>
    /// <param name="firstY"></param>
    private void SetRandomMine(int firstX, int firstY)
    {
        int mineX, mineY;

        for (int i = 0 ; i < StaticData.mineQty ; i++) 
        {
            mineX = Random.Range(0, StaticData.maxXSize);
            mineY = Random.Range(0, StaticData.maxYSize);

            //처음 클릭한 블럭과 그 주변은 무조건 지뢰블럭이 아니게 세팅(구글 MineSweeper참고)
            //Todo : 맵 json파일에서 RandomMine값을 만들어서 랜덤한 위치 or 특정한 위치에 지뢰생성 조건을 생성하는 함수를 따로 제작해보기
            if(IsSafeArea(firstX, firstY, mineX, mineY, 2))
            {
                i--;
                continue;
            }
            /*if(mineX == firstX && mineY == firstY)
            {
                //mineX +- 3 , mineY +- 3
                i--;
                continue;
            }*/

            //일반블럭인지 체크
            if (blockArray[mineX, mineY].GetComponent<MineBlock>().tile.type == 1)
            {
                //해당타일을 지뢰로 바꿈
                blockArray[mineX, mineY].GetComponent<MineBlock>().tile.type = 2;
                //주변 타일의 숫자+1
                AddNeighborNumber(mineX, mineY);
            }
            else
            {
                i--;
            }
        }

        //블럭마다 스프라이트 지정
        for (int y = 0 ; y < StaticData.maxYSize ; y++)
        {
            for (int x = 0 ; x < StaticData.maxXSize ; x++)
            {
                SpriteRenderer publicRenderer = blockArray[x, y].GetComponent<MineBlock>().publicSprite;
                SpriteRenderer blindRenderer = blockArray[x, y].GetComponent<MineBlock>().blindSprite;
                Tile tile = blockArray[x, y].GetComponent<MineBlock>().tile;

                switch (tile.type)
                {
                    case 1://일반
                        blindRenderer.sprite = sprites[tile.mineNumber];
                        break;
                    case 2://지뢰
                        blindRenderer.sprite = sprites[9];
                        break;
                    case 3://공백
                        publicRenderer.sprite = sprites[10];
                        blindRenderer.sprite = sprites[10];
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 블럭 클릭 시 블럭의 종류에따라 처리
    /// </summary>
    /// <param name="tile"></param>
    private void Check(Tile tile)
    {
        switch (tile.type)
        {
            case 1://일반타일
                blockArray[tile.x, tile.y].GetComponent<MineBlock>().OpenBlock();
                FindProgress(tile.x, tile.y);
                break;
            case 2://지뢰타일
                GameOver();
                break;
            case 3://공백타일
                return;
            default:
                return;
        }

        //게임오버 체크
    }
    
    /// <summary>
    /// 지뢰주변의 타일 숫자를 높임
    /// </summary>
    private void AddNeighborNumber(int x, int y)
    {
        /*
		 * 11 /   12   / 1
		 * 9  /  MINE  / 3
		 * 7  /   6    / 5
		 */

        //11
        if (IsExist(x - 1, y - 1))
            blockArray[x - 1, y - 1].GetComponent<MineBlock>().tile.mineNumber++;
        //9
        if(IsExist(x - 1,y))
            blockArray[x - 1, y].GetComponent<MineBlock>().tile.mineNumber++;
        //7
        if (IsExist(x - 1, y + 1))
            blockArray[x - 1, y + 1].GetComponent<MineBlock>().tile.mineNumber++;
        //1
        if(IsExist(x + 1, y - 1))
            blockArray[x + 1, y - 1].GetComponent<MineBlock>().tile.mineNumber++;
        //3
        if (IsExist(x + 1, y))
            blockArray[x + 1, y].GetComponent<MineBlock>().tile.mineNumber++;
        //5
        if (IsExist(x + 1, y + 1))
            blockArray[x + 1, y + 1].GetComponent<MineBlock>().tile.mineNumber++;
        //12
        if(IsExist(x, y - 1))
            blockArray[x, y - 1].GetComponent<MineBlock>().tile.mineNumber++;
        //6
        if(IsExist(x, y + 1))
            blockArray[x, y + 1].GetComponent<MineBlock>().tile.mineNumber++;
    }

    /// <summary>
    /// 선택한곳 주변에 지뢰가 없다면 지뢰옆(숫자)블럭까지 밝힘
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void FindProgress(int x, int y)
    {
        /*
		 * 11 /   12   / 1
		 * 9  /  MINE  / 3
		 * 7  /   6    / 5
		 */
        //숫자블럭은 바로 종료
        if (blockArray[x, y].GetComponent<MineBlock>().tile.mineNumber != 0)
            return;
        
        //11
        if (IsExist(x - 1, y - 1))
            OpenBlockProgress(x - 1, y - 1);
        //9
        if (IsExist(x - 1, y))
            OpenBlockProgress(x - 1, y);
        //7
        if (IsExist(x - 1, y + 1))
            OpenBlockProgress(x - 1, y + 1);
        //1
        if (IsExist(x + 1, y - 1))
            OpenBlockProgress(x + 1, y - 1);
        //3
        if (IsExist(x + 1, y))
            OpenBlockProgress(x + 1, y);
        //5
        if (IsExist(x + 1, y + 1))
            OpenBlockProgress(x + 1, y + 1);
        //12
        if (IsExist(x, y - 1))
            OpenBlockProgress(x, y - 1);
        //6
        if (IsExist(x, y + 1))
            OpenBlockProgress(x, y + 1);
    }

    /// <summary>
    /// 해당 블록이 범위를 벗어나는지 확인
    /// </summary>
    /// <param name="x">타겟X좌표</param>
    /// <param name="y">타겟Y좌표</param>
    /// <returns>결과</returns>   
    private bool IsExist(int x, int y)
    {
        if ((0 <= x) && (x <= StaticData.maxXSize - 1) && (0 <= y) && (y <= StaticData.maxYSize - 1))
        {
            return true;
        }

        return false;
    }

    private void OpenBlockProgress(int x, int y)
    {
        if (blockArray[x, y].GetComponent<MineBlock>().tile.isBlind && blockArray[x, y].GetComponent<MineBlock>().tile.type == 1)
        {
            blockArray[x, y].GetComponent<MineBlock>().OpenBlock();
            FindProgress(x, y);
        }
    }

    /// <summary>
    /// 처음 클릭한 곳 주변 타일은 무조건 일반블럭으로 생성
    /// </summary>
    /// <param name="firstX"></param>
    /// <param name="firstY"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool IsSafeArea(int firstX, int firstY, int x, int y, int range)
    {
        if (firstX - range < x && x < firstX + range &&
            firstY - range < y && y < firstY + range)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 일반 타일과 조작불가 타일을 제외한 지뢰, 블럭스프라이트를 전달
    /// </summary>
    public Sprite GetSprite(int type)
    {
        //0 : 지뢰, 1~8 : 숫자 9 : 깃발
        return sprites[type];
    }

    /// <summary>
    /// 지뢰 블럭을 열고 종료처리
    /// </summary>
    public void GameOver()
    {
        for(int y = 0 ; y < StaticData.maxYSize; y++)
        {
            for(int x = 0 ; x < StaticData.maxXSize ; x++)
            {
                if(blockArray[x,y].GetComponent<MineBlock>().tile.type == 2)
                {
                    //TODO:게임오버 메세지창 띄우고 재시작OR메뉴로 돌아가기 만들기
                    blockArray[x, y].GetComponent<MineBlock>().OpenBlock();
                    NoticeManager.Instance.ActiveNotice("게임 오버", "다시 시작하시겠습니까?", "다시하기", "돌아가기");
                    CommonFunction.okButtonClick += OkButtonClick;
                    CommonFunction.cancelButtonClick += CancelButtonClick;
                }
            }
        }
    }

    private void OkButtonClick()
    {

        CommonFunction.okButtonClick -= OkButtonClick;
        CommonFunction.cancelButtonClick -= CancelButtonClick;
    }
    private void CancelButtonClick()
    {

        CommonFunction.okButtonClick -= OkButtonClick;
        CommonFunction.cancelButtonClick -= CancelButtonClick;
    }
}