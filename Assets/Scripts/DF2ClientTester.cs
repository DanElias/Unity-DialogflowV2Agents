﻿using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Syrus.Plugins.DFV2Client;
using UnityEngine.UI;

public class DF2ClientTester : MonoBehaviour
{
	public InputField session, content;

	public Text chatbotText;

    // Start is called before the first frame update
    void Start()
    {
		DialogFlowV2Client client = GetComponent<DialogFlowV2Client>();

		client.ChatbotResponded += LogResponseText;
		client.DetectIntentError += LogError;
		client.ReactToContext("DefaultWelcomeIntent-followup", 
			context => Debug.Log("Reacting to welcome followup"));

		Dictionary<string, object> parameters = new Dictionary<string, object>()
		{
			{ "name", "George" }
		};
		client.DetectIntentFromEvent("Welcome", parameters, "test");
    }

	private void LogResponseText(DF2Response response)
	{
		Debug.Log(name + " said: \"" + response.queryResult.fulfillmentText + "\"");
		chatbotText.text = response.queryResult.fulfillmentText;
	}

	private void LogError(long responseCode, string errorMessage)
	{
		Debug.LogError(string.Format("Error {0}: {1}", responseCode.ToString(), errorMessage));
	}

    
	public void SendText()
	{
		GetComponent<DialogFlowV2Client>().DetectIntentFromText(content.text, session.text);
	}


	public void SendEvent()
	{
		GetComponent<DialogFlowV2Client>().DetectIntentFromEvent(content.text,
			new Dictionary<string, object>(), session.text);
	}
}
