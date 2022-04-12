//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, SYSLIB0011,SYSLIB0032
// dnlib: See LICENSE.txt for more info

using System;

namespace Datadog.Trace.Vendors.dnlib.DotNet.MD {
	/// <summary>
	/// MDStream flags
	/// </summary>
	[Flags]
	internal enum MDStreamFlags : byte {
		/// <summary>#Strings stream is big and requires 4 byte offsets</summary>
		BigStrings = 1,
		/// <summary>#GUID stream is big and requires 4 byte offsets</summary>
		BigGUID = 2,
		/// <summary>#Blob stream is big and requires 4 byte offsets</summary>
		BigBlob = 4,
		/// <summary/>
		Padding = 8,
		/// <summary/>
		DeltaOnly = 0x20,
		/// <summary>Extra data follows the row counts</summary>
		ExtraData = 0x40,
		/// <summary>Set if certain tables can contain deleted rows. The name column (if present) is set to "_Deleted"</summary>
		HasDelete = 0x80,
	}
}
