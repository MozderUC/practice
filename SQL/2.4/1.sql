SELECT * FROM Suppliers as s
WHERE s.SupplierID NOT IN (SELECT p.SupplierID FROM Products as p GROUP BY p.SupplierID HAVING SUM(p.UnitsInStock) != 0)