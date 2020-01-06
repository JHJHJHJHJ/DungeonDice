using UnityEngine;
using System.Collections.Generic;

namespace DungeonDice.Stats
{
    public enum BuffType
    {
        foresee
    }

    public class Buffer : MonoBehaviour
    {
        public List<Buff> buffs = new List<Buff>();

        public void AddBuff(BuffType buffType, int value, int turn)
        {
            if (GetCurrentBuff(buffType) != null)
            {
                GetCurrentBuff(buffType).value += value;
                GetCurrentBuff(buffType).turn += value;
            }
            else
            {
                Buff newBuff = new Buff();
                newBuff.buffType = buffType;
                newBuff.value = value;
                newBuff.turn = turn;

                buffs.Add(newBuff);
            }
        }

        public Buff GetCurrentBuff(BuffType buffType)
        {
            foreach (Buff buff in buffs)
            {
                if (buff.buffType == buffType)
                {
                    return buff;
                }
            }

            return null;
        }

        public void ReduceAllBuffsTurn()
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                buffs[i].turn--;
                if (buffs[i].turn == 0)
                {
                    buffs.RemoveAt(i);
                }
            }
        }
    }

    [System.Serializable]
    public class Buff
    {
        public BuffType buffType;
        public int value;
        public int turn;
    }
}
