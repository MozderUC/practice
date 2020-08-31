SELECT * FROM Employees as e
JOIN Territories AS t on e.City = t.TerritoryDescription
JOIN Region AS r on r.RegionID = t.RegionID
WHERE r.RegionDescription = 'Western'