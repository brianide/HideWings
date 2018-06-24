using WinglessFlight.Items;
using Terraria.ModLoader;

namespace WinglessFlight
{
    class WinglessFlightPlayer : ModPlayer
    {
        // TODO: Implement different smoker effects for each slot
        public int SmokerSlot;
        private SmokerEffect smoke = new SpectreSmokerEffect();

        // Cleans up smoker slot determination from previous frame
        public override void PreUpdate()
        {
            SmokerSlot = -1;
        }

        // Renders smoker sounds/visuals
        public override void FrameEffects()
        {
            if (SmokerSlot >= 0)
            {
                smoke.DoEffects(player);
            }
        }

        // Reevaluates whether the player has a visible cape and/or set of
        // wings equipped, and updates their graphics accordingly
        public override void PostUpdate()
        {
            player.back = -1;
            player.front = -1;
            player.wings = -1;

            // Iterate over accessory slots
            for (int i = Constants.AccessoryOffset; i < Constants.AccessoryOffset + Constants.AccessoryCount + player.extraAccessorySlots; i++)
            {
                if(!player.hideVisual[i])
                {
                    ShowItem(i);
                }
            }

            // Iterate over vanity accessory slots
            for (int i = Constants.SocialOffset; i < Constants.SocialOffset + Constants.AccessoryCount + player.extraAccessorySlots; i++)
            {
                ShowItem(i);
            }
        }

        // Sets the player's front/back/wing slots to those of the passed item,
        // if available
        private void ShowItem(int index)
        {
            if (player.armor[index].backSlot > 0 || player.armor[index].frontSlot > 0)
            {
                player.back = player.armor[index].backSlot;
                player.front = player.armor[index].frontSlot;
            }

            if (player.armor[index].wingSlot > 0)
            {
                player.wings = player.armor[index].wingSlot;
            }
        }
    }
}
