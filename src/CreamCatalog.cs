using System.Collections.Generic;
using UnityEngine;

namespace UnknownCreams
{
    internal static class CreamCatalog
    {
        internal static readonly IReadOnlyList<CreamDefinition> All = new List<CreamDefinition>
        {
            new CreamDefinition
            {
                Id = "vaseline",
                Name = "Vaseline",
                Description = "Plain petroleum jelly. Restores skin at a barely noticeable 0.05x rate and does not disinfect. It cannot be used as a disinfectant in crafting.",
                Color = new Color(0.93f, 0.9f, 0.78f),
                SkinRate = 0.05f,
                SpawnFrequency = 2,
                Value = 1,
                Recognition = 2
            },
            new CreamDefinition
            {
                Id = "nanozin",
                Name = "Nanozin",
                Description = "A medical nanogel that rebuilds skin and muscle tissue at 3x the rate of Relief Cream and sterilises the treated limb completely. Fast, clean and faintly humming.",
                Color = new Color(0.2f, 0.9f, 0.85f),
                SkinRate = 3f,
                MuscleRate = 3f,
                DisinfectSeconds = 600f,
                DisinfectQuality = 1f,
                SpawnFrequency = 1,
                Value = 25,
                Recognition = 6
            },
            new CreamDefinition
            {
                Id = "aloeextract",
                Name = "Extract of Aloe",
                Description = "Cool aloe extract. Restores skin at a mere 0.05x rate and does not disinfect. Edible, although the taste is deeply unappreciated.",
                Color = new Color(0.45f, 0.8f, 0.45f),
                SkinRate = 0.05f,
                Drink = DrinkEffect.Aloe,
                SpawnFrequency = 2,
                Value = 2,
                Recognition = 2
            },
            new CreamDefinition
            {
                Id = "honeybalm",
                Name = "Honey Balm",
                Description = "A thick honey salve. Restores skin at 0.3x rate and disinfects a little, but it is too weak to be used as a disinfectant in crafting.",
                Color = new Color(0.95f, 0.7f, 0.25f),
                SkinRate = 0.3f,
                DisinfectSeconds = 90f,
                SpawnFrequency = 2,
                Value = 4,
                Recognition = 3
            },
            new CreamDefinition
            {
                Id = "zelenka",
                Name = "Zelenka / Iodine",
                Description = "Brilliant green antiseptic. It does not heal tissue, but it disinfects fully and counts as a disinfectant in crafting. Stains the skin green for a while.",
                Color = new Color(0.2f, 0.75f, 0.3f),
                DisinfectSeconds = 600f,
                DisinfectQuality = 1f,
                Dye = new SkinDyeSpec
                {
                    Color = new Color(0.2f, 0.8f, 0.3f),
                    Strength = 0.35f
                },
                SpawnFrequency = 2,
                Value = 6,
                Recognition = 5
            },
            new CreamDefinition
            {
                Id = "lidocainegel",
                Name = "Lidocaine Gel",
                Description = "A numbing gel. It does not heal or disinfect; it simply silences pain and relaxes the treated limb for about a minute. Blessed relief.",
                Color = new Color(0.72f, 0.68f, 0.95f),
                Pain = PainEffect.LocalAnesthetic,
                SpawnFrequency = 1,
                Value = 8,
                Recognition = 6
            },
            new CreamDefinition
            {
                Id = "heparinoidgel",
                Name = "Heparinoid Gel",
                Description = "A vascular gel for bruises and swelling. Restores skin and muscle at 0.5x rate and disinfects lightly, though not enough for crafting.",
                Color = new Color(0.9f, 0.55f, 0.3f),
                SkinRate = 0.5f,
                MuscleRate = 0.5f,
                DisinfectSeconds = 120f,
                SpawnFrequency = 1,
                Value = 6,
                Recognition = 5
            },
            new CreamDefinition
            {
                Id = "stimgel",
                Name = "Stim-Gel",
                Description = "A growth-factor gel. Rebuilds skin and muscle at 2x the rate of Relief Cream. It does not disinfect anything.",
                Color = new Color(0.9f, 0.3f, 0.6f),
                SkinRate = 2f,
                MuscleRate = 2f,
                SpawnFrequency = 1,
                Value = 20,
                Recognition = 6
            },
            new CreamDefinition
            {
                Id = "autotanningspray",
                Name = "Auto-Tanning Spray",
                Description = "A dubious tanning spray. No healing value at all; it just turns the treated skin a convincing shade of orange. Purely cosmetic, probably.",
                Color = new Color(0.95f, 0.55f, 0.2f),
                Dye = new SkinDyeSpec
                {
                    Color = new Color(0.95f, 0.55f, 0.2f),
                    Strength = 0.6f
                },
                SpawnFrequency = 1,
                Value = 2,
                Recognition = 2
            },
            new CreamDefinition
            {
                Id = "nanozinplus",
                Name = "Nanozin-Plus",
                Description = "An advanced nanogel made from Nanozin, Vaseline, mushrooms, Relief Cream and Bio-Chem Fluid. Heals skin and muscle at 3.5x rate and speeds up broken-bone recovery, but it cannot disinfect or fix dislocations.",
                Color = new Color(0.2f, 0.65f, 0.95f),
                SkinRate = 3.5f,
                MuscleRate = 3.5f,
                BoneHealSeconds = 20f,
                SpawnFrequency = 0,
                Value = 40,
                Recognition = 8
            },
            new CreamDefinition
            {
                Id = "warriorpaint",
                Name = "Warrior Paint",
                Description = "Thick red war paint. No medical effect whatsoever; it simply paints the treated skin crimson. For intimidation, or for fun.",
                Color = new Color(0.75f, 0.1f, 0.1f),
                Dye = new SkinDyeSpec
                {
                    Color = new Color(0.75f, 0.08f, 0.08f),
                    Strength = 0.65f
                },
                SpawnFrequency = 1,
                Value = 1,
                Recognition = 2
            },
            new CreamDefinition
            {
                Id = "clownwhite",
                Name = "Clown White",
                Description = "Opaque white greasepaint. Zero healing, zero disinfection. Makes the treated skin pale as a clown's grin. Use responsibly.",
                Color = new Color(0.97f, 0.97f, 0.95f),
                Dye = new SkinDyeSpec
                {
                    Color = new Color(0.97f, 0.97f, 0.95f),
                    Strength = 0.7f
                },
                SpawnFrequency = 1,
                Value = 1,
                Recognition = 2
            },
            new CreamDefinition
            {
                Id = "curseditchcream",
                Name = "Cursed Itch Cream",
                Description = "It heals skin slightly faster than Vaseline, but the moment it touches skin it starts to itch. Pain rises by 10-20% and the patient will not thank you. No disinfection, no crafting value.",
                Color = new Color(0.55f, 0.5f, 0.25f),
                SkinRate = 0.25f,
                PainIncreaseMin = 10f,
                PainIncreaseMax = 20f,
                SpawnFrequency = 1,
                Value = 1,
                Recognition = 3
            }
        };

        internal static readonly IReadOnlyDictionary<string, CreamDefinition> ById = BuildIndex();

        private static Dictionary<string, CreamDefinition> BuildIndex()
        {
            Dictionary<string, CreamDefinition> index = new Dictionary<string, CreamDefinition>();
            foreach (CreamDefinition def in All)
            {
                index[def.Id] = def;
            }
            return index;
        }
    }
}
