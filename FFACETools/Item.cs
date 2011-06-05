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
			public ItemTools(int instanceID)
			{
				_InstanceID = instanceID;

			} // @ public InventoryWrapper(int instanceID)

			#endregion

			#region Members

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
						if (GetInventoryItem(i).ID > 0)
						{
							count++;
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
						if (GetSackItem(i).ID > 0)
						{
							count++;
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
						if (GetLockerItem(i).ID > 0)
						{
							count++;
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
						if (GetSafeItem(i).ID > 0)
						{
							count++;
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
						if (GetStorageItem(i).ID > 0)
						{
							count++;
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
						if (GetTempItem(i).ID > 0)
						{
							count++;
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
						if (GetSatchelItem(i).ID > 0)
						{
							count++;
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
					// get the string from FFACE
					int size = 20;
					byte[] buffer = new byte[20];
					GetSelectedItemName(_InstanceID, buffer, ref size);

					// convert to a string
					return System.Text.Encoding.GetEncoding(1252).GetString(buffer, 0, size - 1);
				}
			} // @ public string SelectedItemName

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
				get { return GetInventoryItemIDByIndex(GetSelectedItemIndex(_InstanceID)); }
			} // @ public int SelectedItemID

			/// <summary>
			/// Place in inventory (1 -> max inventory)
			/// </summary>
			public int SelectedItemNum
			{
				get { return GetSelectedItemNum(_InstanceID); }

			} // @ public short SelectedItemNum

			#endregion

			#region Methods

			#region Methods relating to inventory
			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			public uint GetInventoryItemCountByIndex(byte index)
			{
				// 0 is gil, rest is 1-80
				if (0 > index || 80 < index)
					throw new ArgumentOutOfRangeException(INVENTORY_RANGE);

				return GetInventoryItem(index).Count;
			}

			/// <summary>
			/// The count of an item by index
			/// </summary>
			/// <param name="index">Index of the item</param>
			[Obsolete("Use GetInventoryItemCountByIndex")]
			public uint GetItemCountByIndex(byte index)
			{
				return GetInventoryItemCountByIndex(index);

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get an inventory item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your inventory</param>
			public int GetInventoryItemIDByIndex(byte index)
			{
				if (0 > index || 80 < index)
					throw new ArgumentOutOfRangeException(INVENTORY_RANGE);

				return GetInventoryItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of items matching iD that you have in your inventory
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetInventoryItemCount(ushort iD)
			{
				uint count = 0;

				for (byte i = 0; i <= 80; i++)
				{
					InventoryItem item = GetInventoryItem(i);

					if (item.ID == iD)
						count += item.Count;

				} // @ for (short i = 0; i < 80; i++)

				return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your inventory
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetInventoryItem(int index)
			{
				if (0 > index || 80 < index)
					throw new ArgumentOutOfRangeException(INVENTORY_RANGE);

				// done this way because INVENTORYITEM is a private structure
				INVENTORYITEM item = FFACE.GetInventoryItem(_InstanceID, index);
				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Inventory);

			} // @ public INVENTORYITEM GetInventoryItem(int index)

			private bool IsSet(InventoryType val, InventoryType settings)
			{
				return (((uint)val & (uint)settings) != 0);
			}

			/// <summary>
			/// Returns a list of all inventory items matching ID
			/// </summary>
			/// <param name="iD">ID of item to get list of.</param>
			/// <returns></returns>
			public System.Collections.Generic.List<InventoryItem> GetItemList(int iD, InventoryType location)
			{
				System.Collections.Generic.List<InventoryItem> retList = new System.Collections.Generic.List<InventoryItem>();
				InventoryItem item;
				if (location != InventoryType.None)
				{
					for (byte i = 1; i <= 80; i++)
					{
						if (IsSet(location, InventoryType.Inventory))
						{
							item = GetInventoryItem(i);
							if (item.ID == iD)
								retList.Add(item);
						}
						if (IsSet(location, InventoryType.Safe))
						{
							item = GetSafeItem(i);
							if (item.ID == iD)
								retList.Add(item);
						}
						if (IsSet(location, InventoryType.Storage))
						{
							item = GetStorageItem(i);
							if (item.ID == iD)
								retList.Add(item);
						}
						if (IsSet(location, InventoryType.Locker))
						{
							item = GetLockerItem(i);
							if (item.ID == iD)
								retList.Add(item);
						}
						if (IsSet(location, InventoryType.Temp))
						{
							item = GetTempItem(i);
							if (item.ID == iD)
								retList.Add(item);
						}
						if (IsSet(location, InventoryType.Satchel))
						{
							item = GetSatchelItem(i);
							if (item.ID == iD)
								retList.Add(item);
						}
						if (IsSet(location, InventoryType.Sack))
						{
							item = GetSackItem(i);//GetInventoryItem(i);
							if (item.ID == iD)
								retList.Add(item);
						}
					}
				}

				return retList;
			}

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

				return GetSafeItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Safe item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Safe</param>
			public int GetSafeItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetSafeItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your safe
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetSafeItemCount(ushort iD)
			{
				uint count = 0;

				for (byte i = 1; i <= 80; i++)
				{
					InventoryItem item = GetSafeItem(i);

					if (item.ID == iD)
						count += item.Count;

				} // @ for (short i = 0; i < 80; i++)

				return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your safe
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetSafeItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				// done this way because INVENTORYITEM is a private structure
				INVENTORYITEM item = FFACE.GetSafeItem(_InstanceID, (byte)index);
				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Safe);

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

				return GetStorageItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Storage item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Storage</param>
			public int GetStorageItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetStorageItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Storage
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetStorageItemCount(ushort iD)
			{
				uint count = 0;

				for (byte i = 1; i <= 80; i++)
				{
					InventoryItem item = GetStorageItem(i);

					if (item.ID == iD)
						count += item.Count;

				} // @ for (short i = 0; i < 80; i++)

				return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your storage
			/// </summary>
			/// <param name="index">Index of teh item</param>
			public InventoryItem GetStorageItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				// done this way because INVENTORYITEM is a private structure
				INVENTORYITEM item = FFACE.GetStorageItem(_InstanceID, index);
				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Storage);

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
				return GetLockerItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Locker item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Locker</param>
			public int GetLockerItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetLockerItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Locker
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetLockerItemCount(ushort iD)
			{
				uint count = 0;

				for (byte i = 1; i <= 80; i++)
				{
					InventoryItem item = GetLockerItem(i);

					if (item.ID == iD)
						count += item.Count;

				} // @ for (short i = 0; i < 80; i++)

				return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your locker
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetLockerItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				// done this way because INVENTORYITEM is a private structure
				INVENTORYITEM item = FFACE.GetLockerItem(_InstanceID, (byte)index);
				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Locker);

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

				return GetTempItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Temp item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Temp</param>
			public int GetTempItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetTempItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Temp Storage
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetTempItemCount(ushort iD)
			{
				uint count = 0;

				for (byte i = 1; i <= 80; i++)
				{
					InventoryItem item = GetTempItem(i);

					if (item.ID == iD)
						count += item.Count;

				} // @ for (short i = 0; i < 80; i++)

				return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about a temporary item
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetTempItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				// done this way because INVENTORYITEM is a private structure
				INVENTORYITEM item = FFACE.GetTempItem(_InstanceID, (byte)index);
				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Temp);

			} // @ public InventoryItem GetLockerItem(ushort index)
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
				return GetSatchelItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Satchel item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Satchel</param>
			public int GetSatchelItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetSatchelItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Satchel
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetSatchelItemCount(ushort iD)
			{
				uint count = 0;

				for (byte i = 1; i <= 80; i++)
				{
					InventoryItem item = GetSatchelItem(i);

					if (item.ID == iD)
						count += item.Count;

				} // @ for (short i = 0; i < 80; i++)

				return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your Satchel
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetSatchelItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				// done this way because INVENTORYITEM is a private structure
				INVENTORYITEM item = FFACE.GetSatchelItem(_InstanceID, (byte)index);
				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Satchel);

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
				return GetSackItem(index).Count;

			} // @ public byte GetItemCountByIndex(byte index)

			/// <summary>
			/// Will get a Sack item ID by index
			/// </summary>
			/// <param name="index">Index of the item in your Sack</param>
			public int GetSackItemIDByIndex(byte index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				return GetSackItem(index).ID;

			} // @ public int GetItemIDByIndex(short index)

			/// <summary>
			/// The amount of passed items you have in your Satchel
			/// </summary>
			/// <param name="iD">ID of the item to count</param>
			public uint GetSackItemCount(ushort iD)
			{
				uint count = 0;

				for (byte i = 1; i <= 80; i++)
				{
					InventoryItem item = GetSackItem(i);

					if (item.ID == iD)
						count += item.Count;

				} // @ for (short i = 0; i < 80; i++)

				return count;

			} // @ public int ItemCount(short ID)

			/// <summary>
			/// Gets information about an item from your Sack
			/// </summary>
			/// <param name="index">Index of the item</param>
			public InventoryItem GetSackItem(int index)
			{
				if (1 > index || 80 < index)
					throw new ArgumentOutOfRangeException(OTHERBAG_RANGE);

				// done this way because INVENTORYITEM is a private structure
				INVENTORYITEM item = FFACE.GetSackItem(_InstanceID, index);
				return new InventoryItem(item.ID, item.Index, item.Count, item.Flag, item.Price, item.Extra, InventoryType.Sack);

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
					return GetInventoryItem(index).Count;
				else
					return 0;
			} // @ public byte GetEquippedItemCount(EquipSlot slot)

			/// <summary>
			/// Gets the item id for passed equipment slot
			/// </summary>
			/// <param name="slot">Slot to get the ID for</param>
			public ushort GetEquippedItemID(EquipSlot slot)
			{
				byte index = GetEquippedItemIndex(slot);
				if (index != 0)
					return GetInventoryItem(index).ID;
				else
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

			/// <summary>
			/// Gets information about an item currently in the treasure pool
			/// </summary>
			/// <param name="index">Index of the treasure item</param>
			public TREASUREITEM GetTreasureItem(int index)
			{
				return FFACE.GetTreasureItem(_InstanceID, (byte)index);

			} // @ public TREASUREITEM GetTrasureItem(byte index)

			#endregion

		} // @ class ItemTools
	} // @ public partial class FFACE
}

