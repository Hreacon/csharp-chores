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
        return View["header.cshtml"];
      };
    }
  }
}
