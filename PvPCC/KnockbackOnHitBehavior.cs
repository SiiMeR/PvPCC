using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace PvPCC;

// disabled for not working properly for players in air
public class KnockbackOnHitBehavior : EntityBehavior
{
    private static readonly EnumDamageType[] DamageTypesToAddKnockbackFor = {
        EnumDamageType.BluntAttack,
        EnumDamageType.SlashingAttack,
    };

    public KnockbackOnHitBehavior(Entity entity) : base(entity)
    {
    }

    public override void DidAttack(DamageSource source, EntityAgent targetEntity, ref EnumHandling handled)
    {
        var knockbackAmount = 0.5f;
        if (DamageTypesToAddKnockbackFor.Contains(source.Type) && targetEntity != null)
        {
            var knockbackResistance = targetEntity.Properties.KnockbackResistance;
            
            // var weight = targetEntity.Properties.Weight;
            var knockbackStrength = knockbackAmount * (1 - knockbackResistance);
            source.KnockbackStrength = knockbackStrength;

            var direction = (targetEntity.Pos.XYZ - entity.Pos.XYZ).Normalize();
            var knockbackForce = direction * knockbackStrength;

            targetEntity.SidedPos.Motion.Add(knockbackForce);
            // entity.SidedPos.Motion.Add(knockbackResistance * pos.Motion.X * Weight, knockbackResistance * pos.Motion.Y * Weight, knockbackResistance * pos.Motion.Z * Weight);
        }
   
        base.DidAttack(source, targetEntity, ref handled);
    }

    public override string PropertyName()
    {
        return "KnockbackOnHitBehavior";
    }
}