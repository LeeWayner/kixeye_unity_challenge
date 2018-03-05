using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeeWayner.WeightedRandomization
{
    public class WeightedRandomizer<T>
    {
        private List<WeightedChance<T>> elementList;
        private int totalWeight;
        public WeightedRandomizer(int maxValue = 10)
        {
            this.elementList = new List<WeightedChance<T>>(maxValue);
            totalWeight = 0;
        }

        public void AddOrUpdateValue(T value, int weight)
        {
            WeightedChance<T> element = TryGetValue(value);
            if (element == null)
            {
                elementList.Add(new WeightedChance<T>(value, weight));

            }
            else
            {
                element.Weight = weight;
            }

            AdjustWeight();
        }

        private void AdjustWeight()
        {
            totalWeight = 0;
            for (int i = 0; i < elementList.Count; i++)
            {
                totalWeight += elementList[i].Weight;
                elementList[i].AdjustedWeight = totalWeight;
            }
        }

        public T GetRandom()
        {
            int randomNumber = Random.Range(0, totalWeight);
            for (int i = 0; i < elementList.Count; i++)
            {
                if (randomNumber < elementList[i].AdjustedWeight)
                {
                    return elementList[i].Value;
                }
            }

            return default(T);
        }

        private WeightedChance<T> TryGetValue(T value)
        {
            for (int i = 0; i < elementList.Count; i++)
            {
                if (Equals(elementList[i].Value, value))
                {
                    return elementList[i];
                }
            }

            return null;
        }
    }
}
