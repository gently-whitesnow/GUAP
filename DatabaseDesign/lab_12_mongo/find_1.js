//Аллеи, на которых встречаются разные виды кленов (клен в названии)
db.park_objects.aggregate([
    {
        $match: {
            "details.species_name": { $regex: "Клен", $options: "i" }, // Ищем деревья с "Клен" в названии
            object_type: "tree"
        }
    },
    {
        $group: {
            _id: "$alley", // Группируем по аллеям
            species: { $addToSet: "$details.species_name" } // Собираем уникальные виды кленов
        }
    },
    {
        $match: {
            $expr: { $gt: [{ $size: "$species" }, 1] } // Оставляем аллеи с несколькими видами кленов
        }
    },
    {
        $lookup: {
            from: "alleys", // Подключаем коллекцию аллей
            localField: "_id",
            foreignField: "alley_id",
            as: "alley_info"
        }
    },
    {
        $project: {
            alley_id: "$_id",
            alley_name: { $arrayElemAt: ["$alley_info.name", 0] }, // Берем название аллеи
            species: 1
        }
    }
]);