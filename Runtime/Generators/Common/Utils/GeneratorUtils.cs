using System;
using System.Collections.Generic;
using System.Linq;
using MapGeneration.Interfaces.Core.MapDescriptions;
using MapGeneration.Interfaces.Core.MapLayouts;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.Doors;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;
using Object = UnityEngine.Object;
using OrthogonalLine = GeneralAlgorithms.DataStructures.Common.OrthogonalLine;

namespace ProceduralLevelGenerator.Unity.Generators.Common.Utils
{
    public static class GeneratorUtils
    {
        public static GeneratedLevel TransformLayout(IMapLayout<Room> layout, LevelDescription levelDescription, GameObject rootGameObject)
        {
            return null;
        }

        private static List<DoorInstance> TransformDoorInfo(IEnumerable<IDoorInfo<Room>> doorInfos, Dictionary<Room, RoomInstance> roomInstances)
        {
            return doorInfos.Select(x => TransformDoorInfo(x, roomInstances[x.Node])).ToList();
        }

        private static DoorInstance TransformDoorInfo(IDoorInfo<Room> doorInfo, RoomInstance connectedRoomInstance)
        {
            var doorLine = doorInfo.DoorLine;

            switch (doorLine.GetDirection())
            {
                case OrthogonalLine.Direction.Right:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.up,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLine.Direction.Left:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.down,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLine.Direction.Top:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.From.ToUnityIntVector3(), doorLine.To.ToUnityIntVector3()), Vector2Int.left,
                        connectedRoomInstance.Room, connectedRoomInstance);

                case OrthogonalLine.Direction.Bottom:
                    return new DoorInstance(new Unity.Utils.OrthogonalLine(doorLine.To.ToUnityIntVector3(), doorLine.From.ToUnityIntVector3()), Vector2Int.right,
                        connectedRoomInstance.Room, connectedRoomInstance);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static RepeatMode? GetRepeatMode(RepeatModeOverride repeatModeOverride)
        {
            switch (repeatModeOverride)
            {
                case RepeatModeOverride.NoOverride:
                    return null;

                case RepeatModeOverride.AllowRepeat:
                    return RepeatMode.AllowRepeat;

                case RepeatModeOverride.NoImmediate:
                    return RepeatMode.NoImmediate;

                case RepeatModeOverride.NoRepeat:
                    return RepeatMode.NoRepeat;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}