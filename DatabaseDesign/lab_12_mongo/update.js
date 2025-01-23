// изменяем имя аллеи
db.alleys.updateOne(
    { alley_id: 1 }, // Условие поиска
    { $set: { name: "Главная аллея" } } // Поля для обновления
);

// добавление нового поля для фонтанов
db.park_objects.updateMany(
    { object_type: "fountain" }, // Условие поиска 
    { $set: { description: "Этот объект является фонтаном в парке" } } // Обновление поля 
);

// добавляем счетчи и инкрементируем его
db.park_objects.updateMany(
    { object_type: "fountain" }, // Условие поиска
    { $inc: { visits: 1 } } // Увеличиваем значение на 1
);