﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NeoMem2.Gui.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.3.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\wc\\NeoMem2\\Notes.csv")]
        public string ImportConnectionString {
            get {
                return ((string)(this["ImportConnectionString"]));
            }
            set {
                this["ImportConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("server=.;database=NeoMem2;trusted_connection=true")]
        public string MainConnectionString {
            get {
                return ((string)(this["MainConnectionString"]));
            }
            set {
                this["MainConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("NeoMem2.Data.SqlServer.SqlServerStore")]
        public string StoreType {
            get {
                return ((string)(this["StoreType"]));
            }
            set {
                this["StoreType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Score,LastModifiedDate,Class,Name,Tags,TextFormat,[all custom]")]
        public string NoteColumns {
            get {
                return ((string)(this["NoteColumns"]));
            }
            set {
                this["NoteColumns"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ClassNoteTreeModelSorted {
            get {
                return ((bool)(this["ClassNoteTreeModelSorted"]));
            }
            set {
                this["ClassNoteTreeModelSorted"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ClassNoteTreeModelGroupChildren {
            get {
                return ((bool)(this["ClassNoteTreeModelGroupChildren"]));
            }
            set {
                this["ClassNoteTreeModelGroupChildren"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int MaxChildNodesForAutoExpand {
            get {
                return ((int)(this["MaxChildNodesForAutoExpand"]));
            }
            set {
                this["MaxChildNodesForAutoExpand"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Right")]
        public global::System.Windows.Forms.DockStyle NoteFormDefaultPropertiesLocation {
            get {
                return ((global::System.Windows.Forms.DockStyle)(this["NoteFormDefaultPropertiesLocation"]));
            }
            set {
                this["NoteFormDefaultPropertiesLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Bottom")]
        public global::System.Windows.Forms.DockStyle NoteFormDefaultLinksLocation {
            get {
                return ((global::System.Windows.Forms.DockStyle)(this["NoteFormDefaultLinksLocation"]));
            }
            set {
                this["NoteFormDefaultLinksLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MatcherType {
            get {
                return ((string)(this["MatcherType"]));
            }
            set {
                this["MatcherType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TreeModelType {
            get {
                return ((string)(this["TreeModelType"]));
            }
            set {
                this["TreeModelType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string NoteModelType {
            get {
                return ((string)(this["NoteModelType"]));
            }
            set {
                this["NoteModelType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SelectTopSearchMatch {
            get {
                return ((bool)(this["SelectTopSearchMatch"]));
            }
            set {
                this["SelectTopSearchMatch"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("WhenOnlyTemplates")]
        public global::NeoMem2.Gui.Core.TemplateVisibilityType TemplateVisibility {
            get {
                return ((global::NeoMem2.Gui.Core.TemplateVisibilityType)(this["TemplateVisibility"]));
            }
            set {
                this["TemplateVisibility"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ExportConnectionString {
            get {
                return ((string)(this["ExportConnectionString"]));
            }
            set {
                this["ExportConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool RefreshOnNewNote {
            get {
                return ((bool)(this["RefreshOnNewNote"]));
            }
            set {
                this["RefreshOnNewNote"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ARQ,Email,Employee,Journal")]
        public string HtmlExportClassesToExclude {
            get {
                return ((string)(this["HtmlExportClassesToExclude"]));
            }
            set {
                this["HtmlExportClassesToExclude"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\")]
        public string StructuredNoteNameDelimiter {
            get {
                return ((string)(this["StructuredNoteNameDelimiter"]));
            }
            set {
                this["StructuredNoteNameDelimiter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\temp\\neoMem2ExternalNote.md")]
        public string ExternalEditorTempFile {
            get {
                return ((string)(this["ExternalEditorTempFile"]));
            }
            set {
                this["ExternalEditorTempFile"] = value;
            }
        }
    }
}
