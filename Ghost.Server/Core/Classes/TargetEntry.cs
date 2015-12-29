﻿using Ghost.Server.Utilities;
using PNet;
using System.Numerics;

namespace Ghost.Server.Core.Classes
{
    public class TargetEntry : INetSerializable
    {
        public int Guid;
        public int SkillID;
        public double Time;
        public int Upgrade;
        public Vector3 Position;
        public bool HasGuid
        {
            get { return Guid != -1; }
        }
        public int AllocSize
        {
            get
            {
                return Guid == -1 ? 28 : 30;
            }
        }
        public TargetEntry()
        {
            Guid = -1;
        }
        public void OnSerialize(NetMessage message)
        {
            message.Write(SkillID);
            message.Write(Position);
            message.Write(Upgrade);
            message.WriteFixedTime(false);
            if (Guid != -1) message.Write((ushort)Guid);
        }
        public void OnDeserialize(NetMessage message)
        {
            SkillID = message.ReadInt32();
            Time = message.ReadFixedTime(false);
            if (message.RemainingBits == 16)
            {
                Position = Vector3.Zero;
                Guid = message.ReadUInt16();
            }
            else
            {
                Guid = -1;
                Position = message.ReadVector3();
            }
        }
        public override string ToString()
        {
            return $"Skill: {SkillID}, Position{(Guid == -1 ? ": " + Position.ToString() : " at NV " + Guid.ToString())}";
        }
    }
}