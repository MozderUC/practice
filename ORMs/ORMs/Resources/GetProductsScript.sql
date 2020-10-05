SELECT p.ProductID as ProductId, p.ProductName, s.SupplierID as SupplierId, s.PostalCode, c.CategoryID as CategoryId, c.CategoryName, c.Description FROM Products as p
JOIN Suppliers as s on p.SupplierID = s.SupplierID
JOIN Categories as c on c.CategoryID = p.CategoryID