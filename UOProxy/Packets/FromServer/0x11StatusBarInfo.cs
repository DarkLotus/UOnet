using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x11StatusBarInfo : Packet
    {
        short Length;
        public int PlayerID;
        public string PlayerName;
        public short HitsCurrent, HitsMax;
        public byte NameChangeFlag, StatusFlag, SexRace;
        public short Str, Dex, Int, StamCurrent, StamMax, ManaCurrent, ManaMax;
        public int Gold;
        public short AR, Weight;

        //Flag 5>
        public short MaxWeight;
        public byte Race;

        //Flag 3>
        public short StatCap;
        public byte Followers, FollowersMax;

        //Flag 4
        public short FireRes, ColdRes, PoisonRes, EnergyRes, Luck, DmgMin, DmgMax;
        public int TithingPoints;

        //Flag 6
        public short HitChanceIncrease, SwingSpeedIncrease, DamageChanceIncrease, LowerReagCost, HitsRegen, StamRegen, ManaRegen, ReflectPhys;
        public short EnhancePotions, DefenseChanceIncrease, SpellDamageIncrease, FasterCastRecovery, FasterCasting, LowerManaCost, StrIncrease, DexIncrease, IntIncrease;
        public short HitsIncrease, StamIncrease, ManaIncrease, MaxHitsIncrease, MaxStamIncrease, MaxManaIncrease;
        public _0x11StatusBarInfo(UOStream Data)
            : base(Data)
        {
            Length = Data.ReadShort();
            PlayerID = Data.ReadInt();
            PlayerName = Data.Read30CharString();
            HitsCurrent = Data.ReadShort();
            NameChangeFlag = Data.ReadBit();
            StatusFlag = Data.ReadBit();
            SexRace = Data.ReadBit();
            Str = Data.ReadShort();
            Dex = Data.ReadShort();
            Int = Data.ReadShort();
            StamCurrent = Data.ReadShort();
            StamMax = Data.ReadShort();
            ManaCurrent = Data.ReadShort();
            ManaMax = Data.ReadShort();
            Gold = Data.ReadInt();
            AR = Data.ReadShort();
            Weight = Data.ReadShort();
            if (StatusFlag >= 0x5)
            {
                MaxWeight = Data.ReadShort();
                Race = Data.ReadBit();
            }
            if (StatusFlag >= 0x3)
            {
                StatCap = Data.ReadShort();
                Followers = Data.ReadBit();
                FollowersMax = Data.ReadBit();
            }
            if (StatusFlag >= 0x04)
            {
                FireRes = Data.ReadShort();
                ColdRes = Data.ReadShort();
                PoisonRes = Data.ReadShort();
                EnergyRes = Data.ReadShort();
                Luck = Data.ReadShort();
                DmgMin = Data.ReadShort();
                DmgMax = Data.ReadShort();
                TithingPoints = Data.ReadInt();
            }
            if (StatusFlag >= 0x06)
            {
                HitChanceIncrease = Data.ReadShort();
                SwingSpeedIncrease = Data.ReadShort();
                DamageChanceIncrease = Data.ReadShort();
                LowerReagCost = Data.ReadShort();
                HitsRegen = Data.ReadShort();
                StamRegen = Data.ReadShort();
                ManaRegen = Data.ReadShort();
                ReflectPhys = Data.ReadShort();
                EnhancePotions = Data.ReadShort();
                DefenseChanceIncrease = Data.ReadShort();
                SpellDamageIncrease = Data.ReadShort();
                FasterCastRecovery = Data.ReadShort();
                FasterCasting = Data.ReadShort();
                LowerManaCost = Data.ReadShort();
                StrIncrease = Data.ReadShort();
                DexIncrease = Data.ReadShort();
                IntIncrease = Data.ReadShort();
                HitsIncrease = Data.ReadShort();
                StamIncrease = Data.ReadShort();
                ManaIncrease = Data.ReadShort();
                MaxHitsIncrease = Data.ReadShort();
                MaxStamIncrease  = Data.ReadShort();
                MaxManaIncrease = Data.ReadShort();
            }
        }
    }
}
