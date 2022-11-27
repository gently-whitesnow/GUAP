using System;
using FileDatabase;

Console.WriteLine("Hello, World!");

var db = new BdContext();
var app = new App(db);
app.Run();
// initialization

// open app