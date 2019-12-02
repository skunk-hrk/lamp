using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    // 自分の現在のクレジット
    private int my_credite = 0;

	// Use this for initialization
	void Start () {

        //SetCredit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // クレジットをセットする
    void SetCredit(int _num){
        my_credite = _num;
    }

    int GetCrediet()
    {

        return 0;
    }

}
