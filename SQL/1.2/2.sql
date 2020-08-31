SELECT
    c.ContactName,
    c.Country
FROM
    Customers AS c
WHERE
    c.Country NOT IN('USA', 'Canada')
ORDER BY
    c.ContactName