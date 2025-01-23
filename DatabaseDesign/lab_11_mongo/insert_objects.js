db.park_objects.insertMany([
    // Статуи
    {
        object_id: 1,
        object_type: "statue",
        location: {latitude: 55.123456, longitude: 37.123456},
        details: {statue_name: "Памятник Пушкину"},
        alley: 1
    },
    {
        object_id: 2,
        object_type: "statue",
        location: {latitude: 55.234567, longitude: 37.234567},
        details: {statue_name: "Памятник Лермонтову"},
        alley: 4
    },

    // Фонтаны
    {
        object_id: 3,
        object_type: "fountain",
        location: {latitude: 55.345678, longitude: 37.345678},
        details: {fountain_name: "Большой фонтан"},
        alley: 1
    },
    {
        object_id: 4,
        object_type: "fountain",
        location: {latitude: 55.456789, longitude: 37.456789},
        details: {fountain_name: "Фонтан Ручеек"},
        alley: 3
    },

    // Деревья
    {
        object_id: 5,
        object_type: "tree",
        location: {latitude: 55.987654, longitude: 37.987654},
        details: {
            species_id: 1,
            species_name: "Клен красный",
            planting_date: "2020-05-10",
            trimming_date: "2021-05-10"
        },
        alley: 2
    },
    {
        object_id: 6,
        object_type: "tree",
        location: {latitude: 55.876543, longitude: 37.876543},
        details: {
            species_id: 2,
            species_name: "Клен серебристый",
            planting_date: "2020-05-15",
            trimming_date: null
        },
        alley: 2
    },
    {
        object_id: 7,
        object_type: "tree",
        location: {latitude: 55.765432, longitude: 37.765432},
        details: {
            species_id: 3,
            species_name: "Дуб",
            planting_date: "2019-04-20",
            trimming_date: "2020-04-20"
        },
        alley: 4
    },
    {
        object_id: 8,
        object_type: "tree",
        location: {latitude: 55.654321, longitude: 37.654321},
        details: {
            species_id: 4,
            species_name: "Сосна",
            planting_date: "2021-06-01",
            trimming_date: null
        },
        alley: 1
    },
    {
        object_id: 9,
        object_type: "tree",
        location: {latitude: 55.543210, longitude: 37.543210},
        details: {
            species_id: 5,
            species_name: "Клен сахарный",
            planting_date: "2022-06-05",
            trimming_date: null
        },
        alley: 2
    },
    {
        object_id: 10,
        object_type: "tree",
        location: {latitude: 55.432109, longitude: 37.432109},
        details: {
            species_id: 1,
            species_name: "Клен красный",
            planting_date: "2023-04-15",
            trimming_date: null
        },
        alley: 3
    }
]);