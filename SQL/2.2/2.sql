SELECT
    COUNT(*) as [ Orders count ],
    (
        SELECT
            (e.FirstName + e.LastName)
        FROM
            Employees as e
        WHERE
            e.EmployeeID = o.EmployeeID
    ) as [ Name ]
FROM
    Orders as o
GROUP BY
    o.EmployeeID