using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace PvPCC
{
    public class PvPCCModSystem : ModSystem
    {
        public override void StartServerSide(ICoreServerAPI api)
        {
            api.Event.PlayerDisconnect += (serverPlayer) =>
            {
                serverPlayer.Entity?.WatchedAttributes.RemoveAttribute("slowonhitcallback");
            };
            
            api.Event.PlayerNowPlaying += (serverPlayer) =>
            {
                var entity = serverPlayer.Entity;
                entity?.AddBehavior(new SlowOnHitBehavior(entity));
                entity?.AddBehavior(new KnockbackOnHitBehavior(entity));
            };
        }
    }
}