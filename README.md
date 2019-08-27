# Unity Android and iOS App Detection Plugin
Unity Android and iOS Ability to Detect if a specific app has been installed

Download the [Unity Package Here](https://github.com/the16bitgamer/Unity_Android_iOS_App_Detection/raw/master/16BitAppDetection.unitypackage)

## Checking if an App is installed
To check if an app is installed first you need to initilize the app with

```GetAppInfo appCheck = new GetAppInfo();```

To Check if an app is installed call ```CheckInstalledApp(string APPID)```

Android and iOS have different ways of checking if an Application is installed Follow the Guides bellow to make sure you have everything setup to check for the installed apps

## Checking Android Apps
On Android App checking is done through the Package Manager in the **appcheck-release.aar** in ./Assets/Plugins/Android

To Check for an Android App you will need to the **Package Name** for the app, i.e. com.android.Chrome for Google Chrome

Look at ```CheckInstalledApp.cs``` for an example of this

## Checking iOS Apps
On iOS App checking is a lot more complicated since there is no Package Manager.
So there is no way to check to see if an app is actually installed. But there is a workaround.

How the 16 Bit Unity App Detection on iOS works is by checking if there is an App installed which can open a specific web link.

The code for this is located in ```AppCheck.mm``` in ./Assets/Plugins/iOS

For example if you were to open a facebook link on Safari, Safari might open the Facebook app is it is installed.

So what you need to do is add the ability to open your app with a unique weblink which to my knowledge cannot be done in Unity but I could be wrong.

0. Get the URL's for the Applications you want to search for or make up your own
   - We can add the ability to search for your app but you need to think for Unique URL's for your apps, i.e. MyCoolApp
   - For Third Party Apps try Google but I cannot guarentee if they would have them

1. Add you checks to your code and build your App to Xcode
   - In Xcode we can add the attributes for the App Sensing
   
2. Add The Following to your **.plist**


### If you want your app to be detected add the Following
```
<key>CFBundleURLTypes</key>
<array>
  <dict>
	  <key>CFBundleURLSchemes</key>
		<array>
		  <string>INSERT_URL_WHICH_THE_APP_CAN_REACT_TO_HERE</string>
		</array>
		<key>CFBundleURLName</key>
		  <string>INSERT_APP_ID_HERE</string>
	</dict>
</array>
```
INSERT_URL_WHICH_THE_APP_CAN_REACT_TO_HERE can be anything like ```APP00``` or ```MyCoolApp``` just so long as it would work as a url

INSERT_APP_ID_HERE is your application bundle ID I use ```com.CompanyName.${PRODUCT_NAME}``` for my apps

### If you want to detect other apps
```
<key>LSApplicationQueriesSchemes</key>
    <array>
        <string>App00</string>
        <string>App01</string>
        <string>App(n-1)</string>
    </array>
 ```
 
Just change the App00 and so on to whatever you need to check for.
