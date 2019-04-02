using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityStandardAssets.Characters.FirstPerson;

namespace Geekbrains
{
	public class MenuPause : BaseMenu
    {
        //public Main Main;
        public AudioMixer MainAudioMixer;
        public Text text;
        private bool _isPause;
        private AudioMixerSnapshot _snapshotPause;
        private AudioMixerSnapshot _snapshotUnPause;
        private float startBackgroundPitch;
        //private FirstPersonController _firstPersonController;

        //private FirstPersonController FirstPersonController
        //{
        //	get
        //	{
        //		return _firstPersonController ?? (_firstPersonController = FindObjectOfType<FirstPersonController>());
        //	}
        //}
        //public MenuPause menuPause;

        private void Start()
        {
            //Interface.InterfaceResources.MainPanel.gameObject.SetActive(false);
            MainAudioMixer.GetFloat("BackgroundPitch", out startBackgroundPitch);
            _snapshotPause = MainAudioMixer.FindSnapshot("Paused");
            _snapshotUnPause = MainAudioMixer.FindSnapshot("UnPaused");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPause = !_isPause;
                Pause();
            }
            //menuPause.Show();
        }

        private void Pause()
        {
            if (_isPause)
            {
                Time.timeScale = 0;
                Main.Instance.PlayerController.Off();
                //Main.Instance.
                Debug.Log(Time.timeScale);
                MainAudioMixer.SetFloat("BackgroundPitch", 2);
                //FirstPersonController.enabled = false;
                _snapshotPause.TransitionTo(0.001f);
                text.text = "Pause";
                //Text.gameObject.SetActive(true);

                // Interface.Execute(InterfaceObject.MainMenu);
            }
            else
            {
                Main.Instance.PlayerController.On();
                Time.timeScale = 1;
                Debug.Log(Time.timeScale);
                MainAudioMixer.SetFloat("BackgroundPitch", startBackgroundPitch);
                //FirstPersonController.enabled = true;
                _snapshotUnPause.TransitionTo(0.001f);

                text.text = string.Empty;
                //Text.gameObject.SetActive(false);

                // Interface.Execute(InterfaceObject.MainMenu);
            }
        }
        //      private AudioMixerSnapshot _paused;

        //private AudioMixerSnapshot Paused
        //{
        //	get
        //	{
        //		return _paused ?? (_paused = Resources.Load<AudioMixer>("MainAudioMixer").FindSnapshot("Paused"));
        //	}
        //}
        //private AudioMixerSnapshot _unPaused;

        //private AudioMixerSnapshot UnPaused
        //{
        //	get
        //          {
        //              MainAudioMixer.SetFloat("BackgroundPitch", startBackgroundPitch);
        //          }
        //}

        enum MenuPauseItems
		{
			Resume,
			Save,
			MainMenu,
			Quit
		}

		public override void Hide()
		{
			//if (!IsShow) return;
			////UnPaused.TransitionTo(0.001f);
			//Clear(_elementsOfInterface);
			//Interface.InterfaceResources.MainPanel.gameObject.SetActive(false);
			//Time.timeScale = 1;
			////FirstPersonController.enabled = true;
			//Cursor.lockState = CursorLockMode.Locked;
			//Cursor.visible = false;
			//IsShow = false;
		}

		public override void Show()
		{
			//if (IsShow) return;
			////Paused.TransitionTo(0.001f);
			//var tempMenuItems = System.Enum.GetNames(typeof(MenuPauseItems));
			//Interface.InterfaceResources.MainPanel.gameObject.SetActive(true);
			//Time.timeScale = 0;
			////CreateMenu(tempMenuItems);
			////FirstPersonController.enabled = false;
			//Cursor.lockState = CursorLockMode.None;
			//Cursor.visible = true;
			//IsShow = true;
		}

		//private void CreateMenu(string[] menuItems)
		//{
		//	_elementsOfInterface = new IControl[menuItems.Length];
		//	for (var index = 0; index < menuItems.Length; index++)
		//	{
		//		switch (index)
		//		{
		//			case (int)MenuPauseItems.Resume:
		//				{
		//					var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MenuPause", "Resume"));
							
		//					tempControl.GetControl.onClick.AddListener(Hide);
		//					_elementsOfInterface[index] = tempControl;
		//					break;
		//				}
		//			case (int)MenuPauseItems.Save:
		//				{
		//					var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MenuPause", "Save"));

		//					tempControl.GetControl.onClick.AddListener(SaveGame);
		//					tempControl.SetInteractable(false);
		//					_elementsOfInterface[index] = tempControl;
		//					break;
		//				}
		//			case (int)MenuPauseItems.MainMenu:
		//				{
		//					var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MenuPause", "MainMenu"));

		//					tempControl.GetControl.onClick.AddListener(LoadNewGame);
		//					_elementsOfInterface[index] = tempControl;
		//					break;
		//				}
		//			case (int)MenuPauseItems.Quit:
		//				{
		//					var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MenuPause", "Quit"));

		//					tempControl.GetControl.onClick.AddListener(Interface.QuitGame);
		//					_elementsOfInterface[index] = tempControl;
		//					break;
		//				}
		//		}
		//	}
		//	if (_elementsOfInterface.Length < 0) return;
		//	_elementsOfInterface[0].Control.Select();
		//	_elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
		//}

		//private void LoadNewGame()
		//{
		//	Hide();
		//	if (Main.Instance.Scenes.MainMenu == null)
		//	{

		//		Interface.LoadSceneAsync(0);
		//	}
		//	else
		//	{
		//		Interface.LoadSceneAsync(Main.Instance.Scenes.MainMenu);
		//	}
		//}

		//private void SaveGame()
		//{
		//	//Реализовать
		//}
	}

}