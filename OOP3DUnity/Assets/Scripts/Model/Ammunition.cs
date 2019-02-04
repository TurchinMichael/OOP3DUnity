﻿using UnityEngine;

namespace Geekbrains
{
	public abstract class Ammunition : BaseObjectScene
	{
		[SerializeField] protected float _timeToDestruct = 10;
		[SerializeField] protected float _baseDamage = 10;
		protected float _curDamage;
		protected float _lossOfDamageAtTime = 0.2f;

		public AmmunitionType Type { get; } = AmmunitionType.Bullet;

		protected override void Awake()
		{
			base.Awake();
			_curDamage = _baseDamage;
		}

		private void Start()
		{
			Destroy(gameObject, _timeToDestruct);
			InvokeRepeating(nameof(LossOfDamage), 0, 1);
		}

		public void AddForce(Vector3 dir)
		{
			if (!Rigidbody) return;
			Rigidbody.AddForce(dir);
		}


        //#region 5. Добавить изменения текущего урона, который может нанести пуля.
        //protected void SetLossOfDamage(float damage)
        //{
        //    _curDamage -= damage;
        //}
        //#endregion

        protected void LossOfDamage()
		{
			_curDamage -= _lossOfDamageAtTime;
		}
	}
}