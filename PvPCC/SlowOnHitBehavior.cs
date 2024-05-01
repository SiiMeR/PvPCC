using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace PvPCC;

public class SlowOnHitBehavior : EntityBehavior
{
    private const int SlowDurationMilliseconds = 1000;
    private const float PercentSpeedLossOnHit = 40.0f; // flat
    public SlowOnHitBehavior(Entity entity) : base(entity)
    {
    }

    public override void OnEntityReceiveDamage(DamageSource damageSource, ref float damage)
    {
        if (entity is EntityPlayer playerEntity && damageSource.Source == EnumDamageSource.Player)
        {
            playerEntity.Stats.Set("walkspeed", "slowonhit", -(PercentSpeedLossOnHit/100.0f));
            entity.World.RegisterCallback((_) =>
            {
                playerEntity.Stats.Remove("walkspeed", "slowonhit");
            }, SlowDurationMilliseconds);
        }


        base.OnEntityReceiveDamage(damageSource, ref damage);
    }

    public override string PropertyName()
    {
        return "SlowOnHitBehavior";
    }
}