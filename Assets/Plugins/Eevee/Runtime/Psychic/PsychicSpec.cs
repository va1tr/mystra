using UnityEngine;

namespace Eevee
{
	public class PsychicSpec
	{
        public readonly ScriptablePsychic asset;

        public Statistic health = new Statistic();
        public Statistic attack = new Statistic();
        public Statistic defence = new Statistic();
        public Statistic speed = new Statistic();

        public readonly string name;

        public bool isDead => health.value <= 0;

        public int level;
        public int experience;

        public readonly TraitSpec trait;

        private readonly AbilitySpec[] m_Abilities = new AbilitySpec[kMaxNumberOfAbilities];

        public AbilitySpec analyse;

        private const int kMaxNumberOfTraits = 4;
        private const int kMaxNumberOfAbilities = 4;

        public PsychicSpec(ScriptablePsychic asset)
        {
            this.asset = asset;

            this.name = asset.name.ToUpper();
            this.level = asset.level;

            experience = Mathf.FloorToInt(Mathf.Pow(level, 3f));

            this.trait = asset.trait.CreateTraitSpec();

            //CreateStartupTraits(asset);
            CreateStartupAbilities(asset);
            CreateStartupStats(asset);
        }

        public PsychicSpec(ScriptablePsychic asset, int level)
        {
            this.asset = asset;

            this.name = asset.name.ToUpper();
            this.level = level;

            experience = Mathf.FloorToInt(Mathf.Pow(level, 3f));

            this.trait = asset.trait.CreateTraitSpec();

            //CreateStartupTraits(asset);

            CreateStartupAbilities(asset);
            CreateStartupStats(asset);
        }

        private void CreateStartupTraits(ScriptablePsychic asset)
        {
            
        }

        private void CreateStartupAbilities(ScriptablePsychic asset)
        {
            int index = 0;
            var abilities = asset.abilities;

            for (int i = 0; i < abilities.Length; i++)
            {
                int levelRequired = abilities[i].levelRequired;

                if (levelRequired <= level)
                {
                    m_Abilities[index] = abilities[i].ability.CreateAbilitySpec();

                    index++;

                    if (index >= kMaxNumberOfAbilities)
                    {
                        index = 0;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void CreateStartupStats(ScriptablePsychic asset)
        {
            health.iv = Random.Range(1, 32);
            health.value = health.maxValue = CalculateHealthStatValue(asset.health);

            attack.iv = Random.Range(1, 32);
            attack.value = attack.maxValue = CalculateStatValue(asset.attack, attack);

            defence.iv = Random.Range(1, 32);
            defence.value = defence.maxValue = CalculateStatValue(asset.defence, defence);

            speed.iv = Random.Range(1, 32);
            speed.value = speed.maxValue = CalculateStatValue(asset.speed, speed);

            Debug.Log($"{name} HP{health.value} ATK:{attack.value} DEF:{defence.value} SPD:{speed.value}");
        }

        private float CalculateHealthStatValue(float baseValue)
        {
            return Mathf.Floor((2f * baseValue + health.iv + Mathf.Floor(health.ev / 4f)) * level / 100) + level + 10f;
        }

        private float CalculateStatValue(float baseValue, Statistic stat)
        {
            return Mathf.Floor((Mathf.Floor((2f * baseValue + stat.iv + Mathf.Floor(stat.ev / 4f)) * level / 100) + 5f) * 1f);
        }

        public void LevelUp()
        {
            level++;

            health.value = Mathf.Floor(CalculateHealthStatValue(asset.health) * (health.value / health.maxValue));
            health.maxValue = CalculateHealthStatValue(asset.health);

            attack.value = attack.maxValue = CalculateStatValue(asset.attack, attack);
            defence.value = defence.maxValue = CalculateStatValue(asset.defence, defence);
            speed.value = speed.maxValue = CalculateStatValue(asset.speed, speed);

            //Debug.Log($"{name} HP{health.maxValue} ATK:{attack.value} DEF:{defence.value} SPD:{speed.value}");
        }

        public bool TryGetAbility(int index, out AbilitySpec spec)
        {
            spec = null;

            if (index < kMaxNumberOfAbilities && m_Abilities[index] != null)
            {
                spec = m_Abilities[index];
                return true;
            }

            return false;
        }

        public AbilitySpec[] GetAllAbilities()
        {
            return m_Abilities;
        }

        public AbilitySpec GetRandomAbility()
        {
            AbilitySpec ability = null;
            
            do
            {
                ability = m_Abilities[Random.Range(0, m_Abilities.Length - 1)];
            } while (ability == null);

            return ability;
        }

        public bool TryGetStatByType(StatisticType type, out Statistic statistic)
        {
            statistic = null;

            switch (type)
            {
                case StatisticType.Health:
                    statistic = health;
                    break;
                case StatisticType.Attack:
                    statistic = attack;
                    break;
                case StatisticType.Defence:
                    statistic = defence;
                    break;
                case StatisticType.Speed:
                    statistic = speed;
                    break;
            }

            return statistic != null;
        }
    }
}
