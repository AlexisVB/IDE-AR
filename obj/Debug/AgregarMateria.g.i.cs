﻿#pragma checksum "..\..\AgregarMateria.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "81C4646F32CCA7A024C31F20790F12DD2F5885D9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace IDE_AR {
    
    
    /// <summary>
    /// AgregarMateria
    /// </summary>
    public partial class AgregarMateria : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\AgregarMateria.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrar;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\AgregarMateria.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNombreMateria;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\AgregarMateria.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMatriculaMateria;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\AgregarMateria.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnColor;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\AgregarMateria.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNickMateria;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\AgregarMateria.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstMaterias;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\AgregarMateria.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAgregar;
        
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
            System.Uri resourceLocater = new System.Uri("/IDE-AR;component/agregarmateria.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AgregarMateria.xaml"
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
            this.btnCerrar = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\AgregarMateria.xaml"
            this.btnCerrar.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtNombreMateria = ((System.Windows.Controls.TextBox)(target));
            
            #line 55 "..\..\AgregarMateria.xaml"
            this.txtNombreMateria.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtNombreMateria_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtMatriculaMateria = ((System.Windows.Controls.TextBox)(target));
            
            #line 62 "..\..\AgregarMateria.xaml"
            this.txtMatriculaMateria.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtMatriculaMateria_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnColor = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\AgregarMateria.xaml"
            this.btnColor.Click += new System.Windows.RoutedEventHandler(this.btnColor_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtNickMateria = ((System.Windows.Controls.TextBox)(target));
            
            #line 72 "..\..\AgregarMateria.xaml"
            this.txtNickMateria.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtNickMateria_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lstMaterias = ((System.Windows.Controls.ListView)(target));
            
            #line 81 "..\..\AgregarMateria.xaml"
            this.lstMaterias.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstMaterias_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnAgregar = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\AgregarMateria.xaml"
            this.btnAgregar.Click += new System.Windows.RoutedEventHandler(this.btnAgregarClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

