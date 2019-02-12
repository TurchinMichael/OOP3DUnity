namespace Geekbrains
{
    public class Bomb : BaseObjectScene, IBomb, IDestroyable
    {
        public float powerExplosion, radiusExplosion, upforce, damage;
        InfoCollision _infoCollision;


        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.transform.tag == "Player" || collision.transform.tag == "Enemy")
            {
                _infoCollision = new InfoCollision(damage, collision.contacts[0], collision.transform, Rigidbody.velocity);
                Invoke(nameof(Explosion), 1);// Explosion();
                UnityEngine.Debug.Log("EXPL");
            }
        }

        public void Explosion()
        {
            UnityEngine.Vector3 explPos = transform.position;
            UnityEngine.Collider[] colliders = UnityEngine.Physics.OverlapSphere(explPos, radiusExplosion);

            foreach(UnityEngine.Collider hit in colliders)
            {
                UnityEngine.Rigidbody _rb = hit.GetComponent<UnityEngine.Rigidbody>();
                Bot _bot = hit.transform.GetComponent<Bot>();

                if (_rb != null && _bot != null)
                {
                    _rb.AddExplosionForce(powerExplosion, explPos, radiusExplosion, upforce, UnityEngine.ForceMode.Impulse);
                    _bot.SetDamage(_infoCollision);
                    UnityEngine.Debug.Log("Explosion");
                    Destroy();
                }
            }
        }

        public void Destroy()
        {
            Destroy(gameObject, 1f);
        }
    }
}