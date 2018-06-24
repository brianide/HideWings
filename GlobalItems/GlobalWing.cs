using WinglessFlight.Items;
using Terraria;
using Terraria.ModLoader;

namespace WinglessFlight.GlobalItems
{
    class GlobalWing : GlobalItem
    {
        // Prevents sound/visual effects of hidden wings from being rendered,
        // and determines if the smoker is in an accessory slot
        public override bool WingUpdate(int wings, Player player, bool inUse)
        {
            // Tracks which slot, if any, the smoker is equipped to
            int smokerSlot = -1;

            // Tracks whether we've found a pair of visible wings among the
            // player's equipment
            bool visibleWings = false;

            // Iterate over accessory slots
            for (int i = Constants.AccessoryOffset; i < Constants.AccessoryOffset + Constants.AccessoryCount + player.extraAccessorySlots; i++)
            {
                if(!player.hideVisual[i])
                {
                    if (smokerSlot < 0 && player.armor[i].type == mod.ItemType<Smoker>())
                    {
                        smokerSlot = i - Constants.AccessoryOffset;
                    }

                    visibleWings |= player.armor[i].wingSlot > 0;
                }
            }

            // Iterate over social accessory slots
            for (int i = Constants.SocialOffset; i < Constants.SocialOffset + Constants.AccessoryCount + player.extraAccessorySlots; i++)
            {
                if (smokerSlot < 0 && player.armor[i].type == mod.ItemType<Smoker>())
                {
                    smokerSlot = i - Constants.SocialOffset;
                }

                visibleWings |= player.armor[i].wingSlot > 0;
            }

            player.GetModPlayer<WinglessFlightPlayer>().SmokerSlot = smokerSlot;

            // If the player's wings aren't visible, cancel their effects
            if (!visibleWings)
            {
                player.wings = -1;
                return true;
            }

            // Otherwise, continue as normal
            return false;
        }
    }
}
