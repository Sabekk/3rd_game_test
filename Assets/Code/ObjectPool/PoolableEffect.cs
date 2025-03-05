using UnityEngine;

namespace ObjectPooling
{
	public class PoolableEffect : MonoBehaviour, IPoolable
	{
        #region PROPERTIES

        public PoolObject Poolable { get; set; }

		#endregion

		#region UNITY_METHODS

		void OnParticleSystemStopped()
		{
			OnEffectFinish();
			ObjectPool.Instance.ReturnToPool(this);
		}

		#endregion

		#region METHODS

		public void AssignPoolable(PoolObject poolable)
		{
			Poolable = poolable;
		}

		protected virtual void OnEffectFinish()
		{

		}
        #endregion
    }
}
