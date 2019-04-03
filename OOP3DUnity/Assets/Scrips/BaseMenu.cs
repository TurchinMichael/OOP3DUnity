using Object = UnityEngine.Object;
using UnityEngine;
using System;

public abstract class BaseMenu : MonoBehaviour
{
	protected IControl[] _elementsOfInterface;
	
	protected bool IsShow { get;   set; }
	protected Interface Interface;
	protected virtual void Awake()
	{
		Interface = FindObjectOfType<Interface>();
	}

	public abstract void Hide();
	public abstract void Show();

	protected void Clear(IControl[] controls)
	{
		foreach (var t in controls)
		{
			if (t == null) continue;
			Destroy(t.Instance);
		}
	}

	protected T CreateControl<T>(T prefab, string text) where T : Object, IControl
	{
		T tempControl = NewMethod(prefab);

		var tempControlText = tempControl as IControlText;
		if (tempControlText != null)
		{
			tempControlText.GetText.text = text;
		}
		return tempControl;
	}

	protected T CreateControl<T>(T prefab, Sprite sprite) where T : Object, IControl
	{
		T tempControl = NewMethod(prefab);

		var tempControlImage = tempControl as IControlImage;
		if (tempControlImage != null)
		{
			tempControlImage.GetImage.sprite = sprite;
		}
		return tempControl;
	}

	private T NewMethod<T>(T prefab) where T : Object, IControl
	{
		if (!prefab) throw new Exception(string.Format("Отсутствует ссылка на {0}", typeof(T)));
		var tempControl = Instantiate(prefab, Interface.InterfaceResources.MainPanel.transform.position, Quaternion.identity,
			Interface.InterfaceResources.MainPanel.transform);
		return tempControl;
	}
}