using System;
using System.Collections.Generic;

namespace Wii_U_Homebrew_Installer_GUI.Properties {

    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings {

        static List<string> Log { get; set; }

        public Settings() {
            SettingChanging += SettingChangingEventHandler;
            SettingsSaving += SettingsSavingEventHandler;
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            if (Log is not null)
                Log.Add($"{e.SettingName}'s value has been changed to {e.NewValue}.");
            else
                Log = new List<string>
                {
                    $"{e.SettingName}'s value has been changed to {e.NewValue}."
                };
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            if (Log is not null)
                Log.Add("The Settings have been saved.");
            else
                Log = new List<string>
                {
                    "The Settings have been saved."
                };
        }
    }
}
