using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIInterface : MonoBehaviour
{

    public Text TimeText;
    public Text Name;
    public Image DistanceBar;


    private bool SkillBtCheck()  //  어택 버튼 눌렸을 때 스킬까지 사용하는 지 아닌지 체크
    {
        while (Input.GetMouseButton(0)) // 마우스가 계속 눌리는 동안 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            print("pressed");
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {

                string tag = hit.transform.gameObject.tag;
                
                if (tag == "SkillButton")
                {

                    return true;

                }
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
