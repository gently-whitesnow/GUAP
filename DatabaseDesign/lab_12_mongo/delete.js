// удаление 10 объекта
db.park_objects.deleteOne({ object_id: 10 });

// удаление всех статуй
db.park_objects.deleteMany({ object_type: "statue" });

