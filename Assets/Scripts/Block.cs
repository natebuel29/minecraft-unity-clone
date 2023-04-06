using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Block", order = 1)]
    public class Block : Item
    {
        [Header("Block Information")]
        public Sprite blockIcon;
        public string blockName;
        public GameObject blockPrefab;
        public int stackedInventoryCount = 99;

    }
}