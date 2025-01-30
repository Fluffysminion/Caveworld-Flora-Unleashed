using System.Text;
using RimWorld;
using Verse;
using static Caveworld_Flora_Unleashed.Settings;

namespace Caveworld_Flora_Unleashed
{
    public class Mycelium : Plant
	{
		public ThingDef_FruitingBody plantDef = null;

		public int actualSize = 0;

		public int desiredSize = 0;

		public int nextGrowthTick = 0;

		//public int nextReproductionTick = 0;

		public float ExclusivityRadius => plantDef.MyceliumExclusivityRadiusOffset + (float)desiredSize * plantDef.MyceliumExclusivityRadiusFactor;

		public override string LabelMouseover => def.LabelCap;

		public static FruitingBody SpawnNewMyceliumAt(Map map, IntVec3 spawnCell, ThingDef_FruitingBody plantDef, int desiredSize)
		{
			FruitingBody newPlant = ThingMaker.MakeThing(plantDef) as FruitingBody;
			GenSpawn.Spawn(newPlant, spawnCell, map);
			Mycelium newMycelium = ThingMaker.MakeThing(Caveworld_Flora_Unleashed_DefOf.BMT_Mycelium) as Mycelium;
			newMycelium.Initialize(plantDef, desiredSize);
			GenSpawn.Spawn(newMycelium, spawnCell, map);
			newPlant.Mycelium = newMycelium;
			return newPlant;
		}

		public void Initialize(ThingDef_FruitingBody plantDef, int desiredSize)
		{
			Growth = 1f;
			this.plantDef = plantDef;
			actualSize = 1;
			this.desiredSize = desiredSize;
		}

		public static float GetExclusivityRadius(ThingDef_FruitingBody plantDef, int MyceliumSize)
		{
			return plantDef.MyceliumExclusivityRadiusOffset + (float)MyceliumSize * plantDef.MyceliumExclusivityRadiusFactor;
		}

		public static float GetMaxExclusivityRadius(ThingDef_FruitingBody plantDef)
		{
			return plantDef.MyceliumExclusivityRadiusOffset + (float)plantDef.MyceliumSizeRange.max * plantDef.MyceliumExclusivityRadiusFactor;
		}


		public override void TickLong()
		{
			if (Find.TickManager.TicksGame > nextGrowthTick && FruitingBody.IsTemperatureConditionOkAt(plantDef, base.Map, base.Position) && FruitingBody.IsLightConditionOkAt(plantDef, base.Map, base.Position))
			{
				if (actualSize >= desiredSize)
				{
                    GenCaveFungusReproduction.TrySpawnNewMyceliumAwayFrom(this);
                }
				else
				{
                    GenCaveFungusReproduction.TryGrowMycelium(this);
                }
				nextGrowthTick = Find.TickManager.TicksGame + (int)(plantDef.plant.lifespanDaysPerGrowDays * 60000f * 10f / spawnRate);
			}
		}

		public override void ExposeData()
		{
			base.ExposeData();
			/*
			if (Scribe.mode == LoadSaveMode.Saving)
			{
				plantDefAsString = plantDef.defName;
				Scribe_Values.Look(ref plantDefAsString, "plantDefAsString");
			}
			else if (Scribe.mode == LoadSaveMode.LoadingVars)
			{
				Scribe_Values.Look(ref plantDefAsString, "plantDefAsString");
				plantDef = ThingDef.Named(plantDefAsString) as ThingDef_FruitingBody;
			}
			*/
			Scribe_Defs.Look(ref plantDef, "plantDef");
			if (plantDef == null && Scribe.mode == LoadSaveMode.LoadingVars)        //compat with old saves
            {
                string plantDefAsString = "";
                Scribe_Values.Look(ref plantDefAsString, "plantDefAsString");
                plantDef = ThingDef.Named(plantDefAsString) as ThingDef_FruitingBody;
            }
			Scribe_Values.Look(ref actualSize, "actualSize", 0);
			Scribe_Values.Look(ref desiredSize, "desiredSize", 0);
			Scribe_Values.Look(ref nextGrowthTick, "nextGrownTick", 0);
		}

		public void NotifyPlantAdded()
		{
			actualSize++;
		}

		public void NotifyPlantRemoved()
		{
			actualSize--;
			if (actualSize <= 0)
			{
				Destroy();
			}
		}

		public override string GetInspectString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(plantDef.LabelCap);
			return stringBuilder.ToString();
		}
	}
}