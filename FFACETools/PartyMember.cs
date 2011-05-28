﻿using System;

namespace FFACETools {
	public partial class FFACE {
		/// <summary>
		/// Wrapper class for party member information
		/// </summary>
		public class PartyMemberTools {
			#region Constructor

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="instanceID">Instance ID generated by FFACE</param>
			public PartyMemberTools(int instanceID, byte index)
			{
				_InstanceID = instanceID;
				_Index = index;

			} // @ public PartyWrapper(int instanceID)

			#endregion

			#region Members

			/// <summary>
			/// Instance ID generated by FFACE
			/// </summary>
			private int _InstanceID { get; set; }

			/// <summary>
			/// The index of the selected party member
			/// </summary>
			private byte _Index { get; set; }

			/// <summary>
			/// Name of the member
			/// </summary>
			public string Name
			{
				get { return GetPartyMemberInformation().Name; }

			} // @ public string Name

			/// <summary>
			/// ID of the member
			/// </summary>
			public int ID
			{
				get { return GetPartyMemberInformation().ID; }

			} // @ public int ID

			/// <summary>
			/// Server ID of the member
			/// </summary>
			public int ServerID
			{
				get { return GetPartyMemberInformation().SvrID; }

			} // @ public int ServerID

			/// <summary>
			/// Members current TP
			/// </summary>
			public int TPCurrent
			{
				get { return GetPartyMemberInformation().CurrentTP; }

			} // @ public int TPCurrent

			/// <summary>
			/// Members current hit points
			/// </summary>
			public int HPCurrent
			{
				get { return GetPartyMemberInformation().CurrentHP; }

			} // @ public int HPCurrent

			/// <summary>
			/// Members current hit points in percent
			/// </summary>
			public int HPPCurrent
			{
				get { return GetPartyMemberInformation().CurrentHPP; }

			} // @ public int HPPCurrent

			/// <summary>
			/// Members current mana
			/// </summary>
			public int MPCurrent
			{
				get { return GetPartyMemberInformation().CurrentMP; }

			} // @ public int MPCurrent

			/// <summary>
			/// Members current mp in percent
			/// </summary>
			public int MPPCurrent
			{
				get { return GetPartyMemberInformation().CurrentMPP; }

			} // @ public int MPPCurrent

			/// <summary>
			/// Members current zone
			/// </summary>
			public Zone Zone
			{
				get { return (Zone)GetPartyMemberInformation().Zone; }

			} // @ public int Zone

			/// <summary>
			/// UNKNOWN
			/// </summary>
			public uint FlagMask
			{
				get { return GetPartyMemberInformation().FlagMask; }

			} // @ public uint FlagMask 

			/// <summary>
			/// If the party member is present in memory
			/// </summary>
			public bool Active
			{
				get { return Convert.ToBoolean(GetPartyMemberInformation().Active); }

			} // @ public bool Active

			#endregion

			#region Methods

			/// <summary>
			/// Gets the party member information for the passed party member
			/// </summary>
			/// <param name="index">Index of the party member to get</param>
			private PARTYMEMBER GetPartyMemberInformation()
			{
				PARTYMEMBER partyMember = new PARTYMEMBER();
				GetPartyMember(_InstanceID, _Index, ref partyMember);

				return partyMember;

			} // @ private PARTYMEMBER GetPartyMemberInformation(byte index)

			#endregion

		} // @ public class PartyMemberTools
	} // @ public partial class FFACE
}
