using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;

namespace PvPCC
{
    public class PvPCCModSystem : ModSystem
    {
        public override void StartServerSide(ICoreServerAPI api)
        {
            api.Event.PlayerNowPlaying += (serverPlayer) =>
            {
                if (serverPlayer.Entity is not null)
                {
                    var entity = serverPlayer.Entity;
                    entity.AddBehavior(new SlowOnHitBehavior(entity));
                    
                }
            };
        }
    }
}