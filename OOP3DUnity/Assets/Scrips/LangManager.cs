﻿using System.Xml;
using UnityEngine;

public class LangManager : Singleton<LangManager>
{
	private XmlDocument _root;
	
	private void Awake()
	{
		Init("Language");
	}

	public string LanguageCode { get; private set; }

	public void Init(string file, string languageCode = "")
	{
		_root = new XmlDocument();
		if (languageCode == "")
		{
			LanguageCode = Application.systemLanguage ==
			SystemLanguage.Russian ? "Ru" : "En";
		}
		var config = LoadResource(file);
        Debug.Log(config);
		if (!config) return;
		_root.LoadXml(config.text);
	}

	public string Text(string level, string id)
	{
		if (_root == null)
		{
			return "[not init]";
		}
		string path = "Settings/group[@id='" + level + "']/string[@id='" + id +
		"']/text";
		XmlNode value = _root.SelectSingleNode(path);
		if (value == null)
		{
			return "[not found]";
		}
		return value.InnerText;
	}
	private string LocalizeResourceName(string resourceName)
	{
		return resourceName + LanguageCode;
	}
	private TextAsset LoadResource(string resourceName)
	{
		return Resources.Load(LocalizeResourceName(resourceName),
		typeof(TextAsset)) as TextAsset;
	}
}