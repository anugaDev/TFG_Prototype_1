using System.Collections;
using System.Collections.Generic;
using Reliquary.Relic;
using UnityEngine;

namespace Reliquary.Maze
{
    public class MazeConfiguration : MonoBehaviour
    {
        public readonly RelicView relic;
        public readonly Transform[] wanderPoints;
        public readonly Transform fleePoint;
    }
}
