<?php
$conn = pg_connect("host=localhost port=5440 dbname=antivirus_db user=gently");

$query = "
    SELECT 
        a.name AS antivirus_name,
        a.price,
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

echo "<h2>Антивирусные программы</h2>";
while ($row = pg_fetch_assoc($result)) {
    echo "<h3>{$row['antivirus_name']} ({$row['developer']}, {$row['release_year']})</h3>";
    echo "<p>Цена: {$row['price']}</p>";
    if ($row['feature_name']) {
        echo "<p>Особенность: <strong>{$row['feature_name']}</strong> - {$row['description']} (Важность: {$row['importance_level']})</p>";
    }
}
pg_close($conn);
?>