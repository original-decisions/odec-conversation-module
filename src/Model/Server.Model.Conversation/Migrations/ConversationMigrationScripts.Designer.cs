﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace odec.Server.Model.Conversation.Migrations {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ConversationMigrationScripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal ConversationMigrationScripts() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("odec.CP.Server.Model.Conversation.Migrations.ConversationMigrationScripts", typeof(ConversationMigrationScripts).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to  begin tran
        ///IF schema_id(&apos;conv&apos;) IS NULL
        ///    EXECUTE(&apos;CREATE SCHEMA [conv]&apos;)
        ///IF  NOT EXISTS (SELECT * FROM sys.objects 
        ///	WHERE object_id = OBJECT_ID(N&apos;[conv].[ConversationMessages]&apos;) AND type in (N&apos;U&apos;))
        ///begin
        ///CREATE TABLE [conv].[ConversationMessages] (
        ///    [ConversationId] [int] NOT NULL,
        ///    [MessageId] [int] NOT NULL,
        ///    CONSTRAINT [PK_conv.ConversationMessages] PRIMARY KEY ([ConversationId], [MessageId])
        ///)
        ///CREATE INDEX [IX_ConversationId] ON [conv].[ConversationMessages]([ConversationId])
        ///CR [rest of string was truncated]&quot;;.
        /// </summary>
        public static string ConversationInitial {
            get {
                return ResourceManager.GetString("ConversationInitial", resourceCulture);
            }
        }
    }
}
