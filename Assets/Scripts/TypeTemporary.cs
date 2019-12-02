using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeTemporary : MonoBehaviour {

    // 台タイプnone
    public const int TYPE_NOTSET = 0; // 未設定

    // 台タイプ設定用
    private static int type = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // タイプをセット
    public void SetType(int select_type){
        type = select_type;
    }

    // タイプを取得
    public int GetType(){
        return type;
    }

    // タイプをリセット
    void ResetType(){
        type = TYPE_NOTSET;
    }
}
