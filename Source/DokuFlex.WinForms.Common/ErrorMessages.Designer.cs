﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DokuFlex.WinForms.Common {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DokuFlex.WinForms.Common.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ha ocurrido una excepcion mientras se procesaba la tarea..
        /// </summary>
        internal static string AsyncTaskError {
            get {
                return ResourceManager.GetString("AsyncTaskError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Si sus credenciales son correctas compruebe los parámetros de conexión en Configuración....
        /// </summary>
        internal static string CheckSettingsInfo {
            get {
                return ResourceManager.GetString("CheckSettingsInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El archivo seleccionado no es valido..
        /// </summary>
        internal static string FileNotValidError {
            get {
                return ResourceManager.GetString("FileNotValidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No hay una carpeta seleccionada. Para cerrar este dialogo seleccione un carpeta..
        /// </summary>
        internal static string FolderNotValidError {
            get {
                return ResourceManager.GetString("FolderNotValidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se pudo iniciar sesión. Compruebe que el nombre de usuario y/o la contraseña sean correctos..
        /// </summary>
        internal static string LoginError {
            get {
                return ResourceManager.GetString("LoginError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se pudo crear la carpeta en DokuFlex.
        /// </summary>
        internal static string NewFolderError {
            get {
                return ResourceManager.GetString("NewFolderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se encontro el repositorio y/o la carpeta de destino..
        /// </summary>
        internal static string RepositoryNotFoundError {
            get {
                return ResourceManager.GetString("RepositoryNotFoundError", resourceCulture);
            }
        }
    }
}