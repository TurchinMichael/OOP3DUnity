﻿using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public abstract class Weapon : BaseObjectScene
	{
		private int _maxCountAmmunition = 20;
		private int _countClip = 5;

		public Ammunition Ammunition;
		public Clip Clip;

        public int GetSetMaxCountAmmunition
        {
            set => _maxCountAmmunition = value;
            get => _maxCountAmmunition;
        }

        public int GetSetCountClip
        {
            set => _countClip = value;
            get => _countClip;
        }

        protected AmmunitionType[] _ammunitionType = new AmmunitionType[]{AmmunitionType.Bullet};

		[SerializeField] protected Transform _barrel;
		[SerializeField] protected float _force = 999;
		[SerializeField] protected float _rechergeTime = 0.2f;
		private Queue<Clip> _clips = new Queue<Clip>();

		protected bool _isReady = true;
		//protected Timer _timer = new Timer();

		private void Start()
		{
			for (var i = 0; i <= _countClip; i++)
			{
				AddClip(new Clip { CountAmmunition = _maxCountAmmunition });
			}

			ReloadClip();
		}

		public abstract void Fire();

		//protected virtual void Update()
		//{
		//	_timer.Update();
		//	if (_timer.IsEvent())
		//	{
		//		ReadyShoot();
		//	}
		//}

		protected void ReadyShoot()
		{
			_isReady = true;
		}

		protected void AddClip(Clip clip)
		{
			_clips.Enqueue(clip);
		}

		public void ReloadClip()
		{
			if (CountClip <= 0) return;
			Clip = _clips.Dequeue();
		}

		public int CountClip => _clips.Count;
	}
}