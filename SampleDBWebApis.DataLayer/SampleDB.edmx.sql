
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/11/2020 16:38:03
-- Generated from EDMX file: C:\Projects\SampleDBWebApis\SampleDBWebApis.DataLayer\SampleDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SampleDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Orders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Orders];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_Customers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[OrderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetails];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[SAMPLEDBModelStoreContainer].[CustomersOrderTotalByQuarter]', 'U') IS NOT NULL
    DROP TABLE [SAMPLEDBModelStoreContainer].[CustomersOrderTotalByQuarter];
GO
IF OBJECT_ID(N'[SAMPLEDBModelStoreContainer].[OrdersByProductID]', 'U') IS NOT NULL
    DROP TABLE [SAMPLEDBModelStoreContainer].[OrdersByProductID];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductID] int IDENTITY(1,1) NOT NULL,
    [ProductName] nvarchar(40)  NOT NULL,
    [SupplierID] int  NULL,
    [CategoryID] int  NULL,
    [QuantityPerUnit] nvarchar(20)  NULL,
    [UnitPrice] decimal(19,4)  NULL,
    [UnitsInStock] smallint  NULL,
    [UnitsOnOrder] smallint  NULL,
    [ReorderLevel] smallint  NULL,
    [Discontinued] bit  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerID] nchar(5)  NOT NULL,
    [CompanyName] nvarchar(40)  NOT NULL,
    [ContactName] nvarchar(30)  NULL,
    [ContactTitle] nvarchar(30)  NULL,
    [Address] nvarchar(60)  NULL,
    [City] nvarchar(15)  NULL,
    [Region] nvarchar(15)  NULL,
    [PostalCode] nvarchar(10)  NULL,
    [Country] nvarchar(15)  NULL,
    [Phone] nvarchar(24)  NULL,
    [Fax] nvarchar(24)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] nchar(5)  NULL,
    [EmployeeID] int  NULL,
    [OrderDate] datetime  NULL,
    [RequiredDate] datetime  NULL,
    [ShippedDate] datetime  NULL,
    [ShipVia] int  NULL,
    [Freight] decimal(19,4)  NULL,
    [ShipName] nvarchar(40)  NULL,
    [ShipAddress] nvarchar(60)  NULL,
    [ShipCity] nvarchar(15)  NULL,
    [ShipRegion] nvarchar(15)  NULL,
    [ShipPostalCode] nvarchar(10)  NULL,
    [ShipCountry] nvarchar(15)  NULL
);
GO

-- Creating table 'OrdersByProductIDs'
CREATE TABLE [dbo].[OrdersByProductIDs] (
    [ProductID] int  NOT NULL,
    [ProductName] nvarchar(40)  NOT NULL,
    [OrderDate] datetime  NULL,
    [Quantity] smallint  NOT NULL
);
GO

-- Creating table 'CustomersOrderTotalByQuarters'
CREATE TABLE [dbo].[CustomersOrderTotalByQuarters] (
    [CustomerID] nchar(5)  NOT NULL,
    [CompanyName] nvarchar(40)  NOT NULL,
    [Year] int  NULL,
    [Quarter] int  NULL,
    [TotalValue] float  NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [OrderID] int  NOT NULL,
    [ProductID] int  NOT NULL,
    [UnitPrice] decimal(19,4)  NOT NULL,
    [Quantity] smallint  NOT NULL,
    [Discount] real  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [CustomerID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerID] ASC);
GO

-- Creating primary key on [OrderID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderID] ASC);
GO

-- Creating primary key on [ProductID], [ProductName], [Quantity] in table 'OrdersByProductIDs'
ALTER TABLE [dbo].[OrdersByProductIDs]
ADD CONSTRAINT [PK_OrdersByProductIDs]
    PRIMARY KEY CLUSTERED ([ProductID], [ProductName], [Quantity] ASC);
GO

-- Creating primary key on [CustomerID], [CompanyName] in table 'CustomersOrderTotalByQuarters'
ALTER TABLE [dbo].[CustomersOrderTotalByQuarters]
ADD CONSTRAINT [PK_CustomersOrderTotalByQuarters]
    PRIMARY KEY CLUSTERED ([CustomerID], [CompanyName] ASC);
GO

-- Creating primary key on [OrderID], [ProductID] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([OrderID], [ProductID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Customers]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[Customers]
        ([CustomerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_Customers'
CREATE INDEX [IX_FK_Orders_Customers]
ON [dbo].[Orders]
    ([CustomerID]);
GO

-- Creating foreign key on [OrderID] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Orders]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[Orders]
        ([OrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductID] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Products]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Products'
CREATE INDEX [IX_FK_OrderDetails_Products]
ON [dbo].[OrderDetails]
    ([ProductID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------