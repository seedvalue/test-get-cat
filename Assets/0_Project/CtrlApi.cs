using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class CtrlApi : MonoBehaviour
{
    public static CtrlApi Instance;

    private const string SERVER_URL = "https://pusbkbbia3.execute-api.us-east-1.amazonaws.com/default/";
    private const string API_GET_INFO = "get_cat";

    public Action<Cat> OnCatUpdated;

    private Cat CurrentCat = null;
    public void GetInfo()
    {
        Debug.Log("CtrlApi : GetInfo...");
        StartCoroutine(PostDataCo());
    }

    private void OnRequestSucces(string requestText)
    {
         Debug.Log("CtrlApi : OnRequestSucces : requestText = " + requestText);
         var cat = JsonUtility.FromJson<Cat>(requestText);
         CurrentCat = cat;
         OnCatUpdated?.Invoke(cat);
    }
    
    private void OnRequestError(string requestError)
    {
        Debug.LogError("CtrlApi : OnRequestError : requestError = " + requestError);
    }
    
    IEnumerator PostDataCo()
    {
        string url = SERVER_URL + API_GET_INFO;
        WWWForm form = new WWWForm();
        form.AddField("name", "1");
        //form.SetRequestHeader ("token", "tz677zuiuis2qEZKo3Lt+kHluOGss");
        //using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        string rawData = "{\"name\":\"1\"}";
        using (UnityWebRequest request = UnityWebRequest.Put(url,rawData))
        {
            request.SetRequestHeader("Content-Type","application/json");
            //UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/api/rawCoords", jsonStringTrial);

            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    OnRequestError("ConnectionError : " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    OnRequestError("ProtocolError : " + request.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    OnRequestError("DataProcessingError : " + request.error);
                    break;
                case UnityWebRequest.Result.InProgress: 
                    //this can handle InProgress
                    break;
                case UnityWebRequest.Result.Success:
                    OnRequestSucces(request.downloadHandler.text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }



    private void Awake()
        {
            Instance = this;
        }

    public void SetCatColor(Color color)
    {
        if(CurrentCat==null) return;
        CurrentCat.color = color.ToHexString();
        OnCatUpdated?.Invoke(CurrentCat);
    }
    
    public void SetCatEnabled(bool isEnabled)
    {
        if(CurrentCat==null) return;
        CurrentCat.enable = isEnabled;
        OnCatUpdated?.Invoke(CurrentCat);
    }
}
