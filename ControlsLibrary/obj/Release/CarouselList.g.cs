﻿#pragma checksum "..\..\CarouselList.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "30C9038ABAA85EB270319DCBC1132A4E6720EA491882C97D96FAFA869C3EA1BD"
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


namespace ControlsLibrary {
    
    
    /// <summary>
    /// CarouselList
    /// </summary>
    public partial class CarouselList : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\CarouselList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ControlsLibrary.CarouselList Carousel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\CarouselList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas CarouselCanvas;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\CarouselList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCurrentName;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\CarouselList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrevious;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\CarouselList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNext;
        
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
            System.Uri resourceLocater = new System.Uri("/ControlsLibrary;component/carousellist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CarouselList.xaml"
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
            this.Carousel = ((ControlsLibrary.CarouselList)(target));
            
            #line 7 "..\..\CarouselList.xaml"
            this.Carousel.Loaded += new System.Windows.RoutedEventHandler(this.Carousel_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CarouselCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.lblCurrentName = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.btnPrevious = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\CarouselList.xaml"
            this.btnPrevious.Click += new System.Windows.RoutedEventHandler(this.btnPrevious_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnNext = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\CarouselList.xaml"
            this.btnNext.Click += new System.Windows.RoutedEventHandler(this.btnNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

