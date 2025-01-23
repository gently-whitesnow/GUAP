// Аллея, на которой нет фонтанов
db.alleys.aggregate([
    {
        $lookup: {
            from: "park_objects", // Подключаем коллекцию park_objects
            localField: "alley_id", // Связываем по alley_id
            foreignField: "alley", // Поле alley в park_objects (теперь не массив)
            as: "objects" // Результат соединения
        }
    },
    {
        $project: {
            alley_id: 1,
            name: 1,
            has_fountains: {
                $anyElementTrue: {
                    $map: {
                        input: "$objects",
                        as: "object",
                        in: { $eq: ["$$object.object_type", "fountain"] } // Проверяем наличие фонтанов
                    }
                }
            }
        }
    },
    {
        $match: {
            has_fountains: false // Оставляем только аллеи без фонтанов
        }
    },
    {
        $project: {
            alley_id: 1,
            name: 1 // Оставляем только необходимые поля
        }
    }
]);