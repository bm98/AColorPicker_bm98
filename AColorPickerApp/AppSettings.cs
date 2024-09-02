using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adobe_color_picker_clone_part_1
{
  internal sealed class AppSettings : ApplicationSettingsBase, IDisposable
  {
    // Singleton
    private static readonly Lazy<AppSettings> m_lazy = new Lazy<AppSettings>( ( ) => new AppSettings( ) );
    public static AppSettings Instance { get => m_lazy.Value; }


    private AppSettings( )
    {
      if (this.FirstRun) {
        // migrate the settings to the new version if the app runs the first time
        try {
          this.Upgrade( );
        }
        catch { }
        this.FirstRun = false;
        this.Save( );
      }
    }

    public void Dispose( bool disposing )
    {
      if (disposing) {
        // dispose managed resources
      }
      // free native resources
    }

    public void Dispose( )
    {
      Dispose( true );
      GC.SuppressFinalize( this );
    }



    #region Setting Properties

    // manages Upgrade
    [UserScopedSetting( )]
    [DefaultSettingValue( "True" )]
    public bool FirstRun {
      get { return (bool)this["FirstRun"]; }
      set { this["FirstRun"] = value; }
    }

    [UserScopedSetting( )]
    [DefaultSettingValue( "" )]
    public string StoredColors {
      get { return (string)this["StoredColors"]; }
      set { this["StoredColors"] = value; }
    }

    #endregion

  }
}


