//convert "2023-03-17T14:17:18.387" to "2023-03-17 14:17:18"
function convertDate(date) {
  var date = new Date(date);
  var year = date.getFullYear();
  var month = date.getMonth() + 1;
  var dt = date.getDate();
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var seconds = date.getSeconds();
  return (
    year + "-" + month + "-" + dt + " " + hours + ":" + minutes + ":" + seconds
  );
}
