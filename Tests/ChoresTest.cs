using Xunit;
using ChoresNS.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ChoresNS
{
  public class ChoresTest : IDisposable
  {
     public ChoresTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=chores_test;Integrated Security=SSPI;";
     }
     public void Dispose()
     {
       Chores.DeleteAll();
     }
  }
}
