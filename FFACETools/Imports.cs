﻿using System;
using System.Runtime.InteropServices;

namespace FFACETools {
	public partial class FFACE {
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int CreateInstance(UInt32 PID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void DeleteInstance(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetLoginStatus(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool Access(int instanceID);


		#region Player Imports

		/// <summary>
		/// FFACE 4.0.0.9+ import for player information
		/// </summary>
		/// <param name="instance">Instance id generated by FFACE</param>
		/// <param name="playerInfo">Reference to playerinfo struct to be returned by function</param>
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetPlayerInfo(int instanceID, ref PLAYERINFO playerInfo);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern Status GetPlayerStatus(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern ViewMode GetViewMode(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetCastMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetCastCountDown(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetCastPercent(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short GetCastPercentEx(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetWeatherType(int instanceID);
        [DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool HasKeyItem(int instanceID, uint ID);
		#endregion

		#region Party Member Imports

		/// <summary>
		/// FFACE 4.0.0.9 import to get a party members information
		/// </summary>
		/// <param name="instance">Instance ID generated by FFACE</param>
		/// <param name="index">Index of party member</param>
		/// <param name="partyMember">Information returned by reference about party member</param>
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetPartyMember(int instanceID, byte index, ref PARTYMEMBER partyMember);

		#endregion

		#region Chat Imports

		/*
		* FFACE 4.0.0.9 Imports
		*/
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool IsNewLine(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetChatLine(int instanceID, short index, byte[] buffer, ref int size);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetChatLineCount(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetChatLineEx(int instanceID, short index, byte[] buffer, ref int size, ref ChatMode extra);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetChatLineR(int instanceID, short index, byte[] buffer, ref int size);

		#endregion

		#region Timer Imports

		/*
		* FFACE 4.0.0.9 Imports
		*/
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short GetSpellRecast(int instanceID, SpellList index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern AbilityList GetAbilityID(int instanceID, byte index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetAbilityRecast(int instanceID, byte index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetVanaUTC(int instanceID);

		#endregion

		#region Inventory Imports

		/*
		* FFACE 4.0.0.9 Imports
		*/
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetInventoryMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetSafeMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetStorageMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetTempMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetLockerMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetSatchelMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetSackMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern INVENTORYITEM GetInventoryItem(int instanceID, int index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern INVENTORYITEM GetSafeItem(int instanceID, int index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern INVENTORYITEM GetStorageItem(int instanceID, int index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern INVENTORYITEM GetTempItem(int instanceID, int index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern INVENTORYITEM GetLockerItem(int instanceID, int index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern INVENTORYITEM GetSatchelItem(int instanceID, int index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern INVENTORYITEM GetSackItem(int instanceID, int index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetSelectedItemName(int instanceID, byte[] buffer, ref int size);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetSelectedItemNum(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetSelectedItemIndex(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern TREASUREITEM GetTreasureItem(int instanceID, byte index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetEquippedItemIndex(int instanceID, EquipSlot slot);

		#endregion

		#region NPC/PC Imports

		/*
		 * FFACE 4.0.0.9 Imports
		 */
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetNPCBit(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern NPCType GetNPCType(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool IsNPCclaimed(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetNPCclaimID(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool NPCIsActive(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetNPCName(int instanceID, int npcID, byte[] buffer, ref int size);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetNPCPosX(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetNPCPosY(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetNPCPosZ(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetNPCPosH(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetNPCHPP(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern Status GetNPCStatus(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetNPCPetID(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short GetNPCTP(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float GetNPCDistance(int instanceID, int npcID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern double GetNPCHeadingToNPC(int instanceID, int npcIDStart, int npcIDEnd);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool SetNPCTradeInfo(int InstanceID, int Zero, ushort ItemID, byte Index, byte Count, byte Box, UInt32 Gil);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetNPCTradeInfo(int instanceID, ref TRADEINFO info);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool IsSynthesis(int InstanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern float SetNPCPosH(int InstanceID, int index, float value);
		/*		
		 *		FFACE paid functions
		 *
		 *		BOOL SetNPCTradeInfo(void* inst, int Code, unsigned short ItemID, char Index, char Count, char Box, unsigned int Gil)
		 *		void GetNPCTradeInfo(void* inst, TRADEINFO* TI)
		 *		BOOL IsSynthesis(void* inst)
		 */
		[DllImport("FFACE.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, EntryPoint = "SetCraftItem")]
		private static extern bool SetCraftItem(int instanceID, int Zero, ushort ItemID, byte Index, byte Count, byte Box);


		#endregion

		#region Target Imports

		/// <summary>
		/// FFACE 4.0.0.9 import to get information about the target
		/// </summary>
		/// <param name="instance">Instance ID generated by FFACE</param>
		/// <param name="targetInfo">Information returned by reference about the target</param>
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetTargetInfo(int instanceID, ref TARGETINFO targetInfo);
        [DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool SetTarget(int instanceID, int index);
		#endregion

		#region Windower Imports

		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CKHSendString(int instanceID, string data);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CKHSetKey(int instanceID, KeyCode key, bool down);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHCreateTextObject(int instanceID, string name);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetText(int instanceID, string name, string data);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetColor(int instanceID, string name, byte transparent, byte red, byte green, byte blue);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetLocation(int instanceID, string name, float vertical, float horizontal);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetVisibility(int instanceID, string name, bool visible);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHFlushCommands(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHDeleteTextObject(int instanceID, string name);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetBold(int instanceID, string name, bool bold);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetItalic(int instanceID, string name, bool italic);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetBGColor(int instanceID, string name, byte transparent, byte red, byte green, byte blue);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetBGBorderSize(int instanceID, string name, float pixels);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetBGVisibility(int instanceID, string name, bool visible);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetRightJustified(int instanceID, string name, bool justified);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CTHSetFont(int instanceID, string name, string font, short height);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CKHBlockInput(int instanceID, bool block);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short CCHGetArgCount(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void CCHGetArg(int instanceID, short index, ref string buffer);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int CCHIsNewCommand(int instanceID);

		#endregion

		#region Fish Imports

		/*
		* FFACE 4.0.0.9 imports
		*/
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool FishOnLine(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern RodAlign GetRodPosition(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetFishHPMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetFishHP(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short GetFishOnlineTime(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short GetFishTimeout(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetFishID1(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetFishID2(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetFishID3(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetFishID4(int instanceID);

		/*		
		 *		FFACE paid functions
		 *
		 *		bool SetFishHP(void* inst, int value)
		 *		bool FightFish(void* inst)
		 *		bool SetTimeOut(void* inst, short value)
		 */
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool SetFishHP(int InstanceID, int value);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool SetTimeOut(int InstanceID, short value);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool FightFish(int InstanceID);
		#endregion

		#region Alliance Imports

		/// <summary>
		/// FFACE 4.0.0.9 import to get information about the alliance
		/// </summary>
		/// <param name="instance">Instance ID generated by FFACE</param>
		/// <param name="allianceInfo">Information returned by reference about the alliance</param>
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetAllianceInfo(int instance, ref ALLIANCEINFO allianceInfo);

		#endregion

		#region Menu Imports

		/*
		 * FFACE 4.0.0.10 imports
		 */
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern bool MenuIsOpen(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void MenuName(int instanceID, byte[] buffer, ref int size);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void MenuSelection(int instanceID, byte[] buffer, ref int size);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void MenuHelp(int instanceID, byte[] buffer, ref int size);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte ShopQuantityMax(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte ShopQuantity(int instanceID);

		/*  FFACE 4.1.0.14 Exports */
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetDialogStrings(int instanceID, byte[] buffer, ref int size);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short GetDialogIndex(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetDialogIndexCount(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern short GetDialogID(int instanceID);
		#endregion

		#region Search Imports

		/*
		 * FFACE v4.0.0.10 imports
		 */
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern int GetSearchTotalCount(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetSearchPageCount(int instanceID);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern Zone GetSearchZone(int instanceID, short index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern Job GetSearchMainJob(int instanceID, short index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern Job GetSearchSubJob(int instanceID, short index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetSearchMainlvl(int instanceID, short index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern byte GetSearchSublvl(int Instance, short index);
		[DllImport(FFACE_LIBRARY, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention=CallingConvention.Cdecl)]
		private static extern void GetSearchName(int instanceID, short index, byte[] buffer, ref int size);

		#endregion

	} // @ public partial class FFACE
}
