using FileDatabase;

var db = new BdContext();
var app = new App(db);
app.Run();
