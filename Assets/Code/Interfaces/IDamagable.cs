public interface IDamagable
{
	public bool IsAlive { get; }
	public bool IsKilled { get; set; }
	public float Health { get; set; }
	public float MaxHealth { get; }
	public void TakeDamage(float damage);
	public void Kill();
}
