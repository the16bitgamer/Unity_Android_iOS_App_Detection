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

    bool compatible;

    void Initilized()
    {
        int iOSVersion = Int32.Parse(Device.systemVersion[0]);
        if(iOSVersion > 9)
        {
            compatible = true;
            InitilizeAppCheck();
        }
        else
        {
            Debug.LogWarning("16 Bit App Check Warning: Current Device is using iOS " + iOSVersion + ". Please update to iOS 10 or higher for App Detection to work");
        }
    }

    public bool CheckApps(string APP)
    {
        if(!compatible)
            return compatible;

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
