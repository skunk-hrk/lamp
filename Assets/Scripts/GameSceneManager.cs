using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public const int GAME_TYPE_OTENTEN = 1; // オカルトおテン店
    // 一時保存スクリプト
    public GameObject type_temporary;

    // データ関連
    public GameObject data_relation;

    // 計算関連
    public GameObject calculation;

    // クレジット表示用テキスト
    public GameObject credite_text_object;

    // ゲームタイプスクリプト格納用
    public GameObject game_type;
    private List<int> probability_setting_distribution;
    private List<int> big_probability_distribution;
    private List<int> reg_probability_distribution;
    private int big_probability;
    private int reg_probability;

    // ロゴ用イメージ
    public GameObject bg_img;

    // ランプ用イメージ
    public GameObject lamp_img;
    public bool bouns_flg = false;

    // ロゴ用イメージ
    public GameObject logo_img;

    // スロットボタン
    public GameObject slt_btn;
    public bool slt_btn_flg = true;

    // 所持クレジット
    public int my_credit = 0;

    // 台type
    private int type = 1;

    // 台の設定
    private int type_setting = 0;

    // Use this for initialization
    void Start()
    {
        // デバッグ用設定リセット
        data_relation.GetComponent<DataRelation>().ResetProbabilitySetting(type);

        // タイプをセットする。
        SetType();

        // 設定を取得する
        Debug.Log(IsTypeSettingData());
        type_setting = IsTypeSettingData() ? GetTypeSetting() : SetTypeSetting();
        big_probability = big_probability_distribution[type_setting];
        reg_probability = reg_probability_distribution[type_setting];

        Debug.Log(type_setting);
        Debug.Log(big_probability);
        Debug.Log(reg_probability);
        Debug.Log("--------");
        SaveTypeSetting();

        // 設定から各ボーナス確率を取得して格納　＞　確率を元に抽選を行う　＞　ボーナスを引いたらそれに合わせて画像を変化させる

        // 所持クレジットを取得する
        my_credit = GetCredite();

        // ゲーム画面をセットする(クレジット枚数)
        SetCrediteText(my_credit);

        // ゲーム画面をセットする(背景)
        SetBgImg();

        // ゲーム画面をセットする(ロゴ)
        SetLogoImg();

        // ゲーム画面をセットする(ランプ)
        SetLampImg();

        // 試行可能判定
        if (IsBetValidity())
        {
            SetSlotBtnOnImg();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 試行開始
    public void BetCredite()
    {
        if (IsBetValidity() && slt_btn_flg)
        {
            SlotBtnFlgOff();

            // クレジット消費
            ConsumptionCredite();

            // 消費クレジットアニメーション
            AnimationConsumptionCredit();

            // 抽選結果
            int result = GameResult();

            // 画面出力
            if (result != 0)
            {
                string _lamp_img_src = "";
                int get_credit = 0;
                bouns_flg = true;
                // ボーナス
                switch (type)
                {
                    case GAME_TYPE_OTENTEN:
                        _lamp_img_src = game_type.GetComponent<TypeOTENTEN>().SetRanp(result);
                        lamp_img.GetComponent<Image>().sprite = Resources.Load<Sprite>(_lamp_img_src);
                        get_credit = game_type.GetComponent<TypeOTENTEN>().GetBonus(result);
                        my_credit = my_credit + get_credit;
                        break;
                }

                // ボーナスアニメーション
                AnimationBouns();
            }

            // クレジット更新
            SetCredite(my_credit);
            SetCrediteText(my_credit);

            // ボタン押下できるようにする
            Invoke("SlotBtnFlgOn", 0.4f);
        }
        else
        {
            // クレジット0枚
        }
    }

    // 抽選
    private int GameResult()
    {
        return calculation.GetComponent<Calculation>().GameResult(type, big_probability, reg_probability);
    }

    private void SlotBtnFlgOn(){
        // ボタン画像on状態に変化させる
        slt_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("btn_slot");

        slt_btn_flg = true;
    }

    private void SlotBtnFlgOff(){
        // ボタン画像off状態に変化させる
        slt_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("btn_slot_push");

        slt_btn_flg = false;
        if(bouns_flg)
        {
            lamp_img.GetComponent<Image>().sprite = Resources.Load<Sprite>("type1/lamp_normal");
        }
    }

    // 消費アニメーション
    private void AnimationConsumptionCredit(){
        // 消費アニメーション
    }

    // ボーナス時アニメーション
    private void AnimationBouns(){
        // タイプから取得できるクレジット数を取得して、その分クレジットふやすアニメーションを行う
    }

    // ゲーム内の台タイプをセットする
    private void SetType(){
        type = type_temporary.GetComponent<TypeTemporary>().GetType();
        switch(type)
        {
            case GAME_TYPE_OTENTEN:
                game_type.AddComponent<TypeOTENTEN>();
                probability_setting_distribution = game_type.GetComponent<TypeOTENTEN>().probability_setting;
                big_probability_distribution = game_type.GetComponent<TypeOTENTEN>().probability_big;
                reg_probability_distribution = game_type.GetComponent<TypeOTENTEN>().probability_reg;
                break;
        }
    }

    // 設定があるか確認
    private bool IsTypeSettingData(){
        return data_relation.GetComponent<DataRelation>().IsTypeSettingData(type) ? true : false; ;
    }

    // 設定を取得する
    private int GetTypeSetting(){
        return data_relation.GetComponent<DataRelation>().GetProbabilitySetting(type);
    }

    // 設定を決める
    private int SetTypeSetting(){
        int setting_percent = calculation.GetComponent<Calculation>().DecideProbability(probability_setting_distribution, type);
        return setting_percent;
    }

    // 設定を保存する
    private void SaveTypeSetting(){
        data_relation.GetComponent<DataRelation>().SetProbabilitySetting(type, type_setting);
    }

    // クレジット数値をセットする
    private void SetCrediteText(int num)
    {
        credite_text_object.GetComponent<Text>().text = num.ToString() + "クレジット";
    }

    private void SetCredite(int num)
    {
        data_relation.GetComponent<DataRelation>().SetCredit(num);
    }

    // クレジットを取得する
    private int GetCredite(){
        return data_relation.GetComponent<DataRelation>().GetCredit();
    }

    // ゲーム画面をセットする(背景)
    private void SetBgImg()
    {
        string _img_url = "type" + type + "/bg";
        Sprite _image = Resources.Load<Sprite>(_img_url);

        bg_img.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        bg_img.GetComponent<Image>().sprite = _image;
    }

    // ゲーム画面をセットする(ロゴ)
    private void SetLogoImg(){
        string _img_url = "type" + type + "/logo";
        Sprite _image = Resources.Load<Sprite>(_img_url);

        logo_img.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        logo_img.GetComponent<Image>().sprite = _image;
    }

    // ゲーム画面をセットする(ランプ)
    private void SetLampImg()
    {
        string _img_url = "type" + type + "/lamp_normal";
        Sprite _image = Resources.Load<Sprite>(_img_url);

        lamp_img.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        lamp_img.GetComponent<Image>().sprite = _image;
    }

    // クレジットチェック
    private bool IsBetValidity(){
        return (my_credit > 0) ? true : false;
    }

    // スロットボタン変更
    private void SetSlotBtnOnImg(){
        slt_btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("btn_slot");
    }

    // クレジット消費
    private void ConsumptionCredite(){
        my_credit = calculation.GetComponent<Calculation>().UseCredit(my_credit, 1);
    }
}
