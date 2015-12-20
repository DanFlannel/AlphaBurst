using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class SignOut : MonoBehaviour {

	void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("AttemptedToSignIn", 0);
        PlayGamesPlatform.Instance.SignOut();
    }
}
