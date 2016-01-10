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

    private int flag;

    void Awake() {

        flag = 0;
        TimeText.text = "00:01";
        PlayerName.text = "대리";
        CompanyName.text = "NCSOFT";
        DistanceBar.fillAmount = 0.5f;
        print("button");


    }


    void Update() {

        ButtonCheck();

    }

    private void ButtonCheck() {

        // flag 는 공격 버튼 눌렀을 때 활성화

        if (flag == 1 && Input.GetMouseButton(0)) {
            if (CheckTouchUI(Input.mousePosition, SkillButton)) { // 드래그 한 것이 스킬 버튼까지 오면
                SkillBt();
                flag = 0;
            }

        } else if (Input.GetMouseButtonUp(0) && flag == 1)    //  공격 버튼 누르고 떼면
        {
            if (CheckTouchUI(Input.mousePosition, AttackButton)) 
            {
                flag = 1;
                AttackBt();
            }
        } else if (Input.GetMouseButtonDown(0)) {

            if (CheckTouchUI(Input.mousePosition, AttackButton)){
                flag = 1;

            } else if (CheckTouchUI(Input.mousePosition, JumpButton)){

                JumpBt();
            }

        }else {

            flag = 0;
        }

    }

    private bool CheckTouchUI(Vector3 position, Image img) {        //버튼이 사각형 일 때 

        if (img.transform.position.x + img.rectTransform.rect.width / 2 > position.x
            && img.transform.position.x - img.rectTransform.rect.width / 2 < position.x
            && img.transform.position.y + img.rectTransform.rect.height / 2 > position.y
            && img.transform.position.y - img.rectTransform.rect.height / 2 < position.y) {

            return true;

        }

        return false;
    }

    private bool CheckTouchUIRound(Vector3 position, Image img) // 버튼이 원일 때
    {
        float r = img.rectTransform.rect.width / 2;
        if ((img.transform.position.x - position.x) * (img.transform.position.x - position.x) 
            + (img.transform.position.y - position.y) * (img.transform.position.y - position.y) < r*r){

            return true;

        }

        return false;
    }


    public void SkillBt()  // 어택 버튼 눌렸을 때 실행
    {

        Debug.Log("스킬 시전");
        
    }

    public void AttackBt()  // 어택 버튼 눌렸을 때 실행
    {

       Debug.Log("공격 시전");
       
    }

    public void JumpBt()    // 점프  버튼 눌렸을 떄 실행
    {
        // 점프 액션 
        Debug.Log("점프 시전");

    }

    

}
