using UnityEngine;
using System.Collections;

using System;
using System.Runtime.InteropServices;


public class StartAppWrapperiOS : MonoBehaviour {
	
	private static string developerId;
	private static string applicatonId;
	private static Boolean initilized = false;
	
	private static STAUnityOrientation appOrientation = STAUnityOrientation.STAAutoRotation;
	

	private static bool pause = false;
	
	
	//Ad Preferences
	public class STAInterstitialProperties {
		public AdType type;
		public string delegateName;
	}
	//Ad enums
	public enum AdType{
		STAAdType_Automatic,
		STAAdType_FullScreen,
		STAAdType_OfferWall,
		STAAdType_Overlay
	};
	
	
	//Banner Preferences
	public class STABannerProperties {
		public BannerType type;
		public BannerPosition position;
		public BannerFixedPosition fixedPosition = new BannerFixedPosition();
		public bool useFixedPosition;
		public BannerSize size;
		public string delegateName;
	}
	//Banner enums
	public enum BannerType{
		AUTOMATIC
	};
	
	public enum BannerPosition{
		BOTTOM,
		TOP
	};
	
	public enum BannerSize{
		STA_AutoAdSize,
		STA_PortraitAdSize_320x50,
		STA_LandscapeAdSize_480x50,
		STA_PortraitAdSize_768x90,
		STA_LandscapeAdSize_1024x90
	};
	
	public class BannerFixedPosition {
		public int x;
		public int y;
	}
	
	
	//Splash Preferences
	public class STASplashPreferences {
		public STASplashMode mode;
		public STASplashMinTime minTime;
		public STASplashAdDisplayTime adDisplayTime;
		public STASplashTemplateTheme templateTheme;
		public string templateIconImageName;
		public string templateAppName;
		public string delegateName;
		public bool isLandscape;
		public STASplashLoadingIndicatorType loadingIndicatorType;
		public STALoadingIndicatorCenterPoint loadingIndicatorCenterPoint = new STALoadingIndicatorCenterPoint();
	}
	
	//Splash enums
	public enum STASplashMode{
		STASplashModeUserDefined = 1,
		STASplashModeTemplate = 2
	};
	
	
	public enum STASplashMinTime{
		STASplashMinTimeShort = 2,
		STASplashMinTimeRegular = 3,
		STASplashMinTimeLong = 5
	};
	
	public enum STASplashAdDisplayTime{
		STASplashAdDisplayTimeShort = 5,
		STASplashAdDisplayTimeLong = 10,
		STASplashAdDisplayTimeForEver = 86400
	};
	
	public enum STASplashTemplateTheme{
		STASplashTemplateThemeDeepBlue = 0,
		STASplashTemplateThemeSky,
		STASplashTemplateThemeAshenSky,
		STASplashTemplateThemeBlaze,
		STASplashTemplateThemeGloomy,
		STASplashTemplateThemeOcean
	};
	
	
	public enum STASplashLoadingIndicatorType{
		STASplashLoadingIndicatorTypeIOS = 0,
		STASplashLoadingIndicatorTypeDots
	};
	
	
	public class STALoadingIndicatorCenterPoint {
		public int x;
		public int y;
	}
	
	
	//Autorotation enum
	
	public enum STAUnityOrientation{
		STAPortrait = 0,
		STALandscape = 1,
		STAAutoRotation = 2
	};
	
	//Splash Ad enum conversions
	private static string getStringFromSTASplashMode(STASplashMode splashMode)
	{
		string mode = @"STASplashModeUserDefined";
		
		if(splashMode==STASplashMode.STASplashModeUserDefined){
			mode = @"STASplashModeUserDefined";
		}else if (splashMode==STASplashMode.STASplashModeTemplate) {
			mode = @"STASplashModeTemplate";
		}
		
		return mode;
	}
	
	private static string getStringFromSTASplashMinTime(STASplashMinTime splashMinTime)
	{
		string minTime = @"STASplashMinTimeLong";
		
		if(splashMinTime==STASplashMinTime.STASplashMinTimeLong){
			minTime = @"STASplashMinTimeLong";
		}else if (splashMinTime==STASplashMinTime.STASplashMinTimeRegular) {
			minTime = @"STASplashMinTimeRegular";
		}else if (splashMinTime==STASplashMinTime.STASplashMinTimeShort) {
			minTime = @"STASplashMinTimeShort";
		}
		
		return minTime;
	}
	
	private static string getStringFromSTASplashAdDisplayTime(STASplashAdDisplayTime splashAdDisplayTime)
	{
		string adDisplayTime = @"STASplashAdDisplayTimeForEver";
		
		if(splashAdDisplayTime==STASplashAdDisplayTime.STASplashAdDisplayTimeForEver){
			adDisplayTime = @"STASplashAdDisplayTimeForEver";
		}else if (splashAdDisplayTime==STASplashAdDisplayTime.STASplashAdDisplayTimeLong) {
			adDisplayTime = @"STASplashAdDisplayTimeLong";
		}else if (splashAdDisplayTime==STASplashAdDisplayTime.STASplashAdDisplayTimeShort) {
			adDisplayTime = @"STASplashAdDisplayTimeShort";
		}
		
		return adDisplayTime;
	}
	
	private static string getStringFromSTASplashTemplateTheme(STASplashTemplateTheme splashTemplateTheme)
	{
		string templateTheme = @"STASplashTemplateThemeAshenSky";
		
		if(splashTemplateTheme==STASplashTemplateTheme.STASplashTemplateThemeAshenSky){
			templateTheme = @"STASplashTemplateThemeAshenSky";
		}else if (splashTemplateTheme==STASplashTemplateTheme.STASplashTemplateThemeBlaze) {
			templateTheme = @"STASplashTemplateThemeBlaze";
		}else if (splashTemplateTheme==STASplashTemplateTheme.STASplashTemplateThemeDeepBlue) {
			templateTheme = @"STASplashTemplateThemeDeepBlue";
		}else if (splashTemplateTheme==STASplashTemplateTheme.STASplashTemplateThemeGloomy) {
			templateTheme = @"STASplashTemplateThemeGloomy";
		}else if (splashTemplateTheme==STASplashTemplateTheme.STASplashTemplateThemeOcean) {
			templateTheme = @"STASplashTemplateThemeOcean";
		}else if (splashTemplateTheme==STASplashTemplateTheme.STASplashTemplateThemeSky) {
			templateTheme = @"STASplashTemplateThemeSky";
		}
		
		return templateTheme;
	}
	
	private static string getStringFromSTASplashLoadingIndicatorType(STASplashLoadingIndicatorType splashLoadingIndicatorType)
	{
		string loadingIndicatorType = @"STASplashLoadingIndicatorTypeIOS";
		
		if(splashLoadingIndicatorType==STASplashLoadingIndicatorType.STASplashLoadingIndicatorTypeIOS){
			loadingIndicatorType = @"STASplashLoadingIndicatorTypeIOS";
		}else if (splashLoadingIndicatorType==STASplashLoadingIndicatorType.STASplashLoadingIndicatorTypeDots) {
			loadingIndicatorType = @"STASplashLoadingIndicatorTypeDots";
		}
		
		return loadingIndicatorType;
	}
	
	
	
	
	
	//Ad enum conversions
	private static string getStringFromBannerSize(BannerSize bannerSize)
	{
		string size = @"STA_AutoAdSize";
		if(bannerSize==BannerSize.STA_PortraitAdSize_320x50){
			size = @"STA_PortraitAdSize_320x50";
		}else if (bannerSize==BannerSize.STA_LandscapeAdSize_480x50) {
			size = @"STA_LandscapeAdSize_480x50";
		}else if (bannerSize==BannerSize.STA_PortraitAdSize_768x90) {
			size = @"STA_PortraitAdSize_768x90";
		}else if (bannerSize==BannerSize.STA_LandscapeAdSize_1024x90) {
			size = @"STA_LandscapeAdSize_1024x90";
		}else if (bannerSize==BannerSize.STA_AutoAdSize) {
			size = @"STA_AutoAdSize";
		}
		return size;
	}
	
	private static string getStringFromAdType(AdType adType)
	{
		string type = @"STAAdType_Automatic";
		if(adType==AdType.STAAdType_FullScreen){
			type = @"STAAdType_FullScreen";
		}else if (adType==AdType.STAAdType_Automatic) {
			type = @"STAAdType_Automatic";
		}else if (adType==AdType.STAAdType_OfferWall) {
			type = @"STAAdType_OfferWall";
		}else if (adType==AdType.STAAdType_Overlay) {
			type = @"STAAdType_Overlay";
		}
		return type;
	}
	
	//Banner enum conversions
	private static string getStringFromBannerPosition(BannerPosition bannerPosition)
	{
		string position = @"BOTTOM";
		if(bannerPosition==BannerPosition.TOP){
			position = @"TOP";
		}else {
			position = @"BOTTOM";
		}
		return position;
	}
	
	
	
	private static int getIntFromDeviceOrientation(DeviceOrientation orientation)
	{
		int orien = 0;
		if (orientation == DeviceOrientation.Portrait) {
			orien = 0;
		}else if (orientation == DeviceOrientation.PortraitUpsideDown) {
			orien = 1;
		}else if (orientation == DeviceOrientation.LandscapeLeft) {
			orien = 2;
		}else if (orientation == DeviceOrientation.LandscapeRight) {
			orien = 3;
		}
		return orien;
	}
	
	
	
	// Return ad enter background/foreground detection
	void OnApplicationPause (bool pauseStatus){
		if(pause!=pauseStatus){
			if(pauseStatus){
				_enterBackground();
			}else{
				_enterForeground();
			}
		}
		pause = pauseStatus;
	}
	
	
	
	
	
	//sdk initilize functions
	private static void checkInitilize()
	{
//		Debug.Log(
//			"Unity version      = " + Application.unityVersion);
		_setUnityVersion (Application.unityVersion);
		_setUnityAutoRotation (getUnityAutoRotationInt());
		_setUnitySupportedOrientations (unitySupportedOrientations());
		
		if (initilized)
			return;
		if (!initUserData ()) {
			throw new System.ArgumentException ("Error in initializing Application ID or Developer ID, Verify you initialized them in StartAppDataiOS in resources");
		} else {
			if(string.IsNullOrEmpty(developerId)){
				_sdkInitilize (applicatonId, "");
			}else{
				_sdkInitilize (applicatonId, developerId);
			}
			initilized = true;
		}
	}
	
	private static void sdkInitilize(string appId,string devId)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			_sdkInitilize(appId,devId);
		}
	}
	
	private static bool initUserData(){
		bool result = false;
		int assigned = 0;
		
		TextAsset data = (TextAsset)Resources.Load("StartAppDataiOS");
		string userData = data.ToString();
		
		string[] splitData = userData.Split('\n');
		string[] singleData;
		
		for (int i = 0; i < splitData.Length; i++){
			singleData = splitData[i].Split('=');
			if (singleData[0].ToLower().CompareTo("applicationid") == 0){
				assigned++;
				applicatonId = singleData[1].ToString().Trim();
			}
			
			if (singleData[0].ToLower().CompareTo("developerid") == 0){
				assigned++;
				developerId = singleData[1].ToString().Trim();
			}else if (singleData[0].ToLower().CompareTo("accountid") == 0){
				assigned++;
				developerId = singleData[1].ToString().Trim();
			}
		}
		
		if (assigned >= 1){
			result = true;
		}
		return result;
	}
	
	
	
	
	
	//sdk initilize
	
	[DllImport ("__Internal")]
	private static extern void _sdkInitilize(string appId, string devId);
	
	
	
	// Splash ad methods
	[DllImport ("__Internal")]
	private static extern void _showSplashAd(string splashMode,string splashMinTime,string splashAdDisplayTime,string splashTemplateTheme,string splashLoadingIndicatorType,string objectDelegate,string splashTemplateIconImageName,string splashTemplateAppName,int splashLoadingIndicatorCenterPointX,int splashLoadingIndicatorCenterPointY,bool isLandscape);
	
	
	
	// Return ad methods
	[DllImport ("__Internal")]
	private static extern void _disableReturnAd();
	
	[DllImport ("__Internal")]
	private static extern void _enterBackground();
	
	[DllImport ("__Internal")]
	private static extern void _enterForeground();
	
	
	// Orientation method
	[DllImport ("__Internal")]
	private static extern void _setUnitySupportedOrientations(int supportedOrientations);
	
	
	// Orientation method
	[DllImport ("__Internal")]
	private static extern void _setUnityAutoRotation(int autoRotation);
	
	// Orientation method
	[DllImport ("__Internal")]
	private static extern void _setUnityVersion(string unityVersion);
	
	// Ad methods
	[DllImport ("__Internal")]
	private static extern void _loadAd(string adType, string objectDelegate);
	
	[DllImport ("__Internal")]
	private static extern void _loadRewardedVideoAd(string objectDelegate);
	
	[DllImport ("__Internal")]
	private static extern void _showAd();
	
	[DllImport ("__Internal")]
	private static extern bool _isAdReady();
	
	// Banner methods
	[DllImport ("__Internal")]
	private static extern void _addBanner(string bannerType,string bannerPosition,string bannerSize,string objectDelegate);
	
	[DllImport ("__Internal")]
	private static extern void _addBannerAtFixedOrigin(string bannerType,int x,int y,string bannerSize,string objectDelegate);
	
	[DllImport ("__Internal")]
	private static extern void _setBannerPosition(string bannerPosition);
	
	[DllImport ("__Internal")]
	private static extern void _setBannerFixedPosition(int x,int y);
	
	[DllImport ("__Internal")]
	private static extern void _setBannerSize(string bannerSize);
	
	[DllImport ("__Internal")]
	private static extern void _hideBanner();
	
	[DllImport ("__Internal")]
	private static extern void _showBanner();
	
	[DllImport ("__Internal")]
	private static extern bool _bannerIsVisible();
	
	[DllImport ("__Internal")]
	private static extern void _didRotateFromInterfaceOrientation(int orientation);
	
	//Other methods
	[DllImport ("__Internal")]
	private static extern bool _STAShouldAutoRotate();
	
	
	
	
	//Splash Ad methods
	public static void showSplashAd()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_showSplashAd("","","","","","","","",Screen.width/4,Screen.height/3,false);
		}
	}
	public static void showSplashAd(STASplashPreferences splashPref)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			if (splashPref.delegateName == null) {
				splashPref.delegateName = "";
			}
			if(splashPref.loadingIndicatorCenterPoint.x == 0 && splashPref.loadingIndicatorCenterPoint.y == 0){
				splashPref.loadingIndicatorCenterPoint.x = Screen.width/4;
				splashPref.loadingIndicatorCenterPoint.y = Screen.height/3;
			}
			_showSplashAd(getStringFromSTASplashMode(splashPref.mode),getStringFromSTASplashMinTime(splashPref.minTime),getStringFromSTASplashAdDisplayTime(splashPref.adDisplayTime),getStringFromSTASplashTemplateTheme(splashPref.templateTheme),getStringFromSTASplashLoadingIndicatorType(splashPref.loadingIndicatorType),splashPref.delegateName,splashPref.templateIconImageName,splashPref.templateAppName,splashPref.loadingIndicatorCenterPoint.x,splashPref.loadingIndicatorCenterPoint.y,splashPref.isLandscape);
		}
	}
	
	//Return ad methods
	public static void disableReturnAd()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_disableReturnAd();
		}
	}
	
	
	//Ad methods
	public static void loadAd(STAInterstitialProperties adProp)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			if (adProp.delegateName == null) {
				adProp.delegateName = "";
			}
			_loadAd(getStringFromAdType(adProp.type),adProp.delegateName);
		}
	}
	public static void loadAd(AdType adType,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd(getStringFromAdType(adType),objectDelegate);
		}
	}
	public static void loadAd(AdType adType)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd(getStringFromAdType(adType),"");
		}
	}
	public static void loadAd(string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd("STAAdType_Automatic",objectDelegate);
		}
	}
	public static void loadAd()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadAd("STAAdType_Automatic","");
		}
	}
	
	public static void loadRewardedVideoAd(string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_loadRewardedVideoAd(objectDelegate);
		}
	}
	
	
	public static void showAd()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor){
			checkInitilize();
			_showAd();
		}
	}
	
	public static bool isAdReady()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor){
			return _isAdReady();
		}else{
			return false;
		}
	}
	
	//banner methods
	//Full/4 objects
	public static void addBanner(STABannerProperties bannerProperties)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			if (bannerProperties.delegateName == null) {
				bannerProperties.delegateName = "";
			}
			if(bannerProperties.useFixedPosition == true)
			{
				_addBannerAtFixedOrigin("AUTOMATIC",bannerProperties.fixedPosition.x,bannerProperties.fixedPosition.y,getStringFromBannerSize(bannerProperties.size),bannerProperties.delegateName);
			}else {
				_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerProperties.position),getStringFromBannerSize(bannerProperties.size),bannerProperties.delegateName);
			}
		}
	}
	public static void addBanner(BannerType bannerType,BannerPosition bannerPosition,BannerSize bannerSize,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}
	public static void addBanner(BannerType bannerType,int x,int y,BannerSize bannerSize,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}
	
	//3 objects
	public static void addBanner(BannerPosition bannerPosition,BannerSize bannerSize,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}
	public static void addBanner(int x,int y,BannerSize bannerSize,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}
	
	//2 objects
	public static void addBanner(BannerPosition bannerPosition,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),"STA_AutoAdSize",objectDelegate);
		}
	}
	public static void addBanner(int x,int y,string objectDelegate )
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,"STA_AutoAdSize",objectDelegate);
		}
	}
	
	public static void addBanner(BannerSize bannerSize,string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM",getStringFromBannerSize(bannerSize),objectDelegate);
		}
	}
	
	public static void addBanner(BannerPosition bannerPosition,BannerSize bannerSize)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),getStringFromBannerSize(bannerSize),"");
		}
	}
	public static void addBanner(int x,int y,BannerSize bannerSize)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,getStringFromBannerSize(bannerSize),"");
		}
	}
	
	
	
	//1 objects
	public static void addBanner(string objectDelegate)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM","STA_AutoAdSize",objectDelegate);
		}
	}
	public static void addBanner(BannerPosition bannerPosition)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC",getStringFromBannerPosition(bannerPosition),"STA_AutoAdSize","");
		}
	}
	public static void addBanner(int x,int y)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBannerAtFixedOrigin("AUTOMATIC",x,y,"STA_AutoAdSize","");
		}
	}
	public static void addBanner(BannerSize bannerSize)
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM",getStringFromBannerSize(bannerSize),"");
		}
	}
	//0 objects
	public static void addBanner()
	{
		// Call plugin only when running on real device
		if (Application.platform != RuntimePlatform.OSXEditor) {
			checkInitilize();
			_addBanner("AUTOMATIC","BOTTOM","STA_AutoAdSize","");
		}
	}
	
	public static void setBannerPosition(BannerPosition bannerPosition)
	{
		_setBannerPosition(getStringFromBannerPosition(bannerPosition));
	}
	public static void setBannerPosition(int x,int y)
	{
		_setBannerFixedPosition(x,y);
	}
	public static void setBannerSize(BannerSize bannerSize)
	{
		_setBannerSize(getStringFromBannerSize(bannerSize));
	}
	
	
	public static void hideBanner()
	{
		_hideBanner();
	}
	
	public static void showBanner()
	{
		_showBanner();
	}
	
	public static bool bannerIsVisible()
	{
		return _bannerIsVisible();
	}
	
	
	public static void didRotateFromInterfaceOrientation(DeviceOrientation orientation)
	{
		if (_STAShouldAutoRotate()) {
			_didRotateFromInterfaceOrientation(getIntFromDeviceOrientation(orientation));
		}
	}
	
	//others function
	public static bool STAShouldAutoRotate()
	{
		return _STAShouldAutoRotate();
	}
	
	
	private static int unitySupportedOrientations(){
		int ori = 0;
		bool isPort = false;
		bool isLand = false;
		
		if (appOrientation == STAUnityOrientation.STAAutoRotation) {
			if (Screen.autorotateToPortrait == true || Screen.autorotateToPortraitUpsideDown == true) {
				isPort = true;
			}
			
			if (Screen.autorotateToLandscapeRight == true || Screen.autorotateToLandscapeLeft == true) {
				isLand = true;
			}
			if (isPort && !isLand) {
				ori = 0;
			}
			if (!isPort && isLand) {
				ori = 1;
			}
			
			if (isPort && isLand) {
				ori = 2;
			}
			return ori;
		} else {
			if(appOrientation == STAUnityOrientation.STAPortrait ){
				return 0;
			}else{
				return 1;
			}
		}
	}
	
	
	public static void unityOrientation(STAUnityOrientation orientation){
		appOrientation = orientation;
	}
	
	private static int getUnityAutoRotationInt(){
		if (appOrientation == STAUnityOrientation.STAPortrait) {
			return 0;
		} else if (appOrientation == STAUnityOrientation.STALandscape) {
			return 1;
		} else { 
			return 2;
		}
	}
}