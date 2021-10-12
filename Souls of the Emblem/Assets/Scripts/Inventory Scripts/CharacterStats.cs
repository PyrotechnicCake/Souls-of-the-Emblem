using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class CharacterStats : MonoBehaviour
    {
        //name and class

        //basestats
        [Header("Base Stats")]
        public int baseHP;
        public int baseStr;
        public int baseMag;
        public int baseSkl;
        public int baseSpd;
        public int baseLck;
        public int baseDef;
        public int baseRes;

        //stats
        [Header("Stats")]
        public int maxHP;
        public int currentHP;
        public int maxStam;
        public int currentStam;
        public int str;
        public int mag;
        public int skl;
        public int spd;
        public int lck;
        public int def;
        public int res;

        //weapon profeciencies
        [Header("Weapon Profeciency")]
        public int swordProf;
        public int lanceProf;
        public int axeProf;
    }
}
