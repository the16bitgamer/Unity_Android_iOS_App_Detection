using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInstalledApp : MonoBehaviour {

    //We are Checking if Robotipede is installed on your current Device

    //For Android App we check the Bundle ID.
    public string AppToCheckAndroid = "com.SixteenBitGames.Bezerk";

    //For iOS we needed a bit of a work around, so we are checking if the app can open a specific URL and we are checking if that URL can be opened
    public string AppToCheckiOS = "SixteenBitApp06";

    public string WebStoreForAndroid = "https://play.google.com/store/apps/details?id=com.SixteenBitGames.Bezerk";
    public string WebStoreForiOS = "https://itunes.apple.com/us/app/new-berzerk/id1171452875?ls=1&mt=8";

    public Text textBx;
    public Image img;

    GetAppInfo appCheck;
    
    void Start () {
        appCheck = new GetAppInfo();
    }
	
	void Update () {

        string appToCheck = AppToCheckiOS;
        if(CheckAndroid)
        {
            appToCheck = AppToCheckAndroid;
        }

        if (appCheck.CheckInstalledApp(appToCheck))
        {
            AppInstalled();
        }
        else
        {
            AppNotInstalled();
        }
    }

    public void DownloadApp()
    {
        string webStore = WebStoreForiOS;
        if (CheckAndroid)
        {
            webStore = WebStoreForAndroid;
        }

        Application.OpenURL(webStore);
    }

    void AppInstalled()
    {
        textBx.text = "App is Installed";
        img.color = new Color(0, 1, 1);
    }
    void AppNotInstalled()
    {
        textBx.text = "App is Not Installed";
        img.color = new Color(1, 0, 0);
    }

#if UNITY_ANDROID
    bool CheckAndroid
    {
        get {   return true; }
    }
#else
    bool CheckAndroid
    {
        get {   return false; }
    }
#endif
}
