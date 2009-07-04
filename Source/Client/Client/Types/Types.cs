#region Copyright (c)2009 Hathi Project < http://hathi.sourceforge.net >
/*
* This file is part of Hathi Project
* Hathi Developers Team:
* andrewdev, beckman16, biskvit, elnomade_devel, ershyams, grefly, jpierce420, 
* knocte, kshah05, manudenfer, palutz, ramone_hamilton, soudamini, writetogupta
* 
* Hathi is a fork of lphant version 1.0 GPL
* lphant team
* Juanjo, 70n1, toertchn, FeuerFrei, mimontyf, finrold, jicxicmic, bladmorv, 
* andrerib, arcange|, montagu, wins, RangO, FAV, roytam1, Jesse
* 
* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
* 
* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
*/
#endregion

using System;

namespace eLePhant.Client
{
	/// <summary>
	/// Summary description for Types.
	/// </summary>
	public class InterfaceConstants
	{
#if DEBUG		
		public const string GUID="{lphant-A587881B-3091-4770-A6F7-C182B37DA26C}";
#else
		public const string GUID="{lphant-DD14EC11-CB90-4956-B8F4-F5D6D708DC33}";
#endif
	}
}
