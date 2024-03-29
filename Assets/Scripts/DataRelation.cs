﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRelation : MonoBehaviour {

    public int daily_spending = 106;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // クレジットデータのチェック
    public bool IsCreditData(){
        bool _credit_data = PlayerPrefs.HasKey("Credit") ? true : false;
        return _credit_data;
    }

    // 初回クレジット設定
    public void SetFirstCredit(){
        PlayerPrefs.SetInt("Credit", daily_spending);
        SetUpdateDate();
    }

    // クレジット設定
    public void SetCredit(int _num){
        PlayerPrefs.SetInt("Credit", _num);
    }

    // クレジット取得
    public int GetCredit(){
        return PlayerPrefs.GetInt("Credit");
    }

    // クレジット更新日更新
    public void SetUpdateDate(){
        string _now = DateTime.Now.ToString("yyyyMMdd");
        PlayerPrefs.SetString("UpdateDate", _now);
    }

    // クレジット更新日取得
    public string GetUpdateDate(){
        return PlayerPrefs.GetString("UpdateDate");
    }

    // 設定確認
    public bool IsTypeSettingData(int _type){
       string _type_setting = "type" + _type + "Setting";
        return PlayerPrefs.HasKey(_type_setting) ? true : false;
    }

    // 設定を保存
    public void SetProbabilitySetting(int _type, int _setting){
        string _type_setting = "type" + _type + "Setting";
        PlayerPrefs.SetInt(_type_setting, _setting);
        SetProbabilitySettingUpdateDate(_type);
    }

    // 設定を取得
    public int GetProbabilitySetting(int _type)
    {
        string _type_setting = "type" + _type + "Setting";
        return PlayerPrefs.GetInt(_type_setting);
    }

    // 設定更新日更新
    public void SetProbabilitySettingUpdateDate(int _type)
    {
        string _type_setting = "type" + _type + "SettingUpdateDate";
        string _now = DateTime.Now.ToString("yyyyMMdd");
        PlayerPrefs.SetString(_type_setting, _now);
    }

    // 設定更新日取得
    public string GetProbabilitySettingUpdateDate(int _type)
    {
        string _type_setting = "type" + _type + "SettingUpdateDate";
        return PlayerPrefs.GetString(_type_setting);
    }

    // 設定をリセット
    public void ResetProbabilitySetting(int _type){
        string _type_setting_update = "type" + _type + "SettingUpdateDate";
        string _type_setting = "type" + _type + "Setting";
        PlayerPrefs.DeleteKey(_type_setting_update);
        PlayerPrefs.DeleteKey(_type_setting);
    }
}
