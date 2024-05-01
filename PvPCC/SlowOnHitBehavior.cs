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
            var existingCallback = playerEntity.WatchedAttributes.GetLong("slowonhitcallback", -1L);
            if (existingCallback != -1L)
            {
                playerEntity.World.UnregisterCallback(existingCallback);
            }

            playerEntity.Stats.Set("walkspeed", "slowonhit", -(PercentSpeedLossOnHit / 100.0f));
            var callbackId =
                entity.World.RegisterCallback((_) => { playerEntity.Stats.Remove("walkspeed", "slowonhit"); },
                    SlowDurationMilliseconds);

            playerEntity.WatchedAttributes.SetLong("slowonhitcallback", callbackId);
        }


        base.OnEntityReceiveDamage(damageSource, ref damage);
    }

    public override string PropertyName()
    {
        return "SlowOnHitBehavior";
    }
}