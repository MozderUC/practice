SELECT
    o.OrderID,
    CASE
        WHEN ShippedDate is null THEN 'Not Shipped'
        ELSE CONVERT(varchar, ShippedDate)
    END AS ShippedDate
FROM
    Orders AS o
WHERE
    ShippedDate is null;