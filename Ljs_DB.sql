IF Exists (SELECT 1 FROM master.dbo.sysdatabases WHERE NAME = 'Ljs_DB')
BEGIN
	DROP DATABASE Ljs_DB
  print '' print '*** Dropping database Ljs_DB ***'
END
GO

print '' print '*** Creating database Ljs_DB ***'
GO
CREATE DATABASE Ljs_DB
GO

print '' print '*** Using database Ljs_DB ***'
GO
USE [Ljs_DB]
GO

print '' print '*** Creating employee table ***'
GO
CREATE TABLE[dbo].[employee](
	[EmployeeID]		[int] IDENTITY(100000, 1) NOT NULL,
	[Email]					[nvarchar](100)	NOT NULL,
	[FirstName]			[nvarchar](50)	NOT NULL,
	[LastName]			[nvarchar](100)	NOT NULL,
	[PhoneNumber]		[nvarchar](15)	NOT NULL,
	[PasswordHash]	[nvarchar](100)	NOT NULL DEFAULT 
	'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]				[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_employeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_email] UNIQUE([Email] ASC)
)
GO

print '' print '*** Creating product table ***'
GO
CREATE TABLE[dbo].[product](
	[ProductID]		[int] IDENTITY(100000, 1) NOT NULL,
	[ProductName]	[nvarchar]	(100) NOT NULL,
	[Vendor]		[nvarchar](250)	NOT NULL,
	[ProductType]	[nvarchar](50)	NOT NULL,
	[BuyPrice]		[decimal] (19,4) NOT NULL,
	[SalePrice]		[decimal] (19,4) NOT NULL,
	[Quantity]		[int]			NOT NULL,
	[Active]		[bit] DEFAULT 1
	CONSTRAINT [pk_ProductID] PRIMARY KEY([ProductID] ASC)
)
GO

print '' print '*** Creating product test data ***'
GO
INSERT INTO [dbo].[product]
		([ProductName], [Vendor], [ProductType], [BuyPrice], [SalePrice], [Quantity])
	VALUES
		('TenderLoin', 'Tysons', 'Meat', 18.37, 25.00, 10),
		('Roast Beef', 'Tysons', 'Meat', 10.42, 15.00, 20),
		('Turkey', 'Tysons', 'Meat', 7.42, 13.00, 10),
		('AmericanCheese', 'Tysons', 'Cheese', 2.32, 3.00, 40),
		('Sister Shubert Rolls', 'Tysons', 'Bread', 0.25, 0.50, 20),
		('Red Onions', 'Tysons', 'Vegetables', 0.25, 0.50, 20),
		('Green Onions', 'Tysons', 'Vegetables', 0.25, 0.50, 20)
		
GO

print '' print '*** Creating Employee test data ***'
GO
INSERT INTO [dbo].[employee]
		([Email], [FirstName], [LastName], [PhoneNumber])
	VALUES
		('Roxanne@cateringbyljs.com', 'Roxanne', 'Braeden', '3193776647'),
		('Tim@cateringbyljs.com', 'Tim', 'Loesel', '3193776648'),
		('Amy@cateringbyljs.com', 'Amy', 'Garringer', '3193776649'),
		('Tina@cateringbyljs.com', 'Tina', 'Blount', '3193776657'),
		('Carrey@cateringbyljs.com', 'Carrey', 'Blount', '3193776658'),
		('Melanie@cateringbyljs.com', 'Melanie', 'Loesel', '3193776659'),
		('Mike@cateringbyljs.com', 'Mike', 'Johnson', '3193776660')
		
GO

print '' print '*** Creating Role table ***'
GO
CREATE TABLE [dbo].[Role](
	[RoleID]		[nvarchar](25) NOT NULL,
	[Description] 	[nvarchar] (250) NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([ROLEID] ASC)
)
GO

print '' print '*** Creating Role test data ***'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Admin', 'User administrator'),
		('Manager', 'Inventory Manager'),
		('Employee', 'An employee')
		
GO

print '' print '*** Creating Employee Role table ***'
GO
CREATE TABLE [dbo].[EmployeeRole](
	[EmployeeID]		[int]			NOT NULL,
	[RoleID]			[nvarchar](25)	NOT NULL,
	CONSTRAINT [pk_employeeID_roleID] PRIMARY KEY([EmployeeID], [RoleID]),
	CONSTRAINT [fk_employeeID] FOREIGN KEY([EmployeeID]) 
		REFERENCES [dbo].[Employee]([EmployeeID])
)
GO

print '' print '*** Adding FK to Employee table ***'
GO
ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK
	ADD CONSTRAINT [fk_roleID] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID])
		ON UPDATE CASCADE
GO

print '' print '*** Creating Employee Role test data ***'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleId])
	VALUES
		(100000, 'Admin'),
		(100000, 'Manager'),
		(100001, 'Employee')

GO

print '' print '*** Creating Customer table ***'
GO 
CREATE TABLE [dbo].[Customer](
	[CustomerID]	[int] IDENTITY(100000,1)NOT NULL,
	[BusinessName]	[nvarchar] (50) NULL,
	[CustomerFirstName]	[nvarchar] (50) NOT NULL,
	[CustomerLastName]  [nvarchar] (50) NOT NULL,
	[CustomerEmail]		[nvarchar] (100) NOT NULL,
	[CustomerPhoneNumber] [nvarchar] (15) NOT NULL,
	[Active] 			[bit] NOT NULL DEFAULT 1,
	CONSTRAINT [pk_CustomerID] PRIMARY KEY([CustomerID] ASC)
)
GO

print '' print '*** Creating Customer test data ***'
GO
INSERT INTO [dbo].[Customer]
		([BusinessName],[CustomerFirstName],[CustomerLastName],[CustomerEmail],[CustomerPhoneNumber])
	VALUES
		('RockWell', 'John', 'Ingred', 'John@Rockwell.com', '3194825371'),
		('Cedar Rapids Schools', 'James', 'Cameron', 'James@CedarRapidsSchools.com', '3198749617'),
		('Kirkwood', 'john', 'smith', 'John@Kirkwood.edu', '3195318697'),
		('Marion Schools', 'Kristie', 'Varnett', 'Kristie@MarionSchools.com', '3198749617')
GO

print '' print '*** Creating Order table ***'        
GO
CREATE TABLE [dbo].[OrderTable](
	[OrderID]		[int] IDENTITY(100000, 1) NOT NULL,
	[OrderDate] 	[DATETIME] NOT NULL,
	[Active]		[bit] DEFAULT 1,
	CONSTRAINT [pk_OrderID] PRIMARY KEY([OrderID] ASC)
)
GO

print '' print '*** Creating Order test data ***'
GO
INSERT INTO [dbo].[OrderTable]
		([OrderDate])
	VALUES
		('2020-02-12 12:34:00'),
		('2020-03-04 18:52:00'),
		('2020-05-09 09:01:00')
		
GO

print '' print '*** Creating Customer Order table ***'
GO
CREATE TABLE [dbo].[CustomerOrder](
	[CustomerID]		[int]			NOT NULL,
	[OrderID]			[int]			NOT NULL,
	CONSTRAINT [pk_CustomerID_OrderID] PRIMARY KEY([CustomerID], [OrderID]),
	CONSTRAINT [fk_CustomerID] FOREIGN KEY([CustomerID]) 
		REFERENCES [dbo].[Customer]([CustomerID])
)
GO

print '' print '*** Adding FK to Order table ***'
GO
ALTER TABLE [dbo].[CustomerOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_OrderID] FOREIGN KEY([OrderID])
		REFERENCES [dbo].[OrderTable]([OrderID])
		ON UPDATE CASCADE
GO

print '' print '*** Creating Customer Order test data ***'
GO
INSERT INTO [dbo].[CustomerOrder]
		([CustomerID], [OrderID])
	VALUES
		(100000, 100000),
		(100001, 100001),
		(100002, 100002)

GO


print '' print '*** USER PROCEDURES FOR USERS ***'
GO


print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT(Email)
		FROM Employee
		WHERE Email = @Email
		AND PasswordHash = @PasswordHash
		AND Active = 1
	
	END
GO

print '' print '*** creating sp_update_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash]
	(
	@Email			[nvarchar](100),
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE Employee
			SET PasswordHash = @NewPasswordHash
			WHERE Email = @Email
			AND PasswordHash = @OldPasswordHash
			RETURN @@ROWCOUNT
	
	END
GO

print '' print '*** creating sp_update_employee_profile ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_profile]
	(
	@EmployeeID		[int],
	@NewEmail		[nvarchar] (100),
	@NewFirstName	[nvarchar] (50),
	@NewLastName	[nvarchar] (10),
	@NewPhoneNumber [nvarchar] (15),
	
	@OldPhoneNumber [nvarchar] (15),
	@OldEmail  		[nvarchar] (100),
	@OldFirstName	[nvarchar] (100),
	@OldLastName	[nvarchar] (100)
	)
AS
	BEGIN
		UPDATE Employee
			SET Email = @NewEmail,
				FirstName = @NewFirstName,
				LastName = @NewLastName,
				PhoneNumber = @NewPhoneNumber
			WHERE EmployeeID = @EmployeeID
			AND Email = @OldEmail
			AND FirstName = @OldFirstName
			AND LastName = @OldLastName
			AND PhoneNumber = @OldPhoneNumber
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_roles_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_roles_by_employeeID]
	(
	@EmployeeID			[int]
	)
AS
	BEGIN
		SELECT 	RoleID
		FROM 	EmployeeRole
		WHERE	EmployeeID = @EmployeeID
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
	@Email			[nvarchar](100)
	)
AS
	BEGIN
		SELECT EmployeeID, Email, FirstName, LastName, PhoneNumber, Active
		From Employee
		WHERE Email = @Email	
	END
GO

print '' print '*** USER PROCEDURES FOR ADMINS ***'
GO

print '' print '*** creating sp_insert_new_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user]
(
	@Email				[nvarchar](100),
	@FirstName			[nvarchar](50),	
	@LastName			[nvarchar](100),
	@PhoneNumber		[nvarchar](15)
)
AS
	BEGIN
		INSERT INTO [dbo].[employee]
			([Email], [FirstName], [LastName], [PhoneNumber])
		VALUES
			(@Email, @FirstName, @LastName, @PhoneNumber)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_select_all_employees ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
	BEGIN
		SELECT EmployeeID, Email, FirstName, LastName, PhoneNumber, Active
		FROM Employee
		ORDER BY LastName ASC
	END
GO

print '' print '*** creating sp_select_employees_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employees_by_active]
(
	@Active 	[bit]
)
AS
	BEGIN
		SELECT EmployeeID, Email, FirstName, LastName, PhoneNumber, Active
		FROM Employee
		WHERE Active = @Active
		ORDER BY LastName ASC
	END
GO

print '' print '*** creating sp_select_products_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_products_by_active]
(
	@Active 	[bit]
)
AS
	BEGIN
		SELECT ProductID, ProductName, Vendor, ProductType, BuyPrice, SalePrice,Quantity, Active
		FROM product
		WHERE Active = @Active
		ORDER BY ProductName ASC
	END
GO

print '' print '*** creating sp_select_customers_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_customers_by_active]
(
	@Active 	[bit]
)
AS
	BEGIN
		SELECT CustomerID, BusinessName, CustomerFirstName, CustomerLastName, CustomerEmail, CustomerPhoneNumber, Active
		FROM Customer
		WHERE Active = @Active
		ORDER BY CustomerLastName ASC
	END
GO

print '' print '*** creating sp_select_employee_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_id]
	(
	@EmployeeID			[int]
	)
AS
	BEGIN
		SELECT EmployeeID, Email, FirstName, LastName, PhoneNumber, Active
		From Employee
		WHERE EmployeeID = @EmployeeID	
	END
GO

print '' print '*** creating sp_reset_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_reset_passwordhash]
	(
	@Email			[nvarchar](100)
	)
AS
	BEGIN
		UPDATE Employee
			SET PasswordHash = 
			'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E'
			WHERE Email = @Email
			RETURN @@ROWCOUNT
	
	END
GO

print '' print '*** creating sp_reactivate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_employee]
	(
	@EmployeeID			[int]
	)
AS
	BEGIN
		UPDATE Employee
			SET Active = 1
			WHERE EmployeeID = @EmployeeID
			RETURN @@ROWCOUNT
	
	END
GO

print '' print '*** creating_sp_select_all_employee_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_employee_roles]
AS
	BEGIN
		SELECT ROLEID
		FROM ROLE
		ORDER BY ROLEID ASC
	END
GO

print '' print '*** creating sp_add_employee_role ***'
GO
CREATE PROCEDURE [dbo].[sp_add_employeerole]
	(
		@EmployeeID	[int],
		@RoleID		[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO EmployeeRole
			([EmployeeID], [RoleID])
		  VALUES
			(@EmployeeID, @RoleID)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating_sp_safely_remove_employeerole***'
GO
CREATE PROCEDURE [dbo].[sp_safely_remove_employeerole]
	(
		@EmployeeID	[int],
		@RoleId		[nvarchar](25)
	)
AS
	BEGIN
		DECLARE @admins int;
		SELECT @Admins = Count(RoleID)
		FROM EmployeeRole
		WHERE ROLEID = 'Admin';
		
		IF @RoleID = 'Admin' AND @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				DELETE FROM EmployeeRole
					WHERE EmployeeID = @EmployeeID
						AND RoleID = @RoleID
				RETURN @@ROWCOUNT
			END
	END
GO

print '' print '*** creating sp_safely_deactivate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_deactivate_employee]
	(
	@EmployeeID			[int]
	)
AS
	BEGIN
		DECLARE @admins int;
		SELECT @Admins = Count(RoleID)
		FROM EmployeeRole
		WHERE ROLEID = 'Admin'
		AND EmployeeRole.EmployeeID = @EmployeeID
		
		IF @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				UPDATE Employee
					SET Active = 0
					WHERE Employee.EmployeeID = @EmployeeID
				RETURN @@ROWCOUNT
			END
	END
GO

print '' print '*** creating sp_insert_new_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_customer]
	(
	 @BusinessName	[nvarchar] (50),
	 @CustomerFirstName	[nvarchar] (50),
	 @CustomerLastName  [nvarchar] (50),
	 @CustomerEmail	[nvarchar] (100),
	 @CustomerPhoneNumber [nvarchar] (15)
	)
AS
	BEGIN
		INSERT INTO Customer
			([BusinessName], [CustomerFirstName], [CustomerLastName], [CustomerEmail], [CustomerPhoneNumber])
		  VALUES
			(@BusinessName, @CustomerFirstName, @CustomerLastName, @CustomerEmail, @CustomerPhoneNumber)
		Select SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_select_all_customers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_customers]
AS
	BEGIN
		SELECT CustomerID, BusinessName, CustomerFirstName, CustomerLastName, CustomerEmail, CustomerPhoneNumber, Active
		FROM Customer
		ORDER BY CustomerLastName ASC
	END
GO

print '' print '*** creating sp_update_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_update_customer]
	(
	@CustomerID				[int],
	@NewBusinessName		[nvarchar] (50),
	@NewCustomerFirstName	[nvarchar] (50),
	@NewCustomerLastName	[nvarchar] (50),
	@NewCustomerEmail 		[nvarchar] (100),
	@NewCustomerPhoneNumber [nvarchar] (15),
	
	@OldCustomerBusinessName[nvarchar] (50),
	@OldCustomerFirstName  	[nvarchar] (50),
	@OldCustomerLastName	[nvarchar] (100),
	@OldCustomerEmail		[nvarchar] (100),
	@OldCustomerPhoneNumber	[nvarchar] (15)
	
	)
AS
	BEGIN
		UPDATE Customer
			SET BusinessName = @NewBusinessName,
				CustomerFirstName = @NewCustomerFirstName,
				CustomerLastName = @NewCustomerLastName,
				CustomerEmail = @NewCustomerEmail,
				CustomerPhoneNumber = @NewCustomerPhoneNumber
			WHERE CustomerID = @CustomerID
			AND BusinessName = @OldCustomerBusinessName
			AND CustomerFirstName = @OldCustomerFirstName
			AND CustomerLastName = @OldCustomerLastName
			AND CustomerEmail = @OldCustomerEmail
			AND CustomerPhoneNumber = @OldCustomerPhoneNumber
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** creting sp_deactivate_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_customer]
	(
	 @CustomerID [int]
	)
AS
	BEGIN
		UPDATE Customer
			SET Active = 0
			WHERE Customer.CustomerID = @CustomerID
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_new_order ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_order]
	(
	 @OrderDate	[datetime]
	 
	)
AS
	BEGIN
		INSERT INTO OrderTable
			([OrderDate])
		VALUES
			( @OrderDate)
		Select SCOPE_IDENTITY()
		END
GO

print '' print '*** creating sp_select_all_orders ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_orders]
AS
	BEGIN
		SELECT OrderID, OrderDate
		FROM OrderTable
		ORDER BY OrderDate ASC
	END
GO

print '' print '*** creating sp_update_order ***'
GO
CREATE PROCEDURE [dbo].[sp_update_order]
	(
	@OrderID				[int],
	@NewOrderDate			[datetime],

	
	@OldOrderDate 			[datetime],
	@Active					[bit]
	
	)
AS
	BEGIN
		UPDATE OrderTable
			SET OrderDate = @NewOrderDate
			WHERE OrderID = @OrderID
			AND OrderDate = @OldOrderDate
			AND @Active = 1
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** creting sp_deactivate_order ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_order]
	(
	 @OrderID [int]
	)
AS
	BEGIN
		UPDATE OrderTable
			SET Active = 0
			WHERE OrderTable.OrderID = @OrderID
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_new_product ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_product]
	(
	 @ProductName		[nvarchar] (100),
	 @Vendor			[nvarchar] (250),
	 @ProductType		[nvarchar] (50),
	 @BuyPrice  		[decimal] (19,4),
	 @SalePrice			[decimal] (19,4),
	 @Quantity [int]
	)
AS
	BEGIN
		INSERT INTO product
			( [ProductName], [Vendor], [ProductType], [BuyPrice], [SalePrice], [Quantity])
		  VALUES
			(@ProductName, @Vendor, @ProductType, @BuyPrice, @SalePrice, @Quantity)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_select_all_products ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_products]
AS
	BEGIN
		SELECT ProductID, ProductName, Vendor, ProductType, BuyPrice, SalePrice, Quantity
		FROM product
		ORDER BY ProductName ASC
	END
GO

print '' print '*** creating sp_update_product ***'
GO
CREATE PROCEDURE [dbo].[sp_update_product]
	(
	@ProductID				[int],
	@NewProductName			[nvarchar] (100),
	@NewVendor				[nvarchar] (250),
	@NewProductType			[nvarchar] (50),
	@NewBuyPrice 			[decimal]  (19,4),
	@NewSalePrice			[decimal]  (19,4),
	@NewQuantity			[int],

	
	@OldProductName			[nvarchar] (100),
	@OldVendor				[nvarchar] (250),
	@OldProductType			[nvarchar] (50),
	@OldBuyPrice 			[decimal]  (19,4),
	@OldSalePrice			[decimal]  (19,4),
	@OldQuantity			[int]	
	)
AS
	BEGIN
		UPDATE product
			SET ProductName = @NewProductName,
				Vendor = @NewVendor,
				ProductType = @NewProductType,
				BuyPrice = @NewBuyPrice,
				SalePrice = @NewSalePrice,
				Quantity = @NewQuantity
			WHERE ProductID = @ProductID
			AND Quantity = @OldQuantity
			AND SalePrice = @OldSalePrice
			AND BuyPrice = @OldBuyPrice
			AND ProductType = @OldProductType
			AND Vendor = @OldVendor
			AND ProductName = @OldProductName
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_deactivate_product ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_product]
	(
	 @ProductID [int]
	)
AS
	BEGIN
		UPDATE product
			SET Active = 0
			WHERE Product.ProductID = @ProductID
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_reactivate_product ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_product]
	(
	@ProductID			[int]
	)
AS
	BEGIN
		UPDATE Product
			SET Active = 1
			WHERE ProductID = @ProductID
			RETURN @@ROWCOUNT
	
	END
GO

print '' print '*** creating sp_reactivate_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_customer]
	(
	@CustomerID			[int]
	)
AS
	BEGIN
		UPDATE Customer
			SET Active = 1
			WHERE CustomerID = @CustomerID
			RETURN @@ROWCOUNT
	
	END
GO

print '' print '*** creating sp_select_orders_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_orders_by_active]
(
	@Active 	[bit]
)
AS
	BEGIN
		SELECT OrderID, OrderDate, Active
		FROM OrderTable
		WHERE Active = @Active
		ORDER BY OrderDate ASC
	END
GO

print '' print '*** creating sp_select_orders_from_customerID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_orders_from_customerID]
	(
	@CustomerID			[int]
	)
AS
	BEGIN
		SELECT 	OrderID
		FROM 	CustomerOrder
		WHERE	CustomerID = @CustomerID
	END
GO

print '' print '*** creating sp_select_order_by_orderID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_order_by_orderID]
	(
	@OrderID			[int]
	)
AS
	BEGIN
		SELECT 	OrderID, OrderDate, Active
		FROM 	OrderTable
		WHERE	OrderID = @OrderID
	END
GO

print '' print '*** creating sp_add_customer_order ***'
GO
CREATE PROCEDURE [dbo].[sp_add_customer_order]
	(
		@CustomerID	[int],
		@OrderID	[int]
	)
AS
	BEGIN
		INSERT INTO CustomerOrder
			([CustomerID], [OrderID])
		  VALUES
			(@CustomerID, @OrderID)
		RETURN @@ROWCOUNT
	END
GO

