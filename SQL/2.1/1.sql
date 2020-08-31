SELECT
    SUM(
        od.UnitPrice * od.Quantity - (od.UnitPrice * od.Quantity * Discount)
    )
FROM
    [ Order Details ] as od