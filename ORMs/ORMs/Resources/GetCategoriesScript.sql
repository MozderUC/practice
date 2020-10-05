SELECT c.CategoryID, c.CategoryName, c.Description, p.ProductID, p.ProductName, p.CategoryID, p.SupplierID 
FROM Categories as c 
JOIN Products AS p ON p.CategoryID = c.CategoryID