using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Chickensubclass
{
    public sealed class Blacklist : ModPlayer
    {
        private readonly ulong[] BlacklistedSteamIDs = new ulong[]
        {
            76561198010743466, // ozzatron
            76561198107195250, // fabsol
			
        };

        public sealed override async void OnEnterWorld()
        {
            ulong currentSteamID = Steamworks.SteamUser.GetSteamID().m_SteamID;

            if (System.Array.Exists(BlacklistedSteamIDs, id => id == currentSteamID))
            {
				
                Netplay.Disconnect = true;

                string reason;

                if (currentSteamID == 76561198010743466 || currentSteamID == 76561198107195250)
                {
                    reason = "Grooming (allegedly)";
                }
                else
                {
                    reason = "(No reason provided)";
                }

				WorldGen.SaveAndQuit();
		
				while (Main.menuMode != 0)
                {
                    await Task.Delay(100);
                }

                Main.statusText = $"You have Been Blacklisted From Chicken Subclass.\nReason: {reason}";
			

                Main.menuMode = 14; 
            }
        }
    }
}

