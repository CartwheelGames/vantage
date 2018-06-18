using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Teams
{
    public class TeamData : ScriptableObject
    {
        [SerializeField]
        private Color displayColor = new Color(0, 0, 0, 1);
        public Color DisplayColor { get { return displayColor; }}
        [SerializeField]
        private string displayName = "Team";
        public string DisplayName { get { return displayName; } }
    }
}
