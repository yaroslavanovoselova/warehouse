﻿#pragma checksum "..\..\..\CreateItem.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8FA87DEF4B434D380AA3A13DBDE50F0C67F59D8B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using warehouse;


namespace warehouse {
    
    
    /// <summary>
    /// CreateItem
    /// </summary>
    public partial class CreateItem : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal warehouse.CreateItem ArticslNumberTextBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameOfItemTextBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ArticleNumberTextBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AmountLeftTextBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OKButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DownloadImageButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\CreateItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ItemImage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/warehouse;component/createitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CreateItem.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ArticslNumberTextBox = ((warehouse.CreateItem)(target));
            return;
            case 2:
            this.NameOfItemTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ArticleNumberTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.AmountLeftTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.PriceTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.DescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.OKButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\CreateItem.xaml"
            this.OKButton.Click += new System.Windows.RoutedEventHandler(this.OKButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DownloadImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\CreateItem.xaml"
            this.DownloadImageButton.Click += new System.Windows.RoutedEventHandler(this.DownloadImageButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ItemImage = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

