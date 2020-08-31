SELECT
    o.OrderID AS [ Order Number ],
    CASE
        WHEN o.ShippedDate is null THEN 'Not Shipped'
        ELSE CONVERT(varchar, o.ShippedDate)
    END AS [ Shipped Date ]
FROM
    Orders AS o
WHERE
    (
        o.ShippedDate > '1998-05-06'
        or o.ShippedDate is null
    )