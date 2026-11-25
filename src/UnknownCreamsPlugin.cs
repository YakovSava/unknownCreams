using System.Collections.Generic;
using BepInEx;
using CUCoreLib.Data;
using CUCoreLib.Registries;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnknownCreams
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(CoreLibGuid)]
    public sealed class UnknownCreamsPlugin : BaseUnityPlugin
    {
        public const string PluginGuid = "YakovSava.UnknownCreams";
        public const string PluginName = "Unknown Creams";
        public const string PluginVersion = "1.0.0";
        public const string CoreLibGuid = "net.cucorelib";

        private const string CreamIconItemId = "paincream";
        private const float CapacityMl = 50f;
        private const float DoseMl = 10f;
        private const float IconRetrySeconds = 0.5f;

        private static Sprite creamIcon;
        private bool liquidsRegistered;
        private bool itemsRegistered;
        private bool recipesRegistered;
        private float nextIconAttempt;

        private void Awake()
        {
            new Harmony(PluginGuid).PatchAll(typeof(UnknownCreamsPlugin).Assembly);
            SceneManager.activeSceneChanged += OnSceneChanged;
            TryRegister();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private void Update()
        {
            if (itemsRegistered || Time.unscaledTime < nextIconAttempt)
            {
                return;
            }

            nextIconAttempt = Time.unscaledTime + IconRetrySeconds;
            TryRegister();
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            SkinDye.Clear();
        }

        private void TryRegister()
        {
            if (!liquidsRegistered)
            {
                RegisterLiquids();
                liquidsRegistered = true;
            }

            if (!itemsRegistered)
            {
                if (!TryGetCreamIcon(out creamIcon))
                {
                    return;
                }

                RegisterItems(creamIcon);
                itemsRegistered = true;
            }

            if (!recipesRegistered)
            {
                RegisterRecipes();
                recipesRegistered = true;
                Logger.LogInfo("Registered " + CreamCatalog.All.Count + " creams, their liquids and the Nanozin-Plus recipe.");
            }
        }

        private static bool TryGetCreamIcon(out Sprite icon)
        {
            if (creamIcon != null)
            {
                icon = creamIcon;
                return true;
            }

            return ItemRegistry.TryGetIcon(CreamIconItemId, out icon);
        }

        private static void RegisterLiquids()
        {
            foreach (CreamDefinition def in CreamCatalog.All)
            {
                CustomLiquidInfo liquid = new CustomLiquidInfo
                {
                    name = def.Name,
                    description = def.Description,
                    color = def.Color,
                    valuePerLiter = def.Value * 5f,
                    healthUsable = true,
                    injectable = false,
                    injectionSickness = 0.75f,
                    qualities = BuildQualities(def)
                };

                liquid.onApplyToLimb = delegate(float ml, Limb limb)
                {
                    CreamEffects.Apply(def, ml, limb);
                };

                if (def.Drink != DrinkEffect.None)
                {
                    liquid.onDrink = delegate(float ml, Body body)
                    {
                        CreamEffects.Drink(def, ml, body);
                    };
                }

                LiquidRegistry.Register(def.Id, liquid);
            }
        }

        private static void RegisterItems(Sprite icon)
        {
            foreach (CreamDefinition def in CreamCatalog.All)
            {
                CustomItemInfo info = new CustomItemInfo
                {
                    fullName = def.Name,
                    description = def.Description,
                    category = "medical",
                    slotRotation = -45f,
                    usable = false,
                    usableOnLimb = true,
                    destroyAtZeroCondition = false,
                    combineable = true,
                    weight = 0.5f,
                    scaleWeightWithCondition = true,
                    capacity = CapacityMl,
                    autoFill = false,
                    defaultContents = new List<LiquidStack>
                    {
                        new LiquidStack(def.Id, CapacityMl)
                    },
                    value = def.Value,
                    tags = "medicine",
                    rec = new Recognition(def.Recognition),
                    SpawnFrequency = def.SpawnFrequency,
                    Icon = icon
                };

                info.useLimbAction = delegate(Limb limb, Item item)
                {
                    WaterContainerItem container = item.GetComponent<WaterContainerItem>();
                    if (container != null)
                    {
                        container.ApplyToLimb(limb, DoseMl);
                    }
                };

                ItemRegistry.Register(def.Id, info, icon);
            }
        }

        private static void RegisterRecipes()
        {
            Recipe recipe = new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Medicine,
                result = new RecipeResult
                {
                    id = "nanozinplus",
                    isLiquid = false,
                    amount = 1,
                    resultCondition = 1f,
                    dontDrainResultLiquid = true
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.3f) { specificId = "nanozin" },
                    new RecipeItem(0.3f) { specificId = "vaseline" },
                    new RecipeItem(0.2f) { specificId = "musharm" },
                    new RecipeItem(0.3f) { specificId = "paincream" },
                    new RecipeItem(10f) { specificId = "biochem", isLiquid = true }
                }
            };

            RecipeRegistry.Register(recipe);
        }

        private static List<CraftingQuality> BuildQualities(CreamDefinition def)
        {
            if (def.DisinfectQuality <= 0f)
            {
                return new List<CraftingQuality>();
            }

            return new List<CraftingQuality>
            {
                new CraftingQuality("disinfectant", def.DisinfectQuality)
            };
        }
    }
}
