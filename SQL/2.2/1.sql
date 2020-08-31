SELECT
    YEAR(o.OrderDate) AS [ Year ],
    COUNT(o.Freight) AS [ Total Freight ]
FROM
    Orders AS o
GROUP BY
    YEAR(o.OrderDate)