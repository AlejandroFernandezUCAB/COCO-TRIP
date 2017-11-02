using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
namespace ApiRest_COCO_TRIP.Models
{
  public class ConexionBase
  {
    private static string config;
    private NpgsqlConnection sqlconexion;
    private NpgsqlCommand cmd;

    public ConexionBase()
    {
      Config = "Host=localhost;Username=admin_cocotrip;Password=ds1718a;Database=cocotrip";
      Sqlconexion = new NpgsqlConnection(Config);
      cmd = new NpgsqlCommand();
    }

    public static string Config { get => config; set => config = value; }
    public NpgsqlConnection Sqlconexion { get => sqlconexion; set => sqlconexion = value; }
    public NpgsqlCommand Cmd { get => cmd; set => cmd = value; }
  }
}
