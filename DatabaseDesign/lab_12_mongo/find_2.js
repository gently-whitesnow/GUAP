// Аллеи, на которых есть и статуи, и фонтаны
db.park_objects.aggregate([
    {
        $match: { object_type: { $in: ["statue", "fountain"] } } // Выбираем статуи и фонтаны
    },
    {
        $group: {
            _id: "$alley", // Группируем по полю `alley`
            types: { $addToSet: "$object_type" } // Собираем уникальные типы объектов
        }
    },
    {
        $match: {
            types: { $all: ["statue", "fountain"] } // Оставляем аллеи, где есть и статуи, и фонтаны
        }
    },
    {
        $lookup: {
            from: "alleys", // Подключаем коллекцию аллей
            localField: "_id", // Поле `_id` в группе соответствует `alley`
            foreignField: "alley_id", // Связываем с `alley_id` в коллекции `alleys`
            as: "alley_info"
        }
    },
    {
        $project: {
            alley_id: "$_id", // ID аллеи
            alley_name: { $arrayElemAt: ["$alley_info.name", 0] } // Название аллеи
        }
    }
]);