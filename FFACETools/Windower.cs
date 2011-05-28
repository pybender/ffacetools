﻿using System;
using System.Collections.Generic;

namespace FFACETools {
	public partial class FFACE {
		/// <summary>
		/// Wrapper class for sending Windower commands through FFACE
		/// </summary>
		public class WindowerTools {
			#region Classes

			/// <summary>
			/// Text object to display in FFXi
			/// </summary>
			public class TextObject {
				#region Constants

				/// <summary>
				/// Constant to show required name field for creating a text object
				/// </summary>
				private const string MISSING_NAME = "Name is required for create a text object";

				#endregion

				#region enums

				/// <summary>
				/// Color parameters
				/// </summary>
				private enum Colors {
					Blue,
					Red,
					Green,
					Transparent

				} // @ private enum Colors

				#endregion

				#region Constructor / Destructor

				/// <summary>
				/// Constructor
				/// </summary>
				/// <param name="instanceID">Instance ID generated by FFACE</param>
				public TextObject(int instanceID)
				{
					_InstanceID = instanceID;

				} // @ public TextObject(int instanceID)

				~TextObject()
				{
					if (_Instantiated)
						DeleteTextObject();
				}

				#endregion

				#region Members

				/// <summary>
				/// Instance ID generated by FFACE
				/// </summary>
				private int _InstanceID { get; set; }

				/// <summary>
				/// Whether we have created a text object and sent to windower
				/// </summary>
				private bool _Instantiated { get; set; }

				/// <summary>
				/// Name (ID) of the text object
				/// </summary>
				public string Name { get; set; }

				/// <summary>
				/// Text to show
				/// </summary>
				public string Text { get; set; }

				/// <summary>
				/// Size of the background border in pixels
				/// </summary>
				public float BGBorderSize { get; set; }

				/// <summary>
				/// If the font is shown or not
				/// </summary>
				public bool FontVisible { get; set; }

				/// <summary>
				/// if the background is shown or not
				/// </summary>
				public bool BGVisible { get; set; }

				/// <summary>
				/// Whether to include the background colors
				/// </summary>
				public bool IncludeBackGround { get; set; }

				/// <summary>
				/// If the font should be in bold
				/// </summary>
				private bool FontBold { get; set; }

				/// <summary>
				/// If the font should be in italic
				/// </summary>
				private bool FontItalic { get; set; }

				/// <summary>
				/// If the found should be right justified
				/// </summary>
				private bool FontRightJustified { get; set; }

				/// <summary>
				/// Color of the font
				/// </summary>
				private byte[] _FontColor = new byte[4];

				/// <summary>
				/// Color of the background
				/// </summary>
				private byte[] _BGColor = new byte[4];

				/// <summary>
				/// Location of the text object
				/// </summary>
				private float[] _Location = new float[2];

				/// <summary>
				/// Type of font
				/// </summary>
				private string _FontType { get; set; }

				/// <summary>
				/// Size of font
				/// </summary>
				private short _FontHeight { get; set; }

				#endregion

				#region Methods

				/// <summary>
				/// Generates a parameter error message based on color
				/// </summary>
				/// <param name="color">Which parameter has an error</param>
				private string GetExceptionMessage(Colors color)
				{
					string errorMessage = "Parameter ";
					switch (color)
					{
						case Colors.Blue:
							errorMessage += "Blue";
							break;
						case Colors.Green:
							errorMessage += "Green";
							break;
						case Colors.Red:
							errorMessage += "Red";
							break;
						case Colors.Transparent:
							errorMessage += "Transparent";
							break;

					} // @ switch (color)

					return errorMessage += " must be between 0 and 255";

				} // @ private string GetExceptionMessage(Colors color)

				/// <summary>
				/// Sets the Color of the font
				/// </summary>
				/// <param name="transparent">Transparency level (0-255)</param>
				/// <param name="red">Red depth (0-255)</param>
				/// <param name="green">Green depth (0-255)</param>
				/// <param name="blue">Blue depth (0-255)</param>
				public void SetFontColor(byte transparent, byte red, byte green, byte blue)
				{

					// make sure we're not out of range
					if (0 > blue || 255 < blue)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Blue));
					if (0 > red || 255 < red)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Red));
					if (0 > green || 255 < green)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Green));
					if (0 > transparent || 255 < transparent)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Transparent));

					_FontColor[0] = transparent;
					_FontColor[1] = red;
					_FontColor[2] = green;
					_FontColor[3] = blue;

				} // @ public void Color(byte Transparent, byte Red, byte Green, byte Blue)

				/// <summary>
				/// Sets the Color of the font
				/// </summary>
				/// <param name="transparent">Transparency level (0-255)</param>
				/// <param name="red">Red depth (0-255)</param>
				/// <param name="green">Green depth (0-255)</param>
				/// <param name="blue">Blue depth (0-255)</param>
				/// <param name="visible">If the font is visible or not</param>
				/// <param name="bold">If the font should be in bold</param>
				/// <param name="italic">If the font should be in italic</param>
				/// <param name="rightJustified">If the font should be right justified</param>
				public void SetFontColor(byte transparent, byte red, byte green, byte blue,
										 bool visible, bool bold, bool italic, bool rightJustified)
				{

					// make sure we're not out of range
					if (0 > blue || 255 < blue)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Blue));
					if (0 > red || 255 < red)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Red));
					if (0 > green || 255 < green)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Green));
					if (0 > transparent || 255 < transparent)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Transparent));

					_FontColor[0] = transparent;
					_FontColor[1] = red;
					_FontColor[2] = green;
					_FontColor[3] = blue;

					FontBold = bold;
					FontItalic = italic;
					FontRightJustified = rightJustified;

				} // @ public void Color(byte Transparent, byte Red, byte Green, byte Blue)

				/// <summary>
				/// Set the background color of the text
				/// </summary>
				/// <param name="transparent">Transparency level (0-255)</param>
				/// <param name="red">Red depth (0-255)</param>
				/// <param name="green">Green depth (0-255)</param>
				/// <param name="blue">Blue depth (0-255)</param>
				public void SetBGColor(byte transparent, byte red, byte green, byte blue)
				{
					// make sure we're not out of range
					if (0 > blue || 255 < blue)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Blue));
					if (0 > red || 255 < red)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Red));
					if (0 > green || 255 < green)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Green));
					if (0 > transparent || 255 < transparent)
						throw new ArgumentOutOfRangeException(GetExceptionMessage(Colors.Transparent));

					_BGColor[0] = transparent;
					_BGColor[1] = red;
					_BGColor[2] = green;
					_BGColor[3] = blue;

				} // @ public void BGColor(byte Transparent, byte Red, byte Green, byte Blue)

				/// <summary>
				/// Set the location of the text
				/// </summary>
				/// <param name="Horizontal">Horizontal co-ord</param>
				/// <param name="Vertical">Vertical co-ord</param>
				public void SetLocation(float horizontal, float vertical)
				{
					_Location[0] = horizontal;
					_Location[1] = vertical;

				} // @ public void Location(float Horizontal, float Vertical)

				/// <summary>
				/// Sets the font type and height
				/// </summary>
				/// <param name="type">Type of font</param>
				/// <param name="height">Height of font</param>
				public void SetFont(string type, short height)
				{
					_FontType = type;
					_FontHeight = height;

				} // @ public void SetFont(byte font, short height)

				/// <summary>
				/// Will render the Text Object
				/// </summary>
				public void RenderTextObject()
				{
					// make sure we have a name
					if (!String.IsNullOrEmpty(Name))
					{
						CTHCreateTextObject(_InstanceID, Name);
						CTHSetText(_InstanceID, Name, Text);
						CTHSetColor(_InstanceID, Name, _FontColor[0], _FontColor[1], _FontColor[2], _FontColor[3]);
						CTHSetLocation(_InstanceID, Name, _Location[0], _Location[1]);
						CTHSetVisibility(_InstanceID, Name, FontVisible);
						CTHSetRightJustified(_InstanceID, Name, FontRightJustified);
						CTHSetBold(_InstanceID, Name, FontBold);
						CTHSetItalic(_InstanceID, Name, FontItalic);

						if (!String.IsNullOrEmpty(_FontType))
							CTHSetFont(_InstanceID, Name, _FontType, _FontHeight);

						if (IncludeBackGround)
						{
							CTHSetBGColor(_InstanceID, Name, _BGColor[0], _BGColor[1], _BGColor[2], _BGColor[3]);
							CTHSetBGVisibility(_InstanceID, Name, BGVisible);
							CTHSetBGBorderSize(_InstanceID, Name, BGBorderSize);
						}

						CTHFlushCommands(_InstanceID);

						_Instantiated = true;

					} // @ if (!Name.Equals(""))
					else
						throw new MissingFieldException(MISSING_NAME);

				} // @ public void CreateTextObject()

				/// <summary>
				/// Will remove the text object
				/// </summary>
				public void DeleteTextObject()
				{
					CTHDeleteTextObject(_InstanceID, Name);
					CTHFlushCommands(_InstanceID);

					_Instantiated = false;

				} // @ public void DeleteTextObject()

				/// <summary>
				/// Will update the text object after changes
				/// </summary>
				public void UpdateTextObject()
				{
					CTHFlushCommands(_InstanceID);

				} // @ public void UpdateTextObject()

				/// <summary>
				/// Will update the text of the object
				/// </summary>
				public void UpdateTextObject(string sText)
				{
					CTHDeleteTextObject(_InstanceID, Name);
					CTHFlushCommands(_InstanceID);
					this.Text = sText;
					RenderTextObject();

				} // @ public void UpdateTextObject()

				#endregion

			} // @ public class TextObject

			#endregion

			#region Constructor

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="instanceID">Instance ID generated by FFACE</param>
			public WindowerTools(int instanceID)
			{
				_InstanceID = instanceID;

			} // @ public WindowerWrapper(int instanceID)

			#endregion

			#region Members

			/// <summary>
			/// Instance ID generated by FFACE
			/// </summary>
			private int _InstanceID { get; set; }

			#endregion

			#region Methods

			/// <summary>
			/// Sends a string to FFXi
			/// </summary>
			/// <param name="stringToSend">string to send</param>
			public void SendString(string stringToSend)
			{
				CKHSendString(_InstanceID, stringToSend);

			} // @ public void SendString(string stringToSend)

			/// <summary>
			/// Sends a key stroke status to FFXi
			/// </summary>
			/// <param name="key">Key to send</param>
			/// <param name="down">True if keypress is down, false for up</param>
			public void SendKey(KeyCode key, bool down)
			{
				CKHSetKey(_InstanceID, key, down);

			} // @ public void SendKey(KeyCode key, bool down)

			/// <summary>
			/// Sends a key press to FFXi (up, then down)
			/// </summary>
			/// <param name="key">Key to send</param>
			public void SendKeyPress(KeyCode key)
			{
				CKHSetKey(_InstanceID, key, true);
				System.Threading.Thread.Sleep(1);
				CKHSetKey(_InstanceID, key, false);

			} // @ public void SendKeyPress(KeyCode key)

			/// <summary>
			/// Will create a Windower text object
			/// </summary>
			public TextObject CreateTextObject()
			{
				return new TextObject(_InstanceID);

			} // @ public TextObject CreateTextObject()

			/// <summary>
			/// Whether to block input to ffxi
			/// </summary>
			/// <param name="block">True if blocking</param>
			public void BlockInput(bool block)
			{
				CKHBlockInput(_InstanceID, block);

			} // @ public BlockInput(bool block)

			/// <summary>
			/// Get the amount of arguments the windower console has
			/// </summary>
			/// <returns>Argument count in windower</returns>
			public short ArgumentCount()
			{
				return CCHGetArgCount(_InstanceID);

			} // @ public short ArgumentCount()

			/// <summary>
			/// Gets a specific argument from the windower console
			/// </summary>
			/// <param name="index">Index of the argument to get</param>
			public string GetArgument(short index)
			{
				string argument = String.Empty;
				CCHGetArg(_InstanceID, index, ref argument);

				return argument;

			} // @ public string GetArgument(short index)

			/// <summary>
			/// Time of the last command
			/// 
			/// NOTE: This may still be bugged (always returns true)
			///	   See Windower wiki/forums for details
			/// </summary>
			public int IsNewCommand()
			{
				return CCHIsNewCommand(_InstanceID);

			} // @ public bool IsNewCommand()

			#endregion

		} // @ public class WindowerTools
	} // @ public partial class FFACE
}
