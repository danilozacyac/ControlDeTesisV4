﻿#pragma checksum "..\..\..\UtilitiesWin\PlenosCircuito.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "01E76BBE5C02687CFE30B1CC68B7790C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34209
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ControlDeTesisV4.UtilitiesWin {
    
    
    /// <summary>
    /// PlenosCircuito
    /// </summary>
    public partial class PlenosCircuito : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtPleno;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ChkEspecializado;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtEspecializacion;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnGuardar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ControlDeTesisV4;component/utilitieswin/plenoscircuito.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
            ((ControlDeTesisV4.UtilitiesWin.PlenosCircuito)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TxtPleno = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ChkEspecializado = ((System.Windows.Controls.CheckBox)(target));
            
            #line 25 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
            this.ChkEspecializado.Checked += new System.Windows.RoutedEventHandler(this.ChkEspecializado_Checked);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
            this.ChkEspecializado.Unchecked += new System.Windows.RoutedEventHandler(this.ChkEspecializado_Unchecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TxtEspecializacion = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.BtnGuardar = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\UtilitiesWin\PlenosCircuito.xaml"
            this.BtnGuardar.Click += new System.Windows.RoutedEventHandler(this.BtnGuardar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
