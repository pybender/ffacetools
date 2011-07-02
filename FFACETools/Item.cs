﻿using System;

namespace FFACETools {
	public partial class FFACE {
		/// <summary>
		/// Wrapper class for item information from FFACE
		/// </summary>
		public class ItemTools {
			#region Constants

			/// <summary>
			/// Argument exception for inventory item calls
			/// </summary>
			private const string INVENTORY_RANGE = "Index must be between 0 and 80";
			/// <summary>
			/// Argument exception for non-inventory item calls (satchel/sack/etc)
			/// </summary>
			private const string OTHERBAG_RANGE = "Index must be between 1 and 80";

			#endregion

			#region Classes

			public class TreasureItem {
				#region Members
				public byte Flag { get; set; } //3=no item, 2=item	
				public short ItemID { get; set; }
				public byte Count { get; set; }
				public TreasureStatus Status { get; set; }
				public short MyLot { get; set; }
				public short WinLot { get; set; }
				public int WinPlayerSrvID { get; set; }
				public int WinPlayerID { get; set; }
				public int TimeStamp { get; set; } //utc timestamp
				public bool IsWon
				{
					get { return ((Flag != 3) && (WinPlayerID == PlayerID) && (MyLot >= WinLot)); }
				}
				#endregion

				/// <summary>
				/// Interal PlayerID for checking whether or not we won an item.
				/// </summary>
				private int PlayerID { get; set; }

				#region Constructors
				public TreasureItem(int playerid, short itemID, byte count, TreasureStatus status, short mylot, short winlot, int winplayersrvid, int winplayerid, int timestamp, byte flag)
				{
					Flag = flag;
					ItemID = itemID;
					Count = count;
					Status = status;
					MyLot = mylot;
					WinLot = winlot;
					WinPlayerSrvID = winplayersrvid;
					WinPlayerID = WinPlayerID;
					TimeStamp = timestamp;
					PlayerID = playerid;
				}
				#endregion

			}

			/// <summary>
			/// Class container for Inventory Items
			/// </summary>
			public class InventoryItem {
				#region Constructors

				public InventoryItem(ushort iD, byte index, uint count, uint flag, uint price, ushort extra, InventoryType loc)
				{
					ID = iD;
					Index = index;
					Count = count;
					Flag = flag;
					Price = price;
					Extra = extra;
					Location = loc;
				}

				public InventoryItem(ushort iD, byte index, uint count)
				{
					ID = iD;
					Index = index;
					Count = count;
				}

				#endregion

				#region Members

				/// <summary>
				/// ID of the item
				/// </summary>
				public ushort ID { get; set; }

				/// <summary>
				/// Index of the item
				/// </summary>
				public byte Index { get; set; }

				/// <summary>
				/// Count of the item
				/// </summary>
				public uint Count { get; set; }

				/// <summary>
				/// Flags on the item
				/// </summary>
				public uint Flag { get; set; }

				/// <summary>
				/// Price of the item
				/// </summary>
				public uint Price { get; set; }

				/// <summary>
				/// Extra information about the item
				/// </summary>
				public ushort Extra { get; set; }

				/// <summary>
				/// Location item was in.
				/// </summary>
				public InventoryType Location { get; set; }
				#endregion

			} // @ public class InventoryItem

			#endregion

			#region Constructor

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="instanceID">Instance ID generated by FFACE</param>
			public ItemTools(FFACE fface)// int instanceID)
			{
				_FFACE = fface;
				_InstanceID = fface._InstanceID;
			} // @ public InventoryWrapper(int instanceID)

			#endregion

			#region Members

			/// <summary>
			/// Our link to the Player Tools
			/// </summary>
			private FFACE _FFACE { get; set; }

			/// <summary>
			/// Instance ID generated by FFACE
			/// </summary>
			private int _InstanceID { get; set; }

			/// <summary>
			/// Player's Current Gil
			/// </summary>
			public uint CurrentGil
			{
				get { return GetInventoryItem(0).Count; }
			}

			/// <summary>
			/// Maximum amount of inventory slots player has
			/// </summary>
			public short InventoryMax
			{
				get { return GetInventoryMax(_InstanceID); }

			} // @ public short Max

			/// <summary>
			/// Maximum amount of safe slots player has
			/// </summary>
			public short SafeMax
			{
				get { return GetSafeMax(_InstanceID); }

			} // @ public short SafeMax

			/// <summary>
			/// Maximum amount of storage slots player has
			/// </summary>
			public short StorageMax
			{
				get { return GetStorageMax(_InstanceID); }

			} // @ public short StorageMax

			/// <summary>
			/// Maximum amount of temporary slots player has
			/// </summary>
			public short TemporaryMax
			{
				get { return GetTempMax(_InstanceID); }

			} // @ public short TemporaryMax

			/// <summary>
			/// Maximum amount of locker slots player has
			/// </summary>
			public short LockerMax
			{
				get { return GetLockerMax(_InstanceID); }

			} // @ public short LockerMax

			/// <summary>
			/// Maximum amount of satchel slots player has
			/// </summary>
			public short SatchelMax
			{
				get { return GetSatchelMax(_InstanceID); }

			} // @ public short SatchelMax

			/// <summary>
			/// Maximum amount of sack slots player has
			/// </summary>
			public short SackMax
			{
				get { return GetSackMax(_InstanceID); }

			} // @ public short SatchelMax

			/// <summary>
			/// Inventory slots in use, returns -1 on error
			/// </summary>
			public short InventoryCount
			{
				get
				{
					// calculate amount of slots are not empty
					// -1 for error (loading/zoning), count Gil too because of this.
					short count = -1;

					for (ushort i = 0; i <= 80; i++)
					{
						if (GetInventoryItem(i) != null)
						{
							count++;
						}
					}
					return count;
				}

			} // @ public short InventoryCount

			/// <summary>
			/// Sack slots in use, returns -1 on error
			/// </summary>
			public short SackCount
			{
				get
				{
					// calculate amount of slots are not empty
					// -1 for error (loading/zoning)
					short count = -1;

					for (byte i = 1; i <= 80; i++)
					{
						if (GetSackItem(i) != null)
						{
							count++;
						}
					}
					// since we found at least ONE item, we want to report count+1 (0 == 1 item)
					// since we started counting at item 1
					if (count != -1)
						count++;
					return count;
				}

			} // @ public short SackCount

			/// <summary>
			/// Locker slots in use, returns -1 on error
			/// </summary>
			public short LockerCount
			{
				get
				{
					// calculate amount of slots are not empty
					// -1 for error (loading/zoning)
					short count = -1;

					for (ushort i = 1; i <= 80; i++)
					{
						if (GetLockerItem(i) != null)
						{
							count++;
						}
					}
					// since we found at least ONE item, we want to report count+1 (0 == 1 item)
					// since we started counting at item 1
					if (count != -1)
						count++;
					return count;
				}

			} // @ public short LockerCount

			/// <summary>
			/// Safe slots in use, returns -1 on error
			/// </summary>
			public short SafeCount
			{
				get
				{
					// calculate amount of slots are not empty
					// -1 for error (loading/zoning)
					short count = -1;

					for (byte i = 1; i <= 80; i++)
					{
						if (GetSafeItem(i) != null)
						{
							count++;
						}
					}
					// since we found at least ONE item, we want to report count+1 (0 == 1 item)
					// since we started counting at item 1
					if (count != -1)
						count++;
					return count;
				}

			} // @ public short SafeCount

			/// <summary>
			/// Storage slots in use, returns -1 on error
			/// </summary>
			public short StorageCount
			{
				get
				{
					// calculate amount of slots are not empty
					// -1 for error (loading/zoning)
					short count = -1;

					for (byte i = 1; i <= 80; i++)
					{
						if (GetStorageItem(i) != null)
						{
							count++;
						}
					}
					// since we found at least ONE item, we want to report count+1 (0 == 1 item)
					// since we started counting at item 1
					if (count != -1)
						count++;
					return count;
				}

			} // @ public short StorageCount

			/// <summary>
			/// Temporary slots in use, returns -1 on error
			/// </summary>
			public short TemporaryCount
			{
				get
				{
					// calculate amount of slots are not empty
					// -1 for error (loading/zoning)
					short count = -1;

					for (byte i = 1; i <= 80; i++)
					{
						if (GetTempItem(i) != null)
						{
							count++;
						}
					}
					// since we found at least ONE item, we want to report count+1 (0 == 1 item)
					// since we started counting at item 1
					if (count != -1)
						count++;
					return count;
				}

			} // @ public short TemporaryCount

			/// <summary>
			/// Satchel slots in use, returns -1 on error
			/// </summary>
			public short SatchelCount
			{
				get
				{
					// calculate amount of slots are not empty
					// -1 for error (loading/zoning)
					short count = -1;

					for (byte i = 1; i <= 80; i++)
					{
						if (GetSatchelItem(i) != null)
						{
							count++;
						}
					}
					// since we found at least ONE item, we want to report count+1 (0 == 1 item)
					// since we started counting at item 1
					if (count != -1)
						count++;
					return count;
				}

			} // @ public short SatchelCount

			/// <summary>
			/// Name of the selected item
			/// </summary>
			public string SelectedItemName
			{
				get
				{
					if (_FFACE.Menu.IsOpen)
					{
						String s = FFACE.ParseResources.GetItemName(SelectedItemID);
						if (String.IsNullOrEmpty(s))
						{

							// get the string from FFACE
							int size = 20;
							byte[] buffer = new byte[20];
							GetSelectedItemName(_InstanceID, buffer, ref size);

							// convert to a string
							return System.Text.Encoding.GetEncoding(1252).GetString(buffer, 0, size - 1);

						}
					}
					return String.Empty;
				}
			}// @ public string SelectedItemName

			/// <summary>
			/// Index of the selected item
			/// </summary>
			public short SelectedItemIndex
			{
				get { return GetSelectedItemIndex(_InstanceID); }

			} // @ public short SelectedItemIndex

			///<summary>
			/// Id of the selected item
			///</summary>
			public int SelectedItemID
			{
				get
				{
					InventoryType it = InventoryType.None;
					if (_FFACE.Menu.IsOpen)
					{
						String selection = _FFACE.Menu.Selection;

						if (selection.Contains("Satchel"))
							it = InventoryType.Satchel;
						else if (selection.Contains("Mog Sack"))
							it = InventoryType.Sack;
						else if (selection.Contains("Mog Safe"))
							it = InventoryType.Safe;
						else if (selection.Contains("Locker"))
							it = InventoryType.Locker;
						else if (selection.Contains("Storage"))
							it = InventoryType.Storage;
						else if (selection.Contains("Temp"))
							it = InventoryType.Temp;
						//else if (selection.Contains("Equipment"))
						//    it = InventoryType.Inventory;
						else
							it = InventoryType.Inventory;

						byte index = GetSelectedItemIndex(_InstanceID);
						if ((it == InventoryType.Inventory) && ((index < 0) || (index > 80)))
							return 0;
						else if (index < 1 || index > 80)
							return 0;
						return GetItemIDByIndex(index, it);
					}
					else
						return 0;
				}
			}// @ public int SelectedItemID

			/// <summary>
			/// Place in inventory (1 -> max inventory)
			/// </summary>
			public int SelectedItemNum
			{
				get { return GetSelectedItemNum(_InstanceID); }

			} // @ public short SelectedItemNum

			#endregion

			#region Methods

			#region Methods replacing old Get*ItemCountByIndex, Get*ItemIDByIndex, Get*ItemCount, Get*Item

			//public delegate InventoryItem GetItem(byte index, InventoryType location);

			private void DoRangeChecks(int index, InventoryType location)
			{
				if (IsSet(location, InventoryType.None))
					throw new ArgumentOutOfRangeException("Location was set to InventoryType.None");
				// 0 is gil, rest is 1-80
				if (IsSet(location, InventoryType.Inventory) && ((index < 0) || (index > 80)))
					throw new ArgumentOutOfRangeException(INVENTORY_RANGE);
				else if (!IsSet(location, InventoryType.Inventory) && ((index < 1) || (index > 80)))
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);
			}

			public bool LocationHas(ushort ID, InventoryType location)
			{
				for (int i = 0; i < 80; i++)
				{
					if ((i == 0) && !IsSet(location, InventoryType.Inventory))
						continue;
					InventoryItem item = GetItem(i, location);
					if (item != null && item.ID == ID)
						return true;
				}
				return false;
			}

			/// <summary>
			/// Gets the first index taken by item with matching ID
			/// </summary>
			/// <param name="ID">ID of item to find</param>
			/// <param name="location">location indicated by InventoryType (Works best with one location at a time)</param>
			/// <returns>First Index taken by item with matching ID, 0 on error</returns>
			public int GetFirstIndexByItemID(int ID, InventoryType location)
			{
				if (ID <= 0)
					return 0;

				if (IsSet(location, InventoryType.None))
					return 0;

				System.Collections.Generic.List<InventoryItem> list = GetItemList(ID, location);

				if (list.Count <= 0)
					return 0;

				return list[0].Index;
			}

			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			/// <param name="location">LocationType of the location to search (SINGLE LOCATION)</param>
			/// <returns>Number of items at index passed on success, 0 on error</returns>
			public uint GetItemCountByIndex(int index, InventoryType location)
			{
				if (IsSet(location, InventoryType.None))
					return 0;

				DoRangeChecks(index, location);
				InventoryItem item = GetItem(index, location);
				if (item != null)
					return item.Count;
				return 0;
			} // @ public uint GetItemCountByIndex(byte index, InventoryType location)

			/// <summary>
			/// Will get an item ID by index in the passed location
			/// </summary>
			/// <param name="index">Index of the item in your inventory</param>
			/// <param name="location">InventoryType of the location to search (SINGLE LOCATION)</param>
			public int GetItemIDByIndex(int index, InventoryType location)
			{
				if (IsSet(location, InventoryType.None))
					return 0;

				DoRangeChecks(index, location);
				InventoryItem item = GetItem(index, location);
				if (item != null)
					return item.ID;
				return 0;
			} // @ public int GetItemIDByIndex(byte index, InventoryType location)

			/// <summary>
			/// The amount of items matching iD that you have in your inventory
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			/// <param name="location">InventoryType of the location(s) to search.</param>
			/// <returns>Total count of all items matching ID in the location(s) passed.</returns>
			public uint GetItemCount(int iD, InventoryType location)
			{
				// Don't even bother to do range checks here.
				// We're doing the looping ourselves.
				// DoRangeChecks(1, location);

				// Check to ensure iD passed isn't 0
				if (iD <= 0)
					return 0;

				if (IsSet(location, InventoryType.None))
					return 0;

				System.Collections.Generic.List<InventoryItem> list = GetItemList(iD, location);

				if (list.Count <= 0)
					return 0;

				uint count = 0;
				for (int i = 0; i < list.Count; i++)
				{
					count += list[i].Count;
				}
				return count;
			} // @ public uint GetItemCount(ushort iD, InventoryType location)

			/// <summary>
			/// Used internally for assigning a function to a variable.
			/// </summary>
			/// <param name="instanceID">InstanceID of FFACE for FFACE functions.</param>
			/// <param name="index">index into the particular location</param>
			/// <returns>Private INVENTORYITEM struct used internally for sending item data to public accessibility.</returns>
			private delegate INVENTORYITEM FFACEGetItem(int instanceID, int index);

			/// <summary>
			/// Gets information about an item from your inventory
			/// </summary>
			/// <param name="index">Index of the item</param>
			/// <param name="location">Location to check (SINGLE LOCATION)</param>
			/// <returns>InventoryItem filled with data about item, null if no item or error.</returns>
			public InventoryItem GetItem(int index, InventoryType location)
			{
				if (IsSet(location, InventoryType.None))
					return null;

				DoRangeChecks(index, location);

				// done this way because INVENTORYITEM is a private structure
				FFACEGetItem func = null;

				if (IsSet(location, InventoryType.Inventory))
				{
					func = FFACE.GetInventoryItem;
					location = InventoryType.Inventory;
				}
				else if (IsSet(location, InventoryType.Locker))
				{
					func = FFACE.GetLockerItem;
					location = InventoryType.Locker;
				}
				else if (IsSet(location, InventoryType.Sack))
				{
					func = FFACE.GetSackItem;
					location = InventoryType.Sack;
				}
				else if (IsSet(location, InventoryType.Safe))
				{
					func = FFACE.GetSafeItem;
					location = InventoryType.Safe;
				}
				else if (IsSet(location, InventoryType.Satchel))
				{
					func = FFACE.GetSatchelItem;
					location = InventoryType.Satchel;
				}
				else if (IsSet(location, InventoryType.Storage))
				{
					func = FFACE.GetStorageItem;
					location = InventoryType.Storage;
				}
				else if (IsSet(location, InventoryType.Temp))
				{
					func = FFACE.GetTempItem;
					location = InventoryType.Temp;
				}

				if (func == null)
					return null;

				INVENTORYITEM item = func(_InstanceID, index);

				if (item.ID <= 0)
				{
					return null;
				}

				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, location);

			} // @ public InventoryItem GetItem(int index, InventoryType location)

			/// <summary>
			/// Returns a list of all inventory items matching ID
			/// </summary>
			/// <param name="iD">ID of item to get list of.</param>
			/// <returns></returns>
			public System.Collections.Generic.List<InventoryItem> GetItemList(int iD, InventoryType location)
			{
				System.Collections.Generic.List<InventoryItem> retList = new System.Collections.Generic.List<InventoryItem>();
				if (IsSet(location, InventoryType.None))
					return retList;

				if (location != InventoryType.None)
				{
					InventoryItem item = null;
					int start = 0;

					bool	inventory = IsSet(location, InventoryType.Inventory),
							locker = IsSet(location, InventoryType.Locker),
							sack = IsSet(location, InventoryType.Sack),
							safe = IsSet(location, InventoryType.Safe),
							satchel = IsSet(location, InventoryType.Satchel),
							storage = IsSet(location, InventoryType.Storage),
							temp = IsSet(location, InventoryType.Temp);

					// if inventory is not set at all, just skip the 0
					if (!inventory)
						start = 1;

					// Check all bags in parallel
					for (int i = start; i <= 80; i++)
					{
						// inventory index is allowed to be 0
						if (inventory)
						{
							item = GetItem(i, InventoryType.Inventory);

							if ((item != null) && (item.ID == iD))
								retList.Add(item);
						}
						// Inventory is only one allowed to start at 0
						if (i == 0)
							continue;

						if (locker)
						{
							item = GetItem(i, InventoryType.Locker);
							if ((item != null) && (item.ID == iD))
								retList.Add(item);
						}
						if (sack)
						{
							item = GetItem(i, InventoryType.Sack);
							if ((item != null) && (item.ID == iD))
								retList.Add(item);
						}
						if (safe)
						{
							item = GetItem(i, InventoryType.Safe);
							if ((item != null) && (item.ID == iD))
								retList.Add(item);
						}
						if (satchel)
						{
							item = GetItem(i, InventoryType.Satchel);
							if ((item != null) && (item.ID == iD))
								retList.Add(item);
						}
						if (storage)
						{
							item = GetItem(i, InventoryType.Storage);
							if ((item != null) && (item.ID == iD))
								retList.Add(item);
						}
						if (temp)
						{
							item = GetItem(i, InventoryType.Temp);
							if ((item != null) && (item.ID == iD))
								retList.Add(item);
						}
					}
				}

				return retList;
			}

			#endregion

			#region Methods relating to inventory

			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetInventoryItemCountByIndex(int index)
			{
				return GetItemCountByIndex(index, InventoryType.Inventory);
				//// 0 is gil, rest is 1-80
				//if (0 > index || 80 < index)
				//    throw new ArgumentOutOfRangeException(INVENTORY_RANGE);

				//return GetInventoryItem(index).Count;
			}

			/// <summary>
			/// Will get an inventory item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your inventory</param>
			public int GetInventoryItemIDByIndex(byte index)
			{
				if (0 > index || 80 < index)
					throw new ArgumentOutOfRangeException(INVENTORY_RANGE);
				return GetItemIDByIndex(index, InventoryType.Inventory);

				//return GetInventoryItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of items matching iD that you have in your inventory
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetInventoryItemCount(ushort iD)
			{
				return GetItemCount(iD, InventoryType.Inventory);
				//uint count = 0;

				//for (byte i = 0; i <= 80; i++)
				//{
				//    InventoryItem item = GetInventoryItem(i);

				//    if (item.ID == iD)
				//        count += item.Count;

				//} // @ for (short i = 0; i < 80; i++)

				//return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your inventory
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetInventoryItem(int index)
			{
				if (0 > index || 80 < index)
					throw new ArgumentOutOfRangeException(INVENTORY_RANGE);

				return GetItem(index, InventoryType.Inventory);
				//// done this way because INVENTORYITEM is a private structure
				//INVENTORYITEM item = FFACE.GetInventoryItem(_InstanceID, index);
				//return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Inventory);

			} // @ public INVENTORYITEM GetInventoryItem(int index)

			#endregion

			#region Methods relating to Safe
			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetSafeItemCountByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemCountByIndex(index, InventoryType.Safe);
				//return GetSafeItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Safe item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Safe</param>
			public int GetSafeItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemIDByIndex(index, InventoryType.Safe);
				//return GetSafeItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your safe
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetSafeItemCount(ushort iD)
			{
				return GetItemCount(iD, InventoryType.Safe);
				//uint count = 0;

				//for (byte i = 1; i <= 80; i++)
				//{
				//    InventoryItem item = GetSafeItem(i);

				//    if (item.ID == iD)
				//        count += item.Count;

				//} // @ for (short i = 0; i < 80; i++)

				//return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your safe
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetSafeItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItem(index, InventoryType.Safe);
				//// done this way because INVENTORYITEM is a private structure
				//INVENTORYITEM item = FFACE.GetSafeItem(_InstanceID, (byte)index);
				//return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Safe);

			} // @ public INVENTORYITEM GetSafeItem(byte index)
			#endregion

			#region Methods relating to storage
			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetStorageItemCountByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemCountByIndex(index, InventoryType.Storage);
				//GetStorageItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Storage item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Storage</param>
			public int GetStorageItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemIDByIndex(index, InventoryType.Storage);
				//GetStorageItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Storage
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetStorageItemCount(ushort iD)
			{
				return GetItemCount(iD, InventoryType.Storage);
				//uint count = 0;

				//for (byte i = 1; i <= 80; i++)
				//{
				//    InventoryItem item = GetStorageItem(i);

				//    if (item.ID == iD)
				//        count += item.Count;

				//} // @ for (short i = 0; i < 80; i++)

				//return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your storage
			/// </summary>
			/// <param name="index">Index of teh item</param>
			public InventoryItem GetStorageItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItem(index, InventoryType.Storage);
				//// done this way because INVENTORYITEM is a private structure
				//INVENTORYITEM item = FFACE.GetStorageItem(_InstanceID, index);
				//return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Storage);

			} // @ public INVENTORYITEM GetStorageItem(int index)
			#endregion

			#region Methods relating to Locker
			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetLockerItemCountByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);
				return GetItemCountByIndex(index, InventoryType.Locker);
				//GetLockerItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Locker item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Locker</param>
			public int GetLockerItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemIDByIndex(index, InventoryType.Locker);
				//return GetLockerItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Locker
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetLockerItemCount(ushort iD)
			{
				return GetItemCount(iD, InventoryType.Locker);
				//uint count = 0;

				//for (byte i = 1; i <= 80; i++)
				//{
				//    InventoryItem item = GetLockerItem(i);

				//    if (item.ID == iD)
				//        count += item.Count;

				//} // @ for (short i = 0; i < 80; i++)

				//return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your locker
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetLockerItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItem(index, InventoryType.Locker);
				//// done this way because INVENTORYITEM is a private structure
				//INVENTORYITEM item = FFACE.GetLockerItem(_InstanceID, (byte)index);
				//return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Locker);

			} // @ public InventoryItem GetLockerItem(ushort index)
			#endregion

			#region Methods relating to Temporary Storage
			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetTempItemCountByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemCountByIndex(index, InventoryType.Temp);
				//return GetTempItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Temp item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Temp</param>
			public int GetTempItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemIDByIndex(index, InventoryType.Temp);
				//return GetTempItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Temp Storage
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetTempItemCount(ushort iD)
			{
				return GetItemCount(iD, InventoryType.Temp);
				//uint count = 0;

				//for (byte i = 1; i <= 80; i++)
				//{
				//    InventoryItem item = GetTempItem(i);

				//    if (item.ID == iD)
				//        count += item.Count;

				//} // @ for (short i = 0; i < 80; i++)

				//return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about a temporary item
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetTempItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);


				return GetItem(index, InventoryType.Temp);
				//// done this way because INVENTORYITEM is a private structure
				//INVENTORYITEM item = FFACE.GetTempItem(_InstanceID, (byte)index);
				//return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Temp);

			} // @ public InventoryItem GetTempItem(ushort index)
			#endregion

			#region Methods relating to Satchel
			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetSatchelItemCountByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);
				return GetItemCountByIndex(index, InventoryType.Satchel);
				//return GetSatchelItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Satchel item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Satchel</param>
			public int GetSatchelItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemIDByIndex(index, InventoryType.Satchel);
				//return GetSatchelItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Satchel
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetSatchelItemCount(ushort iD)
			{
				return GetItemCount(iD, InventoryType.Satchel);
				//uint count = 0;

				//for (byte i = 1; i <= 80; i++)
				//{
				//    InventoryItem item = GetSatchelItem(i);

				//    if (item.ID == iD)
				//        count += item.Count;

				//} // @ for (short i = 0; i < 80; i++)

				//return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your Satchel
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetSatchelItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItem(index, InventoryType.Satchel);
				//// done this way because INVENTORYITEM is a private structure
				//INVENTORYITEM item = FFACE.GetSatchelItem(_InstanceID, (byte)index);
				//return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Satchel);

			} // @ public InventoryItem GetSatchelItem(ushort index)
			#endregion

			#region Methods relating to Sack
			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetSackItemCountByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);
				return GetItemCountByIndex(index, InventoryType.Sack);
				//return GetSackItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Sack item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Sack</param>
			public int GetSackItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItemIDByIndex(index, InventoryType.Sack);
				//return GetSackItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Satchel
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetSackItemCount(ushort iD)
			{
				return GetItemCount(iD, InventoryType.Sack);
				//uint count = 0;

				//for (byte i = 1; i <= 80; i++)
				//{
				//    InventoryItem item = GetSackItem(i);

				//    if (item.ID == iD)
				//        count += item.Count;

				//} // @ for (short i = 0; i < 80; i++)

				//return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your Sack
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetSackItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetItem(index, InventoryType.Sack);
				//// done this way because INVENTORYITEM is a private structure
				//INVENTORYITEM item = FFACE.GetSackItem(_InstanceID, index);
				//return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Sack);

			} // @ public InventoryItem GetSatchelItem(ushort index)
			#endregion

			#region Methods relating to Equipment

			/// <summary>
			/// The count of an item in a specific equipment slot
			/// </summary>
			/// <param name="slot">Slot to count</param>
			public uint GetEquippedItemCount(EquipSlot slot)
			{
				byte index = GetEquippedItemIndex(slot);
				if (index != 0)
					return GetItemCountByIndex(index, InventoryType.Inventory);// GetInventoryItem(index).Count;
				return 0;
			} // @ public byte GetEquippedItemCount(EquipSlot slot)

			/// <summary>
			/// Gets the item id for passed equipment slot
			/// </summary>
			/// <param name="slot">Slot to get the ID for</param>
			public int GetEquippedItemID(EquipSlot slot)
			{
				byte index = GetEquippedItemIndex(slot);
				if (index != 0)
					return GetItemIDByIndex(index, InventoryType.Inventory);// GetInventoryItem(index).ID;
				return 0;
			} // @ public short GetEquippedItemID(EquipSlot slot)

			/// <summary>
			/// Gets the index of an item for the passed equipment slot
			/// </summary>
			/// <param name="slot">Slot to get the index for</param>
			public byte GetEquippedItemIndex(EquipSlot slot)
			{
				return FFACE.GetEquippedItemIndex(_InstanceID, slot);
			} // @ public byte GetEquippedItemIndex(EquipSlot slot)

			#endregion

			#region Methods relating to Treasure Pool

			/// <summary>
			/// Gets information about an item currently in the treasure pool
			/// </summary>
			/// <param name="index">Index of the treasure item</param>
			/// <returns>null if index is not withing range (0-9) or no item, populated TreasureItem class otherwise.</returns>
			public TreasureItem GetTreasureItem(int index)
			{
				if (index < 0 || index > 9)
					return null;

				TREASUREITEM item = FFACE.GetTreasureItem(_InstanceID, (byte)index);
				if ((item.ItemID <= 0) || (item.Flag == 3))
					return null;
				return new TreasureItem(_FFACE.Player.ID,
							item.ItemID, item.Count, item.Status, 
							item.MyLot, item.WinLot, item.WinPlayerSrvID, 
							item.WinPlayerID, item.TimeStamp, item.Flag
							);
			} // @ public TreasureItem GetTrasureItem(byte index)

			#endregion

			#endregion

		} // @ class ItemTools
	} // @ public partial class FFACE
}

