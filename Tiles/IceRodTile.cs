using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;//TileObjectData
using Terraria.DataStructures;//PlacementHook
using Terraria.Enums;//AnchorType

namespace VipixToolBox.Tiles
{
	public class IceRodTile : GlobalTile
	{
		private FlyingBlockTE myTE;

		public override void SetDefaults()
		{
			TileObjectData.GetTileData(TileID.Dirt, 0, 0).HookCheck = new PlacementHook(mod.GetTileEntity("FlyingBlockTE").Hook_AfterPlacement, -1, 0, false);
			//TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<FlyingBlockTE>().Hook_AfterPlacement, -1, 0, false);
			//TileObjectData.addTile(Type);
		}
	}
}
