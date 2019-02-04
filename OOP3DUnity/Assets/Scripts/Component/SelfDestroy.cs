using UnityEngine;

namespace Geekbrains
{
    public class SelfDestroy : BaseObjectScene
    {
        public void SelfDestroyTime(float time) => MonoBehaviour.Destroy(this.gameObject, time);
    }
}
