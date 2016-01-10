using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour
{

    public Text TimeText;
    public Text PlayerName;
    public Text CompanyName;
    public Image DistanceBar;
    public Image JumpButton;
    public Image AttackButton;
    public Image SkillButton;

    private int _layerMask;

    void Awake() {

        TimeText.text = "00:01";
        PlayerName.text = "대리";
        CompanyName.text = "NCSOFT";
        DistanceBar.fillAmount = 0.5f;
        print("button");


    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            if (CheckTouchUI(Input.mousePosition, AttackButton))
            {
                AttackBt();

            }
            else if (CheckTouchUI(Input.mousePosition, JumpButton))
            {

                JumpBt();
            }



        }
    }
    private bool CheckTouchUI(Vector3 position, Image img) {

        if (img.transform.position.x + img.rectTransform.rect.width / 2 > position.x
            && img.transform.position.x - img.rectTransform.rect.width / 2 < position.x
            && img.transform.position.y + img.rectTransform.rect.height / 2 > position.y
            && img.transform.position.y - img.rectTransform.rect.height / 2 < position.y) {

            return true;

        }

        return false;
    }

  
    private bool SkillBtCheck()  //  어택 버튼 눌렸을 때 스킬까지 사용하는 지 아닌지 체크
    {
        while (Input.GetMouseButton(0)) // 마우스가 계속 눌리는 동안 
        {
            if (CheckTouchUI(Input.mousePosition, SkillButton)) {
                return true;
            }
            
        }

        return false;

    }

    public void AttackBt()  // 어택 버튼 눌렸을 때 실행
    {
        bool value = SkillBtCheck();
        

        if (value)
        {
            //skill  실전
            Debug.Log("스킬 시전");


        }
        else {

            Debug.Log("공격 시전");
        }

    }

    public void JumpBt()    // 점프  버튼 눌렸을 떄 실행
    {
        // 점프 액션 
        Debug.Log("점프 시전");

    }

    

}
