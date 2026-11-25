using UnityEngine;

namespace UnknownCreams
{
    internal sealed class CreamDefinition
    {
        internal string Id;
        internal string Name;
        internal string Description;
        internal Color Color;
        internal float SkinRate;
        internal float MuscleRate;
        internal float DisinfectSeconds;
        internal float BoneHealSeconds;
        internal float DisinfectQuality;
        internal SkinDyeSpec Dye;
        internal PainEffect Pain;
        internal float PainIncreaseMin;
        internal float PainIncreaseMax;
        internal DrinkEffect Drink;
        internal int SpawnFrequency;
        internal int Value;
        internal int Recognition;
    }

    internal sealed class SkinDyeSpec
    {
        internal Color Color;
        internal float Strength;
    }

    internal enum PainEffect
    {
        None,
        LocalAnesthetic
    }

    internal enum DrinkEffect
    {
        None,
        Aloe
    }
}
