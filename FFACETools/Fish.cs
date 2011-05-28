﻿using System;

namespace FFACETools {
	public partial class FFACE {
		/// <summary>
		/// Wrapper class for Fishing information from FFACE
		/// </summary>
		public class FishTools {
			#region Classes

			/// <summary>
			/// ID of a fish
			/// </summary>
			public struct FishID {
				/// <summary>
				/// Part 1 of the fish ID
				/// </summary>
				public int ID1 { get; set; }

				/// <summary>
				/// Part 2 of the fish ID
				/// </summary>
				public int ID2 { get; set; }

				/// <summary>
				/// Part 3 of the fish ID
				/// </summary>
				public int ID3 { get; set; }

				/// <summary>
				/// Part 4 of the fish ID
				/// </summary>
				public int ID4 { get; set; }

				public override bool Equals(object obj)
				{
					bool bResult = false;

					if (obj is FishID)
					{
						if (this.ID1.Equals(((FishID)obj).ID1)
							&& this.ID2.Equals(((FishID)obj).ID2)
							&& this.ID3.Equals(((FishID)obj).ID3)
							&& this.ID4.Equals(((FishID)obj).ID4))
							bResult = true;
					}

					return bResult;
				}

				public override int GetHashCode()
				{
					return this.ID1.GetHashCode();
				}

			} // @ public struct FishID

			#endregion

			#region Constructor

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="instance">Instance ID generated by FFACE</param>
			public FishTools(int instanceID)
			{
				_InstanceID = instanceID;

			} // @ public Fish(int instance)

			#endregion

			#region Members

			/// <summary>
			/// Instance ID generated by FFACE
			/// </summary>
			private int _InstanceID { get; set; }

			/// <summary>
			/// If player has a fish on the hook
			/// </summary>
			public bool FishOnLine
			{
				get { return FishOnLine(_InstanceID); }

			} // @ public bool FishOnLine

			/// <summary>
			/// Current position of the rod
			/// </summary>
			public RodAlign RodPosition
			{
				get { return GetRodPosition(_InstanceID); }

			} // @ public RodAlign RodPosition

			/// <summary>
			/// Fish's maximum hit points
			/// </summary>
			public int HPMax
			{
				get { return GetFishHPMax(_InstanceID); }

			} // @ public int HPMax

			/// <summary>
			/// Fish's current hit points
			/// </summary>
			public int HPCurrent
			{
				get { return GetFishHP(_InstanceID); }

			} // @ public int HPCurrent

			/// <summary>
			/// Time fish has been on the line
			/// </summary>
			public int OnLineTime
			{
				get { return GetFishOnlineTime(_InstanceID); }

			} // @ public int OnLineTime

			/// <summary>
			/// Time, in seconds, before we lose the catch
			/// </summary>
			public int Timeout
			{
				get { return GetFishTimeout(_InstanceID); }

			} // @ public int Timeout

			/// <summary>
			/// ID of the fish on the hook
			/// </summary>
			public FishID ID
			{
				get
				{
					// get all the fish id's
					FishID id = new FishID();
					id.ID1 = GetFishID1(_InstanceID);
					id.ID2 = GetFishID2(_InstanceID);
					id.ID3 = GetFishID3(_InstanceID);
					id.ID4 = GetFishID4(_InstanceID);

					return id;
				}

			} // @ public FishID ID

			/// <summary>
			/// Set the current HP of the fish on the line.
			/// </summary>
			/// <param name="value"></param>
			/// <returns></returns>
			public bool SetHP(int value)
			{
				bool result = false;

				if (SetFishHP(_InstanceID, value))
				{
					result = true;
				}
				return result;
			} // @ public bool SetHP

			/// <summary>
			/// Set the timeout value in seconds for the fish on the line (second before you lose your catch).
			/// </summary>
			/// <param name="value"></param>
			/// <returns></returns>
			public bool SetFishTimeOut(short value)
			{
				bool result = false;

				if (SetTimeOut(_InstanceID, value))
				{
					result = true;
				}
				return result;
			} // @ public bool SetFishTimeOut

			#endregion

			/// <summary>
			/// Return value of FightFish success.
			/// </summary>
			public bool FightFish()
			{
				return FFACE.FightFish(_InstanceID);
			} // @ public bool Fight

		} // @ public class FishTools
	} // @ public partial class FFACE
}
