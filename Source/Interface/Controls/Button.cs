#region Copyright (c)2009 Hathi Project < http://hathi.sourceforge.net >
/*
* This file is part of Hathi Project
* Hathi Developers Team:
* andrewdev, beckman16, biskvit, elnomade_devel, ershyams, grefly, jpierce420,
* knocte, kshah05, manudenfer, palutz, ramone_hamilton, soudamini, writetogupta
*
* Hathi is a fork of Lphant Version 1.0 GPL
* Lphant Team
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
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Hathi.Interface
{
/// <summary>
/// Descripción breve de HathiButton.
/// </summary>
public class HathiButton : Button
{
    private System.ComponentModel.IContainer components;
    private Pen focusPen;

    public HathiButton(System.ComponentModel.IContainer container)
    {
        /// <summary>
        /// Requerido para la compatibilidad con el Diseñador de composiciones de clases Windows.Forms
        /// </summary>
        container.Add(this);
        InitializeComponent();
        //
        // TODO: Agregar código de constructor después de llamar a InitializeComponent
        //
        this.FlatStyle=FlatStyle.Flat;
        this.BackColor=Color.Transparent;
        this.Text="";
        focusPen=new Pen(Color.Black,1);
        focusPen.DashStyle=DashStyle.Dot;
        //this.Image=this.ImageList.Images[0];
    }

    public HathiButton()
    {
        /// <summary>
        /// Requerido para la compatibilidad con el Diseñador de composiciones de clases Windows.Forms
        /// </summary>
        InitializeComponent();
        //
        // TODO: Agregar código de constructor después de llamar a InitializeComponent
        //
        focusPen=new Pen(Color.Black,1);
        focusPen.DashStyle=DashStyle.Dot;
        //this.Image=this.ImageList.Images[0];
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        this.InvokePaintBackground(this,e);
        if (Focused) e.Graphics.DrawRectangle(focusPen,3,3,Width-5,Height-5);
        e.Graphics.DrawImage(this.Image,Width/2-Image.Width/2,5);
//          base.OnPaint(e);
    }
    protected override void OnMouseEnter(EventArgs e )
    {
        //this.Image=this.ImageList.Images[1];
        this.ImageIndex=1;
        base.OnMouseEnter(e);
    }
    protected override void OnMouseLeave(EventArgs e )
    {
        //this.Image=this.ImageList.Images[0];
        this.ImageIndex=0;
        base.OnMouseLeave(e);
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        //this.Image=this.ImageList.Images[0];
        this.ImageIndex=3;
        base.OnMouseDown(e);
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        //this.Image=this.ImageList.Images[0];
        this.ImageIndex=1;
        base.OnMouseUp(e);
    }

    #region Component Designer generated code
    /// <summary>
    /// Método necesario para admitir el Diseñador, no se puede modificar
    /// el contenido del método con el editor de código.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
    }
    #endregion
}
}
