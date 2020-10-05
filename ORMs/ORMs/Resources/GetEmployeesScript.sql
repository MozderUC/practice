SELECT e.EmployeeID, e.LastName, e.FirstName, t.TerritoryID, t.TerritoryDescription FROM Employees AS e
JOIN EmployeeTerritories AS et ON e.EmployeeID = et.EmployeeID
JOIN Territories AS t ON t.TerritoryID = et.TerritoryID