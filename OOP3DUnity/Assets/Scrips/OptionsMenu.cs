using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsMenu : BaseMenu
{
    #region old

    //   enum OptionsMenuItems
    //{
    //	Video,
    //	Sound,
    //	Game,
    //	Back
    //}

    //private void CreateMenu(string[] menuItems)
    //{
    //	_elementsOfInterface = new IControl[menuItems.Length];
    //	for (var index = 0; index < menuItems.Length; index++)
    //	{
    //		switch (index)
    //		{
    //			case (int)OptionsMenuItems.Video:
    //				{
    //					var tempControl =
    //					CreateControl(Interface.InterfaceResources.ButtonPrefab,
    //						LangManager.Instance.Text("OptionsMenuItems", "Video"));
    //					tempControl.GetControl.onClick.AddListener(LoadVideoOptions);
    //					_elementsOfInterface[index] = tempControl;
    //					break;
    //				}
    //			case (int)OptionsMenuItems.Sound:
    //				{
    //					var tempControl =
    //					CreateControl(Interface.InterfaceResources.ButtonPrefab,
    //						LangManager.Instance.Text("OptionsMenuItems", "Sound"));
    //					tempControl.GetControl.onClick.AddListener(LoadSoundOptions);
    //					_elementsOfInterface[index] = tempControl;
    //					break;
    //				}
    //			case (int)OptionsMenuItems.Game:
    //				{
    //					var tempControl =
    //					CreateControl(Interface.InterfaceResources.ButtonPrefab,
    //						LangManager.Instance.Text("OptionsMenuItems", "Game"));
    //					tempControl.GetControl.onClick.AddListener(LoadGameOptions);
    //					_elementsOfInterface[index] = tempControl;
    //					break;
    //				}
    //			case (int)OptionsMenuItems.Back:
    //				{
    //					var tempControl =
    //					CreateControl(Interface.InterfaceResources.ButtonPrefab,
    //						LangManager.Instance.Text("OptionsMenuItems", "Back"));
    //					tempControl.GetControl.onClick.AddListener(Back);
    //					_elementsOfInterface[index] = tempControl;
    //					break;
    //				}
    //		}
    //	}
    //	if (_elementsOfInterface.Length < 0) return;
    //	_elementsOfInterface[0].Control.Select();
    //	_elementsOfInterface[0].Control.OnSelect(new
    //	BaseEventData(EventSystem.current));
    //   }

    #endregion

    #region New

    [SerializeField] private GameObject _mainPanale;

    [SerializeField] private ButtonUi _video;
    [SerializeField] private ButtonUi _sound;
    [SerializeField] private ButtonUi _game;
    [SerializeField] private ButtonUi _back;

    private void Start()
    {
        _video.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Video");
        _video.GetControl.onClick.AddListener(delegate
        {
            LoadVideoOptions(/*Main.Instance.Scenes.Game.SceneAsset.name*/);
        });
        
        _sound.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Sound");
        _sound.GetControl.onClick.AddListener(delegate
        {
            LoadSoundOptions(/*Main.Instance.Scenes.Game.SceneAsset.name*/);
        });

        _game.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Game");
        _game.GetControl.onClick.AddListener(delegate
        {
            LoadGameOptions(/*Main.Instance.Scenes.Game.SceneAsset.name*/);
        });

        _back.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Back");
        _back.GetControl.onClick.AddListener(delegate
        {
            Back(/*Main.Instance.Scenes.Game.SceneAsset.name*/);
        });
        /*
        _continue.GetText.text = LangManager.Instance.Text("MainMenuItems", "Continue");
        _continue.SetInteractable(false);

        _options.GetText.text = LangManager.Instance.Text("MainMenuItems", "Options");
        _options.GetControl.onClick.AddListener(delegate
        {
            Interface.Execute(InterfaceObject.OptionsMenu);
        });
        //_options.SetInteractable(false);

        _quit.GetText.text = LangManager.Instance.Text("MainMenuItems", "Quit");
        _quit.GetControl.onClick.AddListener(delegate
        {
            Interface.QuitGame();
        });
        */
    }

    #endregion

    private void LoadVideoOptions()
	{
		Interface.Execute(InterfaceObject.VideoOptions);		
	}
	private void LoadSoundOptions()
	{
		Interface.Execute(InterfaceObject.AudioOptions);
	}
	private void LoadGameOptions()
	{
		Interface.Execute(InterfaceObject.GameOptions);
	}
	private void Back()
	{
		Interface.Execute(InterfaceObject.MainMenu);
	}
	public override void Hide()
	{
		if (!IsShow) return;
		Clear(_elementsOfInterface);
		IsShow = false;
	}
	public override void Show()
	{
		if (IsShow) return;
        _mainPanale.gameObject.SetActive(true);
  //      var tempMenuItems = System.Enum.GetNames(typeof(OptionsMenuItems));
		//CreateMenu(tempMenuItems);
		IsShow = true;
	}
}
