using UnityEngine;
using UnityEngine.EventSystems;

namespace Helper.Lesson_7
{
	public class VideoOptions : BaseMenu
	{
		private int _selectSettings;
		private VideoSettings _videoSettings;

		public VideoSettings VideoSettings
		{
			get { return _videoSettings ?? (_videoSettings = VideoSettingsRepositoty.VideoSettings); }
		}

		enum VideoOptionsMenuItems
		{
			CurrentName,
			SoftParticles,
			ShadowQuality,
			SaveAndReturn,
			Back
		}

		private void CreateMenu(string[] menuItems)
		{
			_elementsOfInterface = new IControl[menuItems.Length];
			for (var index = 0; index < menuItems.Length; index++)
			{
				switch (index)
				{
					case (int)VideoOptionsMenuItems.CurrentName:
						{
						
							break;
						}
					case (int)VideoOptionsMenuItems.SoftParticles:
						{
							
							break;
						}
					case (int)VideoOptionsMenuItems.ShadowQuality:
						{
							
							break;
						}
					case (int)VideoOptionsMenuItems.SaveAndReturn:
						{
							var tempControl =
								CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("VideoOptionsMenuItems", "SaveAndReturn"));

							tempControl.GetControl.onClick.AddListener(SaveAndReturn);
							_elementsOfInterface[index] = tempControl;
							break;
						}
					case (int)VideoOptionsMenuItems.Back:
						{
							var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("VideoOptionsMenuItems", "Back"));

							tempControl.GetControl.onClick.AddListener(Back);
							_elementsOfInterface[index] = tempControl;
							break;
						}
				}
			}
			if (_elementsOfInterface.Length < 0) return;

			_elementsOfInterface[0].Control.Select();
			_elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
		}

		/// <summary>
		///Задаем значение по умолчанию элементам интерфейса
		/// </summary>
		/// <param name="arg"></param>
		private void Call(int arg)
		{
			_selectSettings = arg;
			foreach (var control in _elementsOfInterface)
			{
				if (control == null) continue;
				if (control.Instance.name == VideoOptionsMenuItems.CurrentName.ToString() && control is DropdownUI)
				{
					var temp = control as DropdownUI;
					temp.GetControl.value = arg;
					temp.GetControl.RefreshShownValue();
				}
				else if (control.Instance.name == VideoOptionsMenuItems.SoftParticles.ToString() && control is ToggleUI)
				{
					(control as ToggleUI).GetControl.isOn = VideoSettings.Items[arg].SoftParticles;
				}
				else if (control.Instance.name == VideoOptionsMenuItems.ShadowQuality.ToString() && control is DropdownUI)
				{
					var temp = control as DropdownUI;
					temp.GetControl.value = (int)VideoSettings.Items[arg].ShadowQuality;
					temp.GetControl.RefreshShownValue();
				}
			}
		}

		private void SaveAndReturn()
		{
			ApplySettings();
			VideoSettingsRepositoty.VideoSettings = _videoSettings;
			VideoSettingsRepositoty.SaveData();
			Hide();
			Interface.Execute(InterfaceObject.OptionsMenu);
		}

		private void Back()
		{
			Hide();
			Interface.Execute(InterfaceObject.OptionsMenu);
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
			var tempMenuItems = System.Enum.GetNames(typeof(VideoOptionsMenuItems));
			CreateMenu(tempMenuItems);
			Call(QualitySettings.GetQualityLevel());
			IsShow = true;
		}

		private void ApplySettings()
		{
			Save();
			QualitySettings.softParticles = VideoSettings.Items[_selectSettings].SoftParticles;
			QualitySettings.shadows = VideoSettings.Items[_selectSettings].ShadowQuality;
			QualitySettings.SetQualityLevel(_selectSettings, true);
		}

		/// <summary>
		/// Заносим значения с элементов UI в нашу структуру данных
		/// </summary>
		private void Save()
		{
			foreach (var control in _elementsOfInterface)
			{
				if (control == null) continue;
				var videoSettings = VideoSettings;
				var videoSettingsItems = videoSettings.Items[_selectSettings];
				if (control.Instance.name == VideoOptionsMenuItems.SoftParticles.ToString() && control is ToggleUI)
				{
					videoSettingsItems.SoftParticles = (control as ToggleUI).GetControl.isOn;
				}
				else if (control.Instance.name == VideoOptionsMenuItems.ShadowQuality.ToString() && control is DropdownUI)
				{
					videoSettingsItems.ShadowQuality = (ShadowQuality)(control as DropdownUI).GetControl.value;
				}
				else if (control.Instance.name == VideoOptionsMenuItems.CurrentName.ToString() && control is DropdownUI)
				{
					videoSettings.CurrentSettings = (control as DropdownUI).GetControl.value;
				}
				videoSettings.Items[_selectSettings] = videoSettingsItems;
				_videoSettings = videoSettings;
			}
		}
	}
}