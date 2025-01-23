// Дерево, которое было посажено позже всех
db.park_objects.find({ object_type: "tree" })
    .sort({ "details.planting_date": -1 }) // Сортируем по дате посадки
    .limit(1); // Берем самое позднее