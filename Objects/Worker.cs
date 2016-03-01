using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ChoresNS.Objects
{
  public class Worker : DBHandler
  {
    private int _id;
    private string _name;
    public static string Table = "workers";
    public static string NameColumn = "name";

    public Worker(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public override bool Equals(System.Object otherWorker)
    {
      if(!(otherWorker is Worker))
      {
        return false;
      }
      else
      {
        Worker newWorker = (Worker) otherWorker;
        bool idEquality = this.GetId() == newWorker.GetId();
        bool nameEquality = this.GetName() == newWorker.GetName();
        return (idEquality && nameEquality);
      }
    }
    public static void DeleteAll()
    {
      DBHandler.DeleteAll(Worker.Table);
    }
    public void Save()
    {

      List<string> columns = new List<string>{Worker.NameColumn};
      List<SqlParameter> parameters = new List<SqlParameter>{new SqlParameter("@name", GetName())};
      _id = base.Save(Worker.Table, columns, parameters, GetId());
    }
    public static List<Worker> GetAll()
    {
      List<Object> newList = DBHandler.GetAll(Worker.Table, MakeObject);
      return newList.Cast<Worker>().ToList();
    }
    public List<Job> GetJobs()
    {
      string queryString = "JOIN assignments ON (jobs.id = assignments.jobs_id) JOIN workers ON (assignments.workers_id = workers.id) WHERE workers.id = @id";
      List<SqlParameter> parameters = new List<SqlParameter> {new SqlParameter("@id", this.GetId())};
      return GetList(Job.Table, queryString, Job.MakeObject, parameters).Cast<Job>().ToList();
    }
    public void AddAssignment(int jobId)
    {
      string table = "assignments";
      List<string> columns = new List<string> { "jobs_id", "workers_id" };
      List<SqlParameter> parameters = new List<SqlParameter> {
      new SqlParameter("@"+columns[0], jobId),
      new SqlParameter("@"+columns[1], GetId())};
      base.Save(table, columns, parameters);
    }
    public void Delete()
    {
      DBHandler.Delete(Worker.Table, _id);
    }
    public static Worker Find(int id)
    {
      string query = "WHERE id = @id";
      List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@id", id) };
      return (Worker) DBHandler.GetObjectFromDB(Worker.Table, query, MakeObject, parameters);
    }
    public static Worker Find(string name)
    {
      string query = "WHERE "+Worker.NameColumn+" = @"+Worker.NameColumn+"";
      List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@"+Worker.NameColumn, name) };
      return (Worker) DBHandler.GetObjectFromDB(Worker.Table, query, MakeObject, parameters);
    }

    public static Object MakeObject(SqlDataReader rdr)
    {
      return new Worker(rdr.GetString(1), rdr.GetInt32(0));
    }
  } // end class
} // end namespace
