﻿using UnityEngine;

namespace Geekbrains
{
	public class UiInterface
	{
		private FlashLightUiText _flashLightUiText;

		public FlashLightUiText LightUiText
		{
			get
			{
				if (!_flashLightUiText)
					_flashLightUiText = MonoBehaviour.FindObjectOfType<FlashLightUiText>();
				return _flashLightUiText;
			}
		}

		private static WeaponUiText _weaponUiText; // static

        public static WeaponUiText WeaponUiText // static
        {
			get
			{
				if (!_weaponUiText)
					_weaponUiText = MonoBehaviour.FindObjectOfType<WeaponUiText>();
				return _weaponUiText;
			}
		}
	}
}