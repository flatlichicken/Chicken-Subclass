using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Chickensubclass.Content
{
    public class ChickenSubclassConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("ZenithChickenProjectleQuality")]

        [DefaultValue(3)]
        [Range(1, 3)]
        [Slider]
        public int ZCPQuality;
    }
}

