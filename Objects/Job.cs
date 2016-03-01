using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ChoresNS.Objects
{
  public class Job : DBHandler
  {
    private int _id;
    private string _name;
    public static string Table = "jobs";
    public static string NameColumn = "name";

    public Job(string name, int id = 0)
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
    public override bool Equals(System.Object otherJob)
    {
      if(!(otherJob is Job))
      {
        return false;
      }
      else
      {
        Job newJob = (Job) otherJob;
        bool idEquality = this.GetId() == newJob.GetId();
        bool nameEquality = this.GetName() == newJob.GetName();
        return (idEquality && nameEquality);
      }
    }
    public static void DeleteAll()
    {
      DBHandler.DeleteAll(Job.Table);
    }
    public void Save()
    {

      List<string> columns = new List<string>{Job.NameColumn};
      List<SqlParameter> parameters = new List<SqlParameter>{new SqlParameter("@name", GetName())};
      _id = base.Save(Job.Table, columns, parameters, GetId());
    }
    public static List<Job> GetAll()
    {
      List<Object> newList = DBHandler.GetAll(Job.Table, MakeObject);
      return newList.Cast<Job>().ToList();
    }

    public void Delete()
    {
      DBHandler.Delete(Job.Table, _id);
    }
    public static Job Find(int id)
    {
      string query = "WHERE id = @id";
      List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@id", id) };
      return (Job) DBHandler.GetObjectFromDB(Job.Table, query, MakeObject, parameters);
    }
    public static Job Find(string name)
    {
      string query = "WHERE "+Job.NameColumn+" = @"+Job.NameColumn+"";
      List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@"+Job.NameColumn, name) };
      return (Job) DBHandler.GetObjectFromDB(Job.Table, query, MakeObject, parameters);
    }
    public List<Worker> GetWorkers()
    {
      string queryString = "JOIN assignments ON (workers.id = assignments.workers_id) JOIN jobs ON (assignments.jobs_id = jobs.id) WHERE jobs.id = @id";
      List<SqlParameter> parameters = new List<SqlParameter> {new SqlParameter("@id", this.GetId())};
      return GetList(Worker.Table, queryString, Worker.MakeObject, parameters).Cast<Worker>().ToList();
    }
    public void AddAssignment(int workerId)
    {
      string table = "assignments";
      List<string> columns = new List<string> { "jobs_id", "workers_id" };
      List<SqlParameter> parameters = new List<SqlParameter> {
      new SqlParameter("@"+columns[0], GetId()),
      new SqlParameter("@"+columns[1], workerId)};
      base.Save(table, columns, parameters);
    }
    public static Object MakeObject(SqlDataReader rdr)
    {
      return new Job(rdr.GetString(1), rdr.GetInt32(0));
    }
  } // end class
} // end namespace
