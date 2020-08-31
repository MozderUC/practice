SELECT c.ContactName, (SELECT Count(*) FROM Orders as o1 WHERE o1.CustomerID = c.CustomerID) as Total
FROM Customers as c
ORDER BY Total


SELECT c.ContactName,
COUNT(o.CustomerID) AS Total FROM Customers as c
LEFT JOIN Orders AS o ON c.CustomerID = o.CustomerID
GROUP BY c.ContactName
ORDER BY Total