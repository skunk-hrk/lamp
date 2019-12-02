using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOTENTEN : MonoBehaviour
{
    public const int GAME_TYPE = 1; // オカルトおテン店

    // 確率{ 131, 111, 136, 161, 178, 190};
    public List<int> probability_big = new List<int> { 152, 141, 139, 136, 119, 117 };
    public List<int> probability_reg = new List<int> { 142, 143, 140, 132, 190, 135 };
    public List<int> probability_setting = new List<int> { 30, 50, 68, 82, 90, 100};

    // 払い出しBIG
    public const int payout_big = 106;

    // 払い出しREG
    public const int payout_reg = 32;

    public const int BIG = 1;
    public const int REG = 2;
    public const int RAINBOW = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // ランプの色を変更
    public string SetRanp(int _bouns_type){
        string _img_name = "";

        switch(_bouns_type)
        {
            case BIG:
                _img_name = "type1/lamp_big";
                break;
            case REG:
                _img_name = "type1/lamp_reg";
                break;
            case RAINBOW:
                _img_name = "type1/lamp_rainbow";
                break;
        }

        return _img_name;
    }

    public int GetBonus(int _result){
        int get_credit = 0;
        switch (_result){
            case BIG:
                get_credit = 106;
                break;
            case REG:
                get_credit = 32;
                break;
            case RAINBOW:
                get_credit = 106;
                break;
        }
        return get_credit;
    }
}
