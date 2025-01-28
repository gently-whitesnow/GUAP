<?php

$conn = pg_connect("host=postgres port=5432 dbname=antivirus_db user=gently");

if (!$conn) {
    die("Ошибка подключения: " . pg_last_error());
}

$query = "
    SELECT 
        a.name AS antivirus_name,
        a.price::numeric,
        a.developer,
        a.release_year,
        f.feature_name,
        f.description,
        f.importance_level
    FROM 
        antivirus a
    LEFT JOIN 
        features f ON a.id = f.antivirus_id
    ORDER BY a.id, f.importance_level DESC;
";

$result = pg_query($conn, $query);

if (!$result) {
    die("Ошибка выполнения запроса: " . pg_last_error());
}

$currentAntivirus = null;
$featureCount = 0;

if (pg_num_rows($result) == 0) {
    echo "<tr><td colspan='3'>Нет данных для отображения</td></tr>";
} else {
    while ($row = pg_fetch_assoc($result)) {
        // Если новый антивирус
        if ($row['antivirus_name'] !== $currentAntivirus) {
            if ($currentAntivirus !== null) {
                // Закрыть предыдущую группу строк
                echo "</tr>";
            }

            $currentAntivirus = $row['antivirus_name'];
            $featureCount = 1; // Счетчик особенностей для rowspan

            // Подсчет особенностей для текущего антивируса
            $countQuery = "
                SELECT COUNT(*)
                FROM features
                WHERE antivirus_id = (
                    SELECT id FROM antivirus WHERE name = '{$currentAntivirus}'
                )
            ";
            $countResult = pg_query($conn, $countQuery);
            if ($countResult) {
                $featureCount = pg_fetch_result($countResult, 0, 0) ?: 1;
            }

            // Первая строка с названием программы и ценой
            echo "<tr>";
            echo "<td rowspan='{$featureCount}' data-tooltip='Описание: {$row['description']}'>{$row['antivirus_name']} ({$row['release_year']})</td>";            
            echo "<td rowspan='{$featureCount}'>{$row['price']} руб./год</td>";
            echo "<td data-tooltip='Описание: {$row['description']}'>{$row['feature_name']} (оценка - {$row['importance_level']})</td>";        
        } else {
            // Остальные строки только с особенностями
            echo "<tr>";
            echo "<td data-tooltip='Описание: {$row['description']}'>{$row['feature_name']} (оценка - {$row['importance_level']})</td>";       
        }
    }
}

pg_free_result($result);
pg_close($conn);
?>