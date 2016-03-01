$(document).ready(function() {
  var workerId = 0;
  var taskId = 0;
  function go() {
    if(workerId > 0 && taskId > 0)
      window.location.href = "/assign/"+workerId+"/"+taskId;
  }
  $(".worker").each(function() {
    $(this).click(function() {
      if($(this).hasClass('selected'))
      {
        window.location.href="/worker/"+$(this).attr('data-id');
      }
      $(".worker").removeClass("selected");
      $(this).addClass('selected');
      workerId = $(this).attr('data-id');
      go();
    });
  });
  $(".chore").each(function() {
    $(this).click(function() {
      taskId = $(this).attr('data-id');
      $(".chore").removeClass("selected");
      $(this).addClass('selected');
      go();
    });
  });
});
