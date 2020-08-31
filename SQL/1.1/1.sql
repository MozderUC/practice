SELECT
    OrderID,
    ShippedDate,
    ShipVia
FROM
    Orders AS o
WHERE
    o.ShippedDate >= '1998-05-06'