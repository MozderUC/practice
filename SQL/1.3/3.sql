SELECT
    *
FROM
    Customers AS cu
WHERE
    SUBSTRING(cu.Country, 1, 1) IN ('b', 'c', 'd', 'e', 'f', 'g')