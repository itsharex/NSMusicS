﻿#pragma checksum "..\..\..\..\Helper_UserControlLibrary\音乐馆\UserControl_分类歌单.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C6F3E8948E517F56D379C5820B9C29085D372C42B84F41CF961BE3FA3987BA94"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using 墨智音乐_3._0._1.Helper_UserControlLibrary.音乐馆;


namespace 墨智音乐_3._0._1.Helper_UserControlLibrary.音乐馆 {
    
    
    /// <summary>
    /// UserControl_分类歌单
    /// </summary>
    public partial class UserControl_分类歌单 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Helper_UserControlLibrary\音乐馆\UserControl_分类歌单.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_This_Background;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Helper_UserControlLibrary\音乐馆\UserControl_分类歌单.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_;
        
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
            System.Uri resourceLocater = new System.Uri("/墨智音乐_3.0.1;component/helper_usercontrollibrary/%e9%9f%b3%e4%b9%90%e9%a6%86/userc" +
                    "ontrol_%e5%88%86%e7%b1%bb%e6%ad%8c%e5%8d%95.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Helper_UserControlLibrary\音乐馆\UserControl_分类歌单.xaml"
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
            
            #line 9 "..\..\..\..\Helper_UserControlLibrary\音乐馆\UserControl_分类歌单.xaml"
            ((墨智音乐_3._0._1.Helper_UserControlLibrary.音乐馆.UserControl_分类歌单)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.UserControl_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Grid_This_Background = ((System.Windows.Controls.Grid)(target));
            
            #line 14 "..\..\..\..\Helper_UserControlLibrary\音乐馆\UserControl_分类歌单.xaml"
            this.Grid_This_Background.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_This_Background_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Image_ = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

