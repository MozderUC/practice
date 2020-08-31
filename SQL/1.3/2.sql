SELECT
    cu.CustomerID,
    cu.Country
FROM
    Customers as cu
WHERE
    SUBSTRING(cu.Country, 1, 1) BETWEEN 'b'
    AND 'g'
ORDER BY
    Country;