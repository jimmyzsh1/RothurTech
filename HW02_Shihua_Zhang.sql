USE AdventureWorks2019;
select * from Production.Product
select * from Production.ProductInventory
select * from Person.CountryRegion
select * from Person.StateProvince



--1.How many products can you find in the Production.Product table?

select count(*) from Production.Product

--2.Write a query that retrieves the number of products in the Production.Product table that are included in a subcategory. 
--The rows that have NULL in column ProductSubcategoryID are considered to not be a part of any subcategory.

--1.How many products can you find in the Production.Product table?

select count(*) AS ProductsWithSubcategory from Production.Product where ProductSubcategoryID is NOT null

--3.Count how many products belong to each product subcategory.
	Write a query that displays the result with two columns:

	ProductSubcategoryID (the subcategory ID)， CountedProducts (the number of products in that subcategory).



	select ProductSubcategoryID as "subcategory ID", count(*) as CountedProducts 
	from Production.Product
	where ProductSubcategoryID is NOT NULL
	group by ProductSubcategoryID

--4.How many products that do not have a product subcategory.

	select count(*) 
	from Production.Product
	where ProductSubcategoryID is NULL

--5.Write a query to list the sum of products quantity in the Production.ProductInventory table.

	select sum(Quantity) as "Sum of Products"
	from Production.ProductInventory


--6.Write a query to list the sum of products in the Production.ProductInventory table 
--and LocationID set to 40 and limit the result to include just summarized quantities less than 100.
	


	SELECT ProductID, sum(Quantity) as "Sum of Products"
	FROM Production.ProductInventory
	WHERE LocationID = 40
	group by ProductID
	having sum(Quantity) < 100




--7. Write a query to list the sum of products with the shelf information in the Production.ProductInventory 
--table and LocationID set to 40 and limit the result to include just summarized quantities less than 100

	SELECT ProductID, Shelf, sum(Quantity) as "Sum of Products"
	FROM Production.ProductInventory
	WHERE LocationID = 40
	group by ProductID, shelf
	having sum(Quantity) < 100


--8.Write the query to list the average quantity for products where column LocationID has the value of 10 from the table Production.ProductInventory table.

	select avg(Quantity) as "AverageQuantity"
	from Production.ProductInventory
	where LocationID = 10

--9.Write query  to see the average quantity  of  products by shelf  from the table Production.ProductInventory

	select shelf, avg(Quantity) as "AverageQuantity"
	from Production.ProductInventory
	group by shelf


--10.Write query  to see the average quantity  of  products by shelf excluding rows that has the value of N/A in the column Shelf from the table Production.ProductInventory

	select shelf, avg(Quantity) as "AverageQuantity"
	from Production.ProductInventory
	where shelf <> 'N/A'
	group by shelf

--11.List the members (rows) and average list price in the Production.Product table. This should be grouped independently over the Color and the Class column. 
--Exclude the rows where Color or Class are null.

	select Color, Class, avg(ListPrice) as "AverageListPrice"
	from Production.Product
	where Color is not null and Class is not Null
	group by Color, Class

--12.Write a query that lists the country and province names from person. CountryRegion and person. StateProvince tables. 
--Join them and produce a result set similar to the following

	select
		c.Name as Country, 
		s.Name as Province

	from Person.CountryRegion c
	JOIN Person.StateProvince s
	ON c.CountryRegionCode = s.CountryRegionCode


--13.Write a query that lists the country and province names from person. CountryRegion and person. 
--StateProvince tables and list the countries filter them by Germany and Canada. Join them and produce a result set similar to the following.

	select
		c.Name as Country, 
		s.Name as Province

	from Person.CountryRegion c
		
	JOIN Person.StateProvince s
	ON c.CountryRegionCode = s.CountryRegionCode
	where c.Name in ('Germany', 'Canada')


-- Using Northwnd Database: (Use aliases for all the Joins)
--14.List all Products that has been sold at least once in last 25 years.

USE Northwnd;

	select * from dbo.Products
	select * from dbo.Orders
	select * from dbo.[Order Details]

	select DISTINCT p.ProductID, p.ProductName
	from dbo.Products p
	JOIN dbo.[Order Details] od ON p.ProductID = od.ProductID
	JOIN dbo.Orders o ON o.OrderID = od.OrderID
	where o.OrderDate >= DATEADD(YEAR, -50, GETDATE())

	-- using last 25 years returns an empty result,
	-- probabily because Northwnd database is too old

--15.List top 5 locations (Zip Code) where the products sold most.

	select * from dbo.Orders
	select * from dbo.[Order Details]
	select * from dbo.Customers

	select TOP 5
	c.PostalCode as 'Zip Code', SUM(od.Quantity) as TotalQuantitySold
	from dbo.[Order Details] od
	JOIN dbo.Orders o ON o.orderID = od.OrderID
	JOIN dbo.Customers c ON o.CustomerID = c.CustomerID
	where c.PostalCode is not null
	group by c.PostalCode
	order by TotalQuantitySold desc
	



--16.List top 5 locations (Zip Code) where the products sold most in last 25 years.

	select TOP 5
	c.PostalCode as 'Zip Code', SUM(od.Quantity) as TotalQuantitySold
	from dbo.[Order Details] od
	JOIN dbo.Orders o ON o.orderID = od.OrderID
	JOIN dbo.Customers c ON o.CustomerID = c.CustomerID
	where c.PostalCode is not null AND o.OrderDate >= DATEADD(YEAR, -50, GETDATE())  
	group by c.PostalCode
	order by TotalQuantitySold desc

	-- using last 25 years returns an empty result,
	-- probabily because Northwnd database is too old

--17.List all city names and number of customers in that city.    

SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME LIKE '%City%'

	select * from dbo.Customers

	select city, count(*) as 'Number of Customers'
	from dbo.Customers c
	where city is not null
	group by city
	order by 'Number of Customers' desc



--18.List city names which have more than 2 customers, and number of customers in that city

	select city, count(*) as 'Number of Customers'
	from dbo.Customers c
	where city is not null 
	group by city
	having count(*) > 2
	order by 'Number of Customers' desc

--19.List the names of customers who placed orders after 1/1/98 with order date.

	select * from dbo.Orders
	select * from dbo.Customers

	select c.ContactName as 'Customer Name', o.OrderDate
	from dbo.Customers c
	JOIN dbo.Orders o ON c.CustomerID = o.CustomerID
	where o.OrderDate > '1998-01-01'


--20.List the names of all customers with most recent order dates

SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME LIKE '%Order%'

	select * from dbo.Orders
	select * from dbo.Customers

	select c.ContactName, MAX(o.OrderDate) as 'MostRecentOrder'
	from dbo.Customers c 
	JOIN dbo.Orders o ON c.CustomerID = o.CustomerID
	group by c.ContactName
	order by MostRecentOrder desc

--21.Display the names of all customers  along with the  count of products they bought


	select * from dbo.Orders
	select * from dbo.[Order Details]
	select * from dbo.Customers

	SELECT c.ContactName, COUNT(od.ProductID) AS ProductCount
	FROM dbo.Customers c
	JOIN dbo.Orders o ON c.CustomerID = o.CustomerID
	JOIN dbo.[Order Details] od ON o.OrderID = od.OrderID
	GROUP BY c.ContactName
	ORDER BY ProductCount DESC;



--22.Display the customer ids who bought more than 100 Products with count of products.

	select c.CustomerID, SUM(od.Quantity) AS TotalQuantity
	from dbo.Customers c
	JOIN dbo.Orders o ON c.CustomerID = o.CustomerID
	JOIN dbo.[Order Details] od ON o.OrderID = od.OrderID
	group by c.CustomerID
	having SUM(od.Quantity) > 100
	order by TotalQuantity desc




--23.Show all the possible combinations of suppliers and shippers, representing every way a supplier can ship its products.
	The result should display two columns:

	Supplier CompanyName， Shipper CompanyName

SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME LIKE '%supp%'

	select * from dbo.Suppliers
	select * from dbo.Shippers

	select s.CompanyName as Supplier, sh.CompanyName as Shipper
	from dbo.Suppliers s
	CROSS JOIN dbo.Shippers sh


--24.Display the products order each day. Show Order date and Product Name.

	select * from dbo.Orders
	select * from dbo.[Order Details]
	select * from dbo.Products

	select o.OrderDate, p.ProductName
	from dbo.Orders o
	JOIN dbo.[Order Details] od ON o.OrderID = od.OrderID
	JOIN dbo.Products p ON od.ProductID = p.ProductID
	order by o.OrderDate


--25.Displays pairs of employees who have the same job title.

	select * from dbo.Employees

	SELECT 
		e1.FirstName + ' ' + e1.LastName AS Employee1,
		e2.FirstName + ' ' + e2.LastName AS Employee2,
		e1.Title
	FROM dbo.Employees e1
	JOIN dbo.Employees e2 
		ON e1.Title = e2.Title 
		AND e1.EmployeeID < e2.EmployeeID
	ORDER BY e1.Title;



--26.Display all the Managers who have more than 2 employees reporting to them.

	SELECT 
		m.EmployeeID AS ManagerID,
		m.FirstName + ' ' + m.LastName AS ManagerName,
		COUNT(e.EmployeeID) AS NumberOfReports
	FROM dbo.Employees m
	JOIN dbo.Employees e ON m.EmployeeID = e.ReportsTo
	GROUP BY m.EmployeeID, m.FirstName, m.LastName
	HAVING COUNT(e.EmployeeID) > 2;



--27.List all customers and suppliers together, grouped by city.
The result should display the following columns:

City，CompanyName，ContactName，Type (indicating whether the record is a Customer or a Supplier).


SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME LIKE '%City%'

	select * from dbo.Suppliers
	select * from dbo.Customers
	select * from [Customer and Suppliers by City]

	SELECT 
		City,
		CompanyName,
		ContactName,
		'Customer' AS Type
	FROM dbo.Customers

	UNION

	SELECT 
		City,
		CompanyName,
		ContactName,
		'Supplier' AS Type
	FROM dbo.Suppliers
	ORDER BY City;


