﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kaikei.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Kaikei;Integrated Security=True")]
        public string KaikeiConnectionString {
            get {
                return ((string)(this["KaikeiConnectionString"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Empresa S.A. de C.V.")]
        public string EmpresaNombre {
            get {
                return ((string)(this["EmpresaNombre"]));
            }
            set {
                this["EmpresaNombre"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2450-0000")]
        public string EmpresaTelefono {
            get {
                return ((string)(this["EmpresaTelefono"]));
            }
            set {
                this["EmpresaTelefono"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0000-000000-000-0")]
        public string EmpresaNIT {
            get {
                return ((string)(this["EmpresaNIT"]));
            }
            set {
                this["EmpresaNIT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<AVENIDA/\r\nCALLE>, <CASA/LOCAL>\r\n<CIUDAD><DEPARTAMENTO>")]
        public string EmpresaDireccion {
            get {
                return ((string)(this["EmpresaDireccion"]));
            }
            set {
                this["EmpresaDireccion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.13")]
        public double IVA {
            get {
                return ((double)(this["IVA"]));
            }
            set {
                this["IVA"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CONTADOR")]
        public string EmpresaContador {
            get {
                return ((string)(this["EmpresaContador"]));
            }
            set {
                this["EmpresaContador"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ADMINISTRADOR")]
        public string EmpresaAdministrador {
            get {
                return ((string)(this["EmpresaAdministrador"]));
            }
            set {
                this["EmpresaAdministrador"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("admin")]
        public string Usuario {
            get {
                return ((string)(this["Usuario"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("kaikei")]
        public string Password {
            get {
                return ((string)(this["Password"]));
            }
        }
    }
}
