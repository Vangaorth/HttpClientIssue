using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.HttpClient;
using System;
using UnityEngine.UI;

public class HttpClientExample : MonoBehaviour
{
    public Text Text;

    bool firstRunDone;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) =>
            {
                return true;
            };
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!firstRunDone)
        {
            firstRunDone = true;

            var httpClient = new HttpClient();
            httpClient.GetString(new Uri("https://api.github.com/users/mralexgray/repos"), (r) =>
            {
                if (r.IsSuccessStatusCode)
                {
                    Text.color = Color.green;
                    Text.text = "Request sent, response received.\n\nEverything's good.";
                }
                else
                {
                    Text.color = Color.red;

                    var exception = r != null ? r.Exception : null;
                    var exceptionMessage = exception != null ? exception.ToString() : "No exception to report";

                    Text.text = "FAILED.\n\n" + exceptionMessage;

                    if (exception != null)
                    {
                        Debug.LogException(exception);
                    }
                }
            });
        }
    }
}
