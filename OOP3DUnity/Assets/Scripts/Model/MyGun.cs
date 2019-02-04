#region 1. Добавить новый тип огнестрельного оружия.
namespace Geekbrains
{
	public sealed class MyGun : Weapon
	{
        public MyGun()
        {
            GetSetMaxCountAmmunition = 4;
            GetSetCountClip = 3;
        }

        public override void Fire()
        {
            if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;

            if (Ammunition as MyBullet)
            {
                MyBullet myBullet = Ammunition as MyBullet;

                int rnd = UnityEngine.Random.Range(0, 4);

                if (rnd == 3)
                {
                    myBullet.DoubleDamage = true; ;
                    //UnityEngine.Debug.Log("DA");
                }
                else
                {
                    myBullet.DoubleDamage = false;
                    //UnityEngine.Debug.Log("NET");
                }             
            }

			if (Ammunition)
			{
				var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);// Pool object
				temAmmunition.AddForce(_barrel.forward * _force);
				Clip.CountAmmunition--;
				_isReady = false;
				Invoke(nameof(ReadyShoot), _rechergeTime);
				//_timer.Start(_rechergeTime);
			}
		}
	}
}
#endregion