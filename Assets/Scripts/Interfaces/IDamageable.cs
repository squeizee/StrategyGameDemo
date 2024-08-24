namespace Interfaces
{
    public interface IDamageable
    {
        // Health is a property of the interface
        int Health { get; set; }
        // IsDead is a property of the interface
        bool IsDead { get;}
        // TakeDamage is a method of the interface
        void TakeDamage(int damage);
        
        
    }
}