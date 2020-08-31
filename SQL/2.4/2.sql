SELECT e.FirstName FROM Employees as e
WHERE e.EmployeeID IN (SELECT o.EmployeeID FROM Orders as o GROUP BY o.EmployeeID HAVING COUNT(*) >= 150)