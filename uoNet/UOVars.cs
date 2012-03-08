/*
 * Copyright (C) 2011 - 2012 James Kidd
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uoNet
{
    
    public partial class UO
    {
        //All the global variables exposed the same as in OpenEUO
        #region Client Variables

        #region Character Info

        public int CharPosX
        {
            get
            {
                return GetInt("CharPosX");
            }
        }
        public int CharPosY
        {
            get
            {
                return GetInt("CharPosY");
            }
        }
        public int CharPosZ
        {
            get
            {
                return GetInt("CharPosZ");
            }
        }
        public int CharDir
        {
            get
            {
                return GetInt("CharDir");
            }
        }
        public int CharID
        {
            get
            {
                return GetInt("CharID");
            }
        }
        public int CharType
        {
            get
            {
                return GetInt("CharType");
            }
        }
        public int CharStatus
        {
            get
            {
                return GetInt("CharStatus");
            }
        }
        public int BackpackID
        {
            get
            {
                return GetInt("BackpackID");
            }
        }
#endregion

        #region Status Info
        public string CharName
        {
            get
            {
                return GetString("CharName");
            }
        }

        public int Sex
        {
            get
            {
                return GetInt("Sex");
            }
        }

        public int Str
        {
            get
            {
                return GetInt("Str");
            }
        }

        public int Dex
        {
            get
            {
                return GetInt("Dex");
            }
        }

        public int Int
        {
            get
            {
                return GetInt("Int");
            }
        }

        public int Hits
        {
            get
            {
                return GetInt("Hits");
            }
        }

        public int MaxHits
        {
            get
            {
                return GetInt("MaxHits");
            }
        }

        public int Stamina
        {
            get
            {
                return GetInt("Stamina");
            }
        }

        public int MaxStam
        {
            get
            {
                return GetInt("MaxStam");
            }
        }

        public int Mana
        {
            get
            {
                return GetInt("Mana");
            }
        }

        public int MaxMana
        {
            get
            {
                return GetInt("MaxMana");
            }
        }

        public int MaxStats
        {
            get
            {
                return GetInt("MaxStats");
            }
        }

        public int Luck
        {
            get
            {
                return GetInt("Luck");
            }
        }

        public int Weight
        {
            get
            {
                return GetInt("Weight");
            }
        }

        public int MaxWeight
        {
            get
            {
                return GetInt("MaxWeight");
            }
        }

        public int MinDmg
        {
            get
            {
                return GetInt("MinDmg");
            }
        }

        public int MaxDmg
        {
            get
            {
                return GetInt("MaxDmg");
            }
        }

        public int Gold
        {
            get
            {
                return GetInt("Gold");
            }
        }

        public int Followers
        {
            get
            {
                return GetInt("Followers");
            }
        }

        public int MaxFol
        {
            get
            {
                return GetInt("MaxFol");
            }
        }

        public int AR
        {
            get
            {
                return GetInt("AR");
            }
        }

        public int FR
        {
            get
            {
                return GetInt("FR");
            }
        }

        public int CR
        {
            get
            {
                return GetInt("CR");
            }
        }

        public int PR
        {
            get
            {
                return GetInt("PR");
            }
        }

        public int ER
        {
            get
            {
                return GetInt("ER");
            }
        }

        public int TP
        {
            get
            {
                return GetInt("TP");
            }
        }
        #endregion

        #region Container Info
        public int ContID
        {
            get
            {
                return GetInt("ContID");
            }
        }
        public int ContType
        {
            get
            {
                return GetInt("ContType");
            }
        }
        public int ContKind
        {
            get
            {
                return GetInt("ContKind");
            }
        }
        public string ContName
        {
            get
            {
                return GetString("ContName");
            }
        }
        public int ContPosX
        {
            get
            {
                return GetInt("ContPosX");
            }
        }
        public int ContPosY
        {
            get
            {
                return GetInt("ContPosY");
            }
        }
        public int ContSizeX
        {
            get
            {
                return GetInt("ContSizeX");
            }
        }
        public int ContSizeY
        {
            get
            {
                return GetInt("ContSizeY");
            }
        }
        public int NextCPosX
        {
            get
            {
                return GetInt("NextCPosX");
            }
            set
            {
                SetInt("NextCPosX", value);
            }
        }
        public int NextCPosY
        {
            get
            {
                return GetInt("NextCPosY");
            }
            set
            {
                SetInt("NextCPosY", value);
            }
        }


        #endregion

        #region Client Info
        public int CliNr
        {
            get
            {
                return GetInt("CliNr");
            }
        }
        public int CliCnt
        {
            get
            {
                return GetInt("CliCnt");
            }
        }
        public string CliLang
        {
            get
            {
                return GetString("CliLang");
            }
        }
        public int CliVer
        {
            get
            {
                return GetInt("CliVer");
            }
        }
        public bool CliLogged
        {
            get
            {
                return GetBoolean("LObjectID");
            }
        }
        public int CliLeft
        {
            get
            {
                return GetInt("CliLeft");
            }
        }
        public int CliTop
        {
            get
            {
                return GetInt("CliTop");
            }
        }
        public int CliXRes
        {
            get
            {
                return GetInt("CliXRes");
            }
        }
        public int CliYRes
        {
            get
            {
                return GetInt("CliYRes");
            }
        }
        public string CliTitle
        {
            get
            {
                return GetString("CliTitle");
            }
        }
        #endregion

        #region Last Action
        public int LObjectID
        {
            get
            {
                return GetInt("LObjectID");
            }
            set
            {
                SetInt("LObjectID", value);
            }
        }
        public int LObjectType
        {
            get
            {
                return GetInt("LObjectType");
            }
            set
            {
                SetInt("LObjectType", value);
            }
        }
        public int LTargetID
        {
            get
            {
                return GetInt("LTargetID");
            }
            set
            {
                SetInt("LTargetID", value);
            }
        }
        public int LTargetKind
        {
            get
            {
                return GetInt("LTargetKind");
            }
            set
            {
                SetInt("LTargetKind", value);
            }
        }
        public int LTargetTile
        {
            get
            {
                return GetInt("LTargetTile");
            }
            set
            {
                SetInt("LTargetTile", value);
            }
        }
        public int LTargetX
        {
            get
            {
                return GetInt("LTargetX");
            }
            set
            {
                SetInt("LTargetX", value);
            }
        }
        public int LTargetY
        {
            get
            {
                return GetInt("LTargetY");
            }
            set
            {
                SetInt("LTargetY", value);
            }
        }
        public int LTargetZ
        {
            get
            {
                return GetInt("LTargetZ");
            }
            set
            {
                SetInt("LTargetZ", value);
            }
        }
        public int LLiftedID
        {
            get
            {
                return GetInt("LLiftedID");
            }
            set
            {
                SetInt("LLiftedID", value);
            }
        }
        public int LLiftKind
        {
            get
            {
                return GetInt("LLiftKind");
            }
            set
            {
                SetInt("LLiftKind", value);
            }
        }
        public int LLiftType
        {
            get
            {
                return GetInt("LLiftType");
            }
            set
            {
                SetInt("LLiftType", value);
            }
        }
        public int LSkill
        {
            get
            {
                return GetInt("LSkill");
            }
            set
            {
                SetInt("LSkill", value);
            }
        }
        public int LSpell
        {
            get
            {
                return GetInt("LSpell");
            }
            set
            {
                SetInt("LSpell", value);
            }
        }
        #endregion

        #region Misc
        public int EnemyHits
        {
            get
            {
                return GetInt("EnemyHits");
            }
        }
        public int EnemyID
        {
            get
            {
                return GetInt("EnemyID");
            }
        }
        public int RHandID
        {
            get
            {
                return GetInt("RHandID");
            }
        }
        public int LHandID
        {
            get
            {
                return GetInt("LHandID");
            }
        }
        public int CursorX
        {
            get
            {
                return GetInt("CursorX");
            }
        }
        public int CursorY
        {
            get
            {
                return GetInt("CursorY");
            }
        }
        public int CursKind
        {
            get
            {
                return GetInt("CursKind");
            }
        }
        public bool TargCurs
        {
            get
            {
                return GetBoolean("TargCurs");
            }
        }
        public string Shard
        {
            get
            {
                return GetString("Shard");
            }
        }
        public string LShard
        {
            get
            {
                return GetString("LShard");
            }
        }
        public string SysMsg
        {
            get
            {
                return GetString("SysMsg");
            }
        }
        #endregion
        #endregion


     
    }
}
