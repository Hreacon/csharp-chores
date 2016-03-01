using Nancy;
using ChoresNS.Objects;
using System.Collections.Generic;
namespace ChoresNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("jobs", Job.GetAll());
        model.Add("workers", Worker.GetAll());
        return View["index.cshtml", model];
      };
      Get["/assign/{wid}/{jid}"] = x => {
        int workerId = x.wid;
        int jobId = x.jid;
        Worker.Find(workerId).AddAssignment(jobId);
        return View["forward.cshtml", "/"];
      };
      Get["/worker/{id}"] = x => {
        return View["worker.cshtml", Worker.Find(int.Parse(x.id))];
      };
      Post["/addChore"] = _ => {
        string name = Request.Form["name"];
        new Job(name).Save();
        return View["forward.cshtml", "/"];
      };
      Post["/worker/{id}/addChore"] = x => {
        Job j = new Job(Request.Form["name"]);
        j.Save();
        Worker.Find(int.Parse(x.id)).AddAssignment(j.GetId());
        return View["forward.cshtml", "/worker/" + int.Parse(x.id)];
      };
      Post["/addWorker"] = _ => {
        new Worker(Request.Form["name"]).Save();
        return View["forward.cshtml", "/"];
      };
    }
  }
}
