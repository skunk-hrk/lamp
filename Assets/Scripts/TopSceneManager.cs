using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class TopSceneManager : MonoBehaviour
{
    // 一時保存用
    public GameObject type_temporary;

    // データ関連
    public GameObject data_relation;

    private BannerView bannerView;

    // Use this for initialization
    void Start()
    {

        // デバッグ用毎回追加
        SetFirstCredit();
        // データチェック
        if (!IsCreditData())
        {
            SetFirstCredit();
        }

        // 所持クレジット取得
        int _my_credit = data_relation.GetComponent<DataRelation>().GetCredit();

        // 現在時刻
        string _now_date = DateTime.Now.ToString("yyyyMMdd");

        // 更新日が今日じゃない かつ クレジットが規定枚数以下
        if (IsAddCreditByUpdateDate(_now_date) && IsAddCreditByCredit(_my_credit))
        {
            SetFirstCredit();
        }

        #if UNITY_ANDROID
            // string appId = "ca-app-pub-9934448897883222~5946818253";
            string appId = "ca-app-pub-3940256099942544~3347511713"; // for test
        #elif UNITY_IPHONE
            string appId = "not_activated_for_iphone";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            // string adUnitId = "ca-app-pub-9934448897883222/9310498137";
            string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // for test
        #elif UNITY_IPHONE
            string adUnitId = "not_activated_for_iphone (adUnitId)";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
        Debug.LogFormat("LoadAd Called. {0}", adUnitId);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        Debug.Log("1");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
        Debug.Log("2");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        Debug.Log("3");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
        Debug.Log("4");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // クレジットデータ有無チェック
    private bool IsCreditData()
    {
        return data_relation.GetComponent<DataRelation>().IsCreditData();
    }

    // 初回のクレジット登録
    private void SetFirstCredit(){
        data_relation.GetComponent<DataRelation>().SetFirstCredit();
    }

    // クレジット更新日判定
    private bool IsAddCreditByUpdateDate(string _now){
        // 更新日取得
        string _update_date = data_relation.GetComponent<DataRelation>().GetUpdateDate();
        return (_now != _update_date) ? true : false;
    }

    // クレジット枚数判定
    private bool IsAddCreditByCredit(int _credit){
        return (_credit < data_relation.GetComponent<DataRelation>().daily_spending) ? true : false;
    }

    // 設定リセット判定(更新日)
    private bool IsResetSettingByUpdateDate(string _now, int _type)
    {
        // 更新日取得
        string _update_date = data_relation.GetComponent<DataRelation>().GetProbabilitySettingUpdateDate(_type);
        return (_now != _update_date) ? true : false;
    }

    // スタート押下
    public void OnStartBtn()
    {
        // 現在時刻
        string _now_date = DateTime.Now.ToString("yyyyMMdd");

        //int type = type_temporary.GetComponent<TypeTemporary>().GetTypeOTENTEN();
        int type = 1;
        type_temporary.GetComponent<TypeTemporary>().SetType(type);

        if(IsResetSettingByUpdateDate(_now_date, type)){
            data_relation.GetComponent<DataRelation>().ResetProbabilitySetting(type);
        }
        SceneManager.LoadScene("GameScene");
    }

    // 交換押下
    public void OnChangeBtn()
    {
        Debug.Log("test2");
        SceneManager.LoadScene("ChangeScene");
    }

    // アイテム押下
    public void OnItemBtn()
    {
        Debug.Log("test3");
        SceneManager.LoadScene("ItemScene");
    }
}
