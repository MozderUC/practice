SELECT
    DISTINCT od.OrderId
FROM
    [Order Details] AS od
WHERE
    od.Quantity BETWEEN 3
    AND 10;