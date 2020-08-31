SELECT
    EmployeeID,
    CustomerID,
    COUNT(*) as Total
FROM
    (
        SELECT
            *
        FROM
            Orders as o1
        WHERE
            YEAR(o1.OrderDate) = '1998'
    ) as o
GROUP BY
    EmployeeID,
    CustomerID