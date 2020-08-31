SELECT * FROM Customers as c
WHERE NOT EXISTS( SELECT * FROM Orders AS o WHERE o.CustomerID = c.CustomerID)