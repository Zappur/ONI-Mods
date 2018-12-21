﻿using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace UniversalHeater
{
    public class UniversalHeaterConfig : IBuildingConfig
    {
        public const string ID = "UNIVERSALHEATER";
        private static readonly LogicPorts.Port OUTPUT_PORT = LogicPorts.Port.OutputPort(FilteredStorage.FULL_PORT_ID, new CellOffset(0, 1), STRINGS.BUILDINGS.PREFABS.REFRIGERATOR.LOGIC_PORT_DESC, false);

        public override BuildingDef CreateBuildingDef()
        {
            string id = "UNIVERSALHEATER";
            int width = 1;
            int height = 2;
            string anim = "fridge_kanim";
            int hitpoints = 30;
            float construction_time = 10f;
            float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
            string[] raw_MINERALS = MATERIALS.ANY_BUILDABLE;
            float melting_point = 800f;
            BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
            EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tier, raw_MINERALS, melting_point, build_location_rule, TUNING.BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 2000f;
            buildingDef.ExhaustKilowattsWhenActive = 1f;
            buildingDef.PermittedRotations = PermittedRotations.R360;
            buildingDef.Floodable = false;
            buildingDef.ViewMode = OverlayModes.Power.ID;
            buildingDef.AudioCategory = "Metal";
            SoundEventVolumeCache.instance.AddVolume("fridge_kanim", "Refrigerator_open", NOISE_POLLUTION.NOISY.TIER1);
            SoundEventVolumeCache.instance.AddVolume("fridge_kanim", "Refrigerator_close", NOISE_POLLUTION.NOISY.TIER1);
            return buildingDef;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, UniversalHeaterConfig.OUTPUT_PORT);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, UniversalHeaterConfig.OUTPUT_PORT);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            List<Tag> mod = new List<Tag>();mod.AddRange((IEnumerable<Tag>)STORAGEFILTERS.NOT_EDIBLE_SOLIDS); mod.AddRange((IEnumerable<Tag>)STORAGEFILTERS.FOOD);

            GeneratedBuildings.RegisterLogicPorts(go, UniversalHeaterConfig.OUTPUT_PORT);
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = mod;
            storage.allowItemRemoval = true;
            storage.capacityKg = 1000000f;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            Prioritizable.AddRef(go);
            go.AddOrGet<TreeFilterable>();
            go.AddOrGet<Refrigerator>();
            go.AddOrGet<Refrigerator>().simulatedInternalTemperature = 323.15f;
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGetDef<StorageController.Def>();
        }
    }

}

