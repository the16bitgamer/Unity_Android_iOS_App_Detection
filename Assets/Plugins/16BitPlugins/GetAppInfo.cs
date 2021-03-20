using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GetAppInfo {

    public GetAppInfo()
    {
        Initilized();
    }

    public bool CheckInstalledApp(string APPID)
    {
        return CheckApps(APPID);
    }

#if UNITY_EDITOR
    void Initilized()
    {
        Debug.Log("Initilized on AppCheck");
    }

    private bool CheckApps(string APP)
    {
        return false;
    }
#elif UNITY_IOS

    [DllImport("__Internal")]
    private static extern void InitilizeAppCheck();

    [DllImport("__Internal")]
    private static extern bool CheckApp(string URL);


    void Initilized()
    {   
        InitilizeAppCheck();
    }

    public bool CheckApps(string APP)
    {
        bool check = CheckApp(APP);
        return check;
    }
#elif UNITY_ANDROID
    private AndroidJavaObject GetApps = null;
	private AndroidJavaObject activityContext = null;

	void Initilized()
	{
		if (Application.platform == RuntimePlatform.Android) {
			if (GetApps == null) {
				using (AndroidJavaClass activityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
					activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				}

				using (AndroidJavaClass pluginClass = new AndroidJavaClass ("com.sixteenbitpluggin.appcheck.SearchApps")) {
					Debug.Log ("Checks " +pluginClass);
					if (pluginClass != null) {
						GetApps = pluginClass.CallStatic<AndroidJavaObject> ("instance");
						GetApps.Call ("setContext", activityContext);
					}
				}
			}
		}
	}

	public bool CheckApps(string APP)
	{
        if (GetApps == null)
            Initilized();
        return GetApps.Call<bool>("appInstalledOrNot", APP);
	}
#endif
}
