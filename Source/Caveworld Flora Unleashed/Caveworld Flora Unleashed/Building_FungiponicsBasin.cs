using RimWorld;
using Verse;

namespace Caveworld_Flora_Unleashed
{
	public class Building_FungiponicsBasin : Building_PlantGrower
	{
		public override string GetInspectString()
		{
			float temperature = GenTemperature.GetTemperatureForCell(base.Position, base.Map);
			ThingDef_FruitingBody fruitingBodyDef = GetPlantDefToGrow() as ThingDef_FruitingBody;
			if (temperature < (float)fruitingBodyDef.minGrowTemperature)
			{
				return "Caveworld_Flora_Unleashed.CannotGrowTooCold".Translate();
			}
			if (temperature > (float)fruitingBodyDef.maxGrowTemperature)
			{
				return "Caveworld_Flora_Unleashed.CannotGrowTooHot".Translate();
			}
			return "Caveworld_Flora_Unleashed.Growing".Translate();
		}
	}
}
