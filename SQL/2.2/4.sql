SELECT
    Name,
    City
FROM
    (
        SELECT
            City,
            ContactName as Name
        FROM
            Customers
        UNION
        SELECT
            City,
            FirstName as Name
        FROM
            Employees
    ) as e
WHERE
    e.City in (
        (
            SELECT
                City
            FROM
                Customers
            INTERSECT
            SELECT
                City
            FROM
                Employees
        )
    )