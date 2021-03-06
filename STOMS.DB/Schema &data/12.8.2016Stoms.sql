USE [STOMS2]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'

GO
/****** Object:  StoredProcedure [dbo].[sspSaveOrder]    Script Date: 8/12/2016 6:31:14 PM ******/
DROP PROCEDURE [dbo].[sspSaveOrder]
GO
/****** Object:  StoredProcedure [dbo].[sspGetGroup]    Script Date: 8/12/2016 6:31:14 PM ******/
DROP PROCEDURE [dbo].[sspGetGroup]
GO
/****** Object:  View [dbo].[svwSampleTestTrack]    Script Date: 8/12/2016 6:31:14 PM ******/
DROP VIEW [dbo].[svwSampleTestTrack]
GO
/****** Object:  Table [dbo].[stblSettings]    Script Date: 8/12/2016 6:31:14 PM ******/
DROP TABLE [dbo].[stblSettings]
GO
/****** Object:  Table [dbo].[stblOrder]    Script Date: 8/12/2016 6:31:14 PM ******/
DROP TABLE [dbo].[stblOrder]
GO
/****** Object:  Table [dbo].[stblAssayGroup]    Script Date: 8/12/2016 6:31:14 PM ******/
DROP TABLE [dbo].[stblAssayGroup]
GO
/****** Object:  Table [dbo].[stblAssayGroup]    Script Date: 8/12/2016 6:31:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stblAssayGroup](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](100) NULL,
	[GroupDescription] [nvarchar](100) NULL,
	[CreateOnTime] [datetime] NOT NULL,
	[completedTime] [datetime] NULL,
	[sampleCount] [int] NULL,
	[Status] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stblOrder]    Script Date: 8/12/2016 6:31:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblOrder](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNum] [varchar](20) NULL,
	[OrderStatus] [varchar](30) NULL,
	[CustID] [int] NULL,
	[Discount] [decimal](8, 2) NULL CONSTRAINT [DF_stblOrder_discount]  DEFAULT ((0)),
	[DiscountType] [varchar](30) NULL,
	[TotOrderCost] [decimal](8, 2) NULL,
	[NetOrderTotal] [decimal](8, 2) NULL,
	[BName] [varchar](30) NULL,
	[BAddress1] [varchar](30) NULL,
	[BAddress2] [varchar](30) NULL,
	[BCity] [varchar](30) NULL,
	[BState] [varchar](50) NULL,
	[BZipCode] [varchar](20) NULL,
	[BCountry] [varchar](50) NULL,
	[SName] [varchar](30) NULL,
	[SAddress1] [varchar](30) NULL,
	[SAddress2] [varchar](30) NULL,
	[SCity] [varchar](30) NULL,
	[SState] [varchar](50) NULL,
	[SZipCode] [varchar](20) NULL,
	[SCountry] [varchar](50) NULL,
	[PaymentStatus] [varchar](30) NULL,
	[ShipOption] [varchar](50) NULL,
	[DeliveryEmail] [varchar](255) NULL,
	[GenOrderCustPhone] [varchar](30) NULL,
	[GenOrderCustName] [varchar](50) NULL,
	[ClientDiscountTier] [smallint] NULL,
	[isBindTest] [bit] NULL,
	[isBlockTest] [bit] NULL,
	[OrderDate] [date] NULL,
	[OrderCompletedOn] [datetime] NULL,
	[CompletedBy] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[GroupID] [int] NULL,
 CONSTRAINT [PK_tblOrder] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblSettings]    Script Date: 8/12/2016 6:31:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblSettings](
	[SettingID] [int] IDENTITY(1,1) NOT NULL,
	[OrderPrefix] [varchar](3) NULL,
	[SamplePrefix] [varchar](3) NULL,
	[OrderPrefixYear] [smallint] NULL,
	[OrderSrNumber] [int] NULL,
	[BarcodeType] [varchar](20) NULL,
	[BarcodeField] [varchar](30) NULL,
	[InvPrefix] [varchar](3) NULL,
	[AssayMin] [int] NULL,
	[AssayMax] [int] NULL,
	[GroupPrefix] [varchar](5) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[svwSampleTestTrack]    Script Date: 8/12/2016 6:31:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[svwSampleTestTrack]
AS
SELECT        dbo.stblSampleTestTrack.TTrackID, dbo.stblSampleTestTrack.SampleBarCode, dbo.stblSampleTestTrack.DateAssayBIN, dbo.stblSampleTestTrack.DateAssayBLO, dbo.stblSampleTestTrack.ResultBIN, 
                         dbo.stblSampleTestTrack.ResultBL, dbo.stblSampleTestTrack.ResultSentDate, dbo.stblSampleTestTrack.SampleStatus, dbo.stblSampleTestTrack.BINCost, dbo.stblSampleTestTrack.BLOCost, 
                         dbo.stblSampleTestTrack.DateDrawn, dbo.stblSampleTestTrack.DateReceived, dbo.stblSampleTestTrack.PatientAge, dbo.stblSampleTestTrack.BindingNeg, dbo.stblSampleTestTrack.BindingBL, 
                         dbo.stblSampleTestTrack.BindingPos, dbo.stblSampleTestTrack.BlockingNeg, dbo.stblSampleTestTrack.BlockingBL, dbo.stblSampleTestTrack.BlockingPos, dbo.stblSampleTestTrack.PtsNeg, 
                         dbo.stblSampleTestTrack.PtsAnyPos, dbo.stblSampleTestTrack.PtsBL, dbo.stblSampleTestTrack.PtsBothPos, dbo.stblSampleTestTrack.PtspBL, dbo.stblSampleTestTrack.ResultPos, 
                         dbo.stblSampleTestTrack.ResultNeg, dbo.stblSampleTestTrack.PatientID, dbo.stblPatient.PatientName, dbo.stblPatient.Gender, dbo.stblPatient.DOB, dbo.stblPatient.Diagnosis, dbo.stblPatient.OrderID, 
                         dbo.stblOrder.OrderNum, dbo.stblCustomer.CustomerName, dbo.stblOrder.OrderNum + ' - ' + dbo.stblCustomer.CustomerName AS CustOrderNumber, dbo.stblOrder.isBindTest, dbo.stblOrder.isBlockTest, 
                         dbo.stblOrder.GroupID
FROM            dbo.stblPatient INNER JOIN
                         dbo.stblSampleTestTrack ON dbo.stblPatient.PatientID = dbo.stblSampleTestTrack.PatientID INNER JOIN
                         dbo.stblOrder ON dbo.stblPatient.OrderID = dbo.stblOrder.OrderID INNER JOIN
                         dbo.stblCustomer ON dbo.stblOrder.CustID = dbo.stblCustomer.CustID

GO
SET IDENTITY_INSERT [dbo].[stblAssayGroup] ON 

INSERT [dbo].[stblAssayGroup] ([GroupID], [GroupName], [GroupDescription], [CreateOnTime], [completedTime], [sampleCount], [Status]) VALUES (1, N'GPN001', NULL, CAST(N'1905-07-10 00:00:00.000' AS DateTime), NULL, 20, N'Ready to Testing')
INSERT [dbo].[stblAssayGroup] ([GroupID], [GroupName], [GroupDescription], [CreateOnTime], [completedTime], [sampleCount], [Status]) VALUES (3, N'GPN002', NULL, CAST(N'2015-05-14 12:21:58.987' AS DateTime), NULL, 20, N'completed')
INSERT [dbo].[stblAssayGroup] ([GroupID], [GroupName], [GroupDescription], [CreateOnTime], [completedTime], [sampleCount], [Status]) VALUES (4, N'GPN003', NULL, CAST(N'2015-05-14 12:21:58.987' AS DateTime), NULL, 15, N'Sample Assessment')
SET IDENTITY_INSERT [dbo].[stblAssayGroup] OFF
SET IDENTITY_INSERT [dbo].[stblOrder] ON 

INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (9, N'ORD2000032', N'Day', 10, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), NULL, N'Test1', N'1st street', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'', NULL, NULL, NULL, 1, 1, CAST(N'2015-05-18' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (10, N'IYN2000019', N'Draft', 11, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'Jack Jill', N'George Street', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'USA', NULL, N'Email', N'', NULL, NULL, NULL, 1, 0, CAST(N'2015-02-14' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (11, N'IYN2000020', N'Draft', 12, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'hh', N'hhhj', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'USA', NULL, N'Email', N'', NULL, NULL, NULL, 1, 0, CAST(N'2015-02-14' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (12, N'IYN2000021', N'Draft', 13, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'jkk', N'jj', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'USA', NULL, N'Email', N'', NULL, NULL, NULL, 1, 0, CAST(N'2015-02-14' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (13, N'IYN2000022', N'Draft', 14, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'jdjfj', N'hdjh', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, CAST(N'2015-02-14' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (14, N'IYN2000023', N'Draft', 15, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'hgjfjf', N'jfjfjh', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, CAST(N'2015-02-14' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (15, N'IYN2000024', N'Draft', 16, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'hdhdh', N'hdhdh', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, CAST(N'2015-02-14' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (16, N'IYN2000028', N'Draft', 19, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'cjcj', N'kdjfh', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, CAST(N'2015-02-15' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (17, N'IYN2000029', N'Draft', 20, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'hh', N'hhhj', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, CAST(N'2015-02-15' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (18, N'ORD2000030', N'Awaiting Test Results', 21, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'manoj', N'3,big street', N'', N'new jersy', N'united states', N'1', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'sdf', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-05-14' AS Date), NULL, NULL, 2, CAST(N'2015-05-14 12:21:58.987' AS DateTime), 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (19, N'ORD2000031', N'Awaiting Test Results', 22, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'manoj', N'3,big street', N'', N'new jersy', N'IN', N'1', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'IN', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-05-14' AS Date), NULL, NULL, 2, CAST(N'2015-05-14 12:36:16.803' AS DateTime), 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (20, N'ORD2000032', N'Draft', 23, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'manoj', N'q', N'', N'ser', N'united states', N'1', N'USA', N'manoj', N'q', N'', N'ser', N'united states', N'1', N'USA', NULL, N'Email', N'a@b.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-05-14' AS Date), NULL, NULL, 2, CAST(N'2015-05-14 17:35:34.460' AS DateTime), 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (21, N'ORD2000033', N'Awaiting Test Results', 24, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'manoj', N'3,big street', N'', N'ser', N'', N'', N'USA', N'manoj', N'3,big street', N'', N'ser', N'', N'', N'USA', NULL, N'Email', N'a@b.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-05-14' AS Date), NULL, NULL, 2, CAST(N'2015-05-14 17:37:06.613' AS DateTime), 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (22, N'ORD2000034', N'Awaiting Test Results', 25, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'manoj', N'3,big street', N'', N'new jersy', N'united states', N'1', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'asd', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-05-18' AS Date), NULL, NULL, 2, CAST(N'2015-05-18 15:18:52.510' AS DateTime), 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (23, N'ORD2000035', N'Awaiting Test Results', 26, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'Test', N'q', N'', N'new jersy', N'', N'', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'asd', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-06-01' AS Date), NULL, NULL, 2, CAST(N'2015-06-01 10:49:03.593' AS DateTime), 1)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (24, N'ORD2000036', N'Awaiting Test Results', 27, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'SSS', N'SS', N'', N'', N'', N'', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'23', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-06-01' AS Date), NULL, NULL, 2, CAST(N'2015-06-01 16:07:58.500' AS DateTime), 2)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (25, N'ORD2000037', N'Awaiting Test Results', 28, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'manoj', N'3,big street', N'', N'new jersy', N'tn', N'', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-06-02' AS Date), NULL, NULL, 2, CAST(N'2015-06-02 10:39:34.697' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (26, N'ORD2000038', N'Awaiting Test Results', 29, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'sdf', N'3,big street', N'', N'', N'', N'', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-06-02' AS Date), NULL, NULL, 2, CAST(N'2015-06-02 10:41:12.613' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (27, N'ORD2000039', N'Awaiting Test Results', 30, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'ssssssssss', N'123', N'', N'new jersy', N'united states', N'1', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-06-02' AS Date), NULL, NULL, 2, CAST(N'2015-06-02 10:42:12.150' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (28, N'ORD2000040', N'Draft', 31, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'azdf', N'sdf', N'', N'', N'', N'', N'USA', N'azdf', N'sdf', N'', N'', N'', N'', N'USA', NULL, N'Email', N'sdf', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-09-14' AS Date), NULL, NULL, 2, CAST(N'2015-09-14 21:25:31.397' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (29, N'ORD2000041', N'Draft', 32, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'manoj', N'3,big street', N'', N'new jersy', N'united states', N'1', N'USA', N'manoj', N'3,big street', N'', N'new jersy', N'united states', N'1', N'USA', NULL, N'Email', N'ma.moni71@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-12-28' AS Date), NULL, NULL, 2, CAST(N'2015-12-28 14:11:07.323' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (30, N'ORD2000042', N'Draft', 33, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'manoj', N'3,big street', N'', N'', N'', N'', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'asd', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-12-29' AS Date), NULL, NULL, 2, CAST(N'2015-12-29 18:04:14.033' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (31, N'ORD2000043', N'Awaiting Test Results', 34, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'kumerash.ak', N'pondy', N'', N'', N'', N'', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-07-12' AS Date), NULL, NULL, 1, CAST(N'2016-07-12 14:20:40.787' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (1031, N'ORD2000001', N'Awaiting Test Results', 1034, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'suresh', N'ak street', N'', N'pondy', N'pondy', N'605001', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'suresh@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-07-27' AS Date), NULL, NULL, 1, CAST(N'2016-07-27 12:28:05.983' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2031, N'ORD2000002', N'Awaiting Test Results', 2034, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'suresh@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-02' AS Date), NULL, NULL, 3, CAST(N'2016-08-02 15:13:41.127' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2032, N'ORD2000003', N'Draft', 2035, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'thiru', N'ak street', N'', N'pondy', N'pondy', N'605001', N'USA', N'thiru', N'ak street', N'', N'pondy', N'pondy', N'605001', N'USA', NULL, N'Email', N'damage@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 1, CAST(N'2016-08-03 10:46:43.320' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2033, N'ORD2000004', N'Awaiting Test Results', 2036, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'thiru', N'ak street', N'', N'pondy', N'pondy', N'605001', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'eeee', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 1, CAST(N'2016-08-03 10:55:50.980' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2034, N'ORD2000005', N'Awaiting Test Results', 2037, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', NULL, N'Email', N'akkumerash@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 1, CAST(N'2016-08-03 11:42:51.577' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2035, N'ORD2000006', N'Draft', 2038, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', NULL, N'Email', N'akkumerash@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 1, CAST(N'2016-08-03 11:53:59.813' AS DateTime), 3)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2036, N'ORD2000007', N'Draft', 2039, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', NULL, N'Email', N'akkumerash@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 1, CAST(N'2016-08-03 12:00:55.467' AS DateTime), 4)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2037, N'ORD2000008', N'Draft', 2040, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'thiru', N'ak street', N'', N'gggg', N'pondy', N'605001', N'USA', N'thiru', N'ak street', N'', N'gggg', N'pondy', N'605001', N'USA', NULL, N'Email', N'dijo@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 1, CAST(N'2016-08-03 12:03:59.010' AS DateTime), 4)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (2038, N'ORD2000009', N'Draft', 2037, CAST(0.00 AS Decimal(8, 2)), NULL, NULL, NULL, N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', NULL, N'Email', N'akkumerash@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 1, CAST(N'2016-08-03 12:34:15.860' AS DateTime), 4)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (3031, N'ORD2000010', N'Awaiting Test Results', 2037, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'rrr', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 2, CAST(N'2016-08-03 15:43:06.393' AS DateTime), 4)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (3032, N'ORD2000011', N'Awaiting Test Results', 3034, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'ddddd', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-03' AS Date), NULL, NULL, 2, CAST(N'2016-08-03 15:52:02.847' AS DateTime), 4)
INSERT [dbo].[stblOrder] ([OrderID], [OrderNum], [OrderStatus], [CustID], [Discount], [DiscountType], [TotOrderCost], [NetOrderTotal], [BName], [BAddress1], [BAddress2], [BCity], [BState], [BZipCode], [BCountry], [SName], [SAddress1], [SAddress2], [SCity], [SState], [SZipCode], [SCountry], [PaymentStatus], [ShipOption], [DeliveryEmail], [GenOrderCustPhone], [GenOrderCustName], [ClientDiscountTier], [isBindTest], [isBlockTest], [OrderDate], [OrderCompletedOn], [CompletedBy], [CreatedBy], [CreatedOn], [GroupID]) VALUES (4031, N'ORD2000012', N'Awaiting Test Results', 2038, CAST(0.00 AS Decimal(8, 2)), NULL, CAST(200.00 AS Decimal(8, 2)), CAST(200.00 AS Decimal(8, 2)), N'kumerash.ak', N'pondy', N'', N'pondy', N'pondy', N'605001', N'USA', N'', N'', N'', N'', N'', N'', N'', NULL, N'Email', N'nn@gmail.com', NULL, NULL, NULL, NULL, NULL, CAST(N'2016-08-11' AS Date), NULL, NULL, 2, CAST(N'2016-08-11 12:47:30.503' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[stblOrder] OFF
SET IDENTITY_INSERT [dbo].[stblSettings] ON 

INSERT [dbo].[stblSettings] ([SettingID], [OrderPrefix], [SamplePrefix], [OrderPrefixYear], [OrderSrNumber], [BarcodeType], [BarcodeField], [InvPrefix], [AssayMin], [AssayMax], [GroupPrefix]) VALUES (1, N'ORD', N'ISM', 2016, 13, NULL, NULL, N'INV', 5, 20, NULL)
SET IDENTITY_INSERT [dbo].[stblSettings] OFF
/****** Object:  StoredProcedure [dbo].[sspGetGroup]    Script Date: 8/12/2016 6:31:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sspGetGroup] @Status varchar(100)
AS
BEGIN

If @Status = N'Completed'
  Begin
	SELECT * FROM stblAssayGroup WHERE Status=@Status
  End
else
  Begin
  SELECT * FROM stblAssayGroup WHERE [Status]<>N'Completed'
  End


END

GO
/****** Object:  StoredProcedure [dbo].[sspSaveOrder]    Script Date: 8/12/2016 6:31:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sspSaveOrder] @OrderID int, @orderDate varchar(20), @CustID int, @DeliveryOption varchar(30),
	@DeliveryEmail varchar(255), @SName varchar(30), @SAddress1 varchar(30), @SAddress2 varchar(30), @SCity varchar(30), 
	@SState varchar(50), @SZipCode varchar(20), @SCountry varchar(50),@CreatedBy int
AS
BEGIN
--Declare @OrderID int
Declare @Orderprefix varchar(5)
Declare @PreFixYear varchar(5)
Declare @SrNumber int
Declare @OrderNum varchar(30)
Declare @Assaymax int
Declare @Samplecount int
Declare @GroupID int
Declare @Status varchar(100)
Declare @GroupName varchar(100)
Declare @GroupPrefix varchar(10)


Select @Assaymax= AssayMax,@GroupPrefix=GroupPrefix  from stblSettings 

Select @GroupID=GroupID, @Samplecount=samplecount ,@Status=Status from stblAssayGroup Where Status='Sample Assessment'

if @Status='Sample Assessment'
Begin 



If @OrderID = 0
Begin
    Select @Orderprefix = orderPrefix + convert(varchar(5),OrderPrefixYear), @PreFixYear = OrderPrefixYear, @SrNumber = OrderSrNumber from stblSettings

	if @PreFixYear = year(getdate())
	begin
		Update stblSettings set OrderSrNumber = OrderSrNumber + 1, OrderPrefixYear = year(getdate()) 
	end
	else
	begin
		Update stblSettings set OrderSrNumber = 1, OrderPrefixYear = year(getdate())
	end

	set @OrderNum = substring('000000000000' + CONVERT(varchar(12),@SrNumber),len(@SrNumber)+1, LEN('000000000000' + CONVERT(varchar(12),@SrNumber)) - len(@SrNumber))

	set @OrderNum = @Orderprefix + substring(@OrderNum,len(@OrderNum)-len(@Orderprefix)+1,12)


	Insert into stblOrder (OrderNum,custID, orderDate,orderStatus,ShipOption,DeliveryEmail,
							Sname,SAddress1,SAddress2,SCity,SState,SZipCode,SCountry,
							BName,BAddress1,BAddress2,BCity,BState,BZipCode,BCountry,CreatedBy,CreatedOn,GroupID)  
		select @OrderNum,@custID,@orderDate,'Draft','Email',Email,
					CustomerName,Address1,Address2,City,State,Zip,Country,
					CustomerName,Address1,Address2,City,State,Zip,Country,@CreatedBy,getdate(),@GroupID
		   from stblCustomer  where custID = @custID

	set @OrderID = @@identity  

	INSERT INTO stblActionAuditLog(EntityType,EntityID,ActionName,ActionBy,ActionOn)
	SELECT 'Order',@OrderID,'Order Created', UserFirstName + ' ' + UserLastName, getdate() FROM dbo.stblUser where UserID = @CreatedBy

	--stblAssayGroup insert
	set @Samplecount=@Samplecount+1

	update stblAssayGroup set sampleCount=@Samplecount where GroupID=@GroupID

	set @Samplecount=@@IDENTITY

	If @Assaymax=@Samplecount
	Begin
	update stblAssayGroup set completedTime=YEAR(GETDATE()),Status='Ready To Testing' where GroupID=@GroupID
	 
	 End
End
else
Begin
	Update stblOrder set ShipOption = @DeliveryOption, DeliveryEmail = @DeliveryEmail , SName = @SName, SAddress1 = @SAddress1, 
		SAddress2 = @SAddress2, SCity = @SCity, SState = @SState, SZipCode = @SZipCode, SCountry = @SCountry where OrderID = @OrderID
End

select @OrderID as OrdrIN
END

else
  Begin
  --GroupName Creation
  set @GroupName = substring('000000000000' + CONVERT(varchar(12),@SrNumber),len(@SrNumber)+1, LEN('000000000000' + CONVERT(varchar(12),@SrNumber)) - len(@SrNumber))

	set @GroupName = @GroupPrefix + substring(@GroupName,len(@GroupName)-len(@GroupName)+1,12)

     
   insert into stblAssayGroup (GroupName,CreateOnTime,sampleCount,Status) values(@GroupName,year(getdate()),0,'Sample Assessment')
   set @GroupID=@@IDENTITY

   Select @GroupID=GroupID, @Samplecount=samplecount ,@Status=Status from stblAssayGroup Where GroupID=@GroupID

   If @OrderID = 0
Begin
    Select @Orderprefix = orderPrefix + convert(varchar(5),OrderPrefixYear), @PreFixYear = OrderPrefixYear, @SrNumber = OrderSrNumber from stblSettings

	if @PreFixYear = year(getdate())
	begin
		Update stblSettings set OrderSrNumber = OrderSrNumber + 1, OrderPrefixYear = year(getdate()) 
	end
	else
	begin
		Update stblSettings set OrderSrNumber = 1, OrderPrefixYear = year(getdate())
	end

	set @OrderNum = substring('000000000000' + CONVERT(varchar(12),@SrNumber),len(@SrNumber)+1, LEN('000000000000' + CONVERT(varchar(12),@SrNumber)) - len(@SrNumber))

	set @OrderNum = @Orderprefix + substring(@OrderNum,len(@OrderNum)-len(@Orderprefix)+1,12)


	Insert into stblOrder (OrderNum,custID, orderDate,orderStatus,ShipOption,DeliveryEmail,
							Sname,SAddress1,SAddress2,SCity,SState,SZipCode,SCountry,
							BName,BAddress1,BAddress2,BCity,BState,BZipCode,BCountry,CreatedBy,CreatedOn,GroupID)  
		select @OrderNum,@custID,@orderDate,'Draft','Email',Email,
					CustomerName,Address1,Address2,City,State,Zip,Country,
					CustomerName,Address1,Address2,City,State,Zip,Country,@CreatedBy,getdate(),@GroupID
		   from stblCustomer  where custID = @custID

	set @OrderID = @@identity  

	INSERT INTO stblActionAuditLog(EntityType,EntityID,ActionName,ActionBy,ActionOn)
	SELECT 'Order',@OrderID,'Order Created', UserFirstName + ' ' + UserLastName, getdate() FROM dbo.stblUser where UserID = @CreatedBy

	--stblAssayGroup insert
	set @Samplecount=@Samplecount+1

	update stblAssayGroup set sampleCount=@Samplecount where GroupID=@GroupID

	set @Samplecount=@@IDENTITY

	
End
else
Begin
	Update stblOrder set ShipOption = @DeliveryOption, DeliveryEmail = @DeliveryEmail , SName = @SName, SAddress1 = @SAddress1, 
		SAddress2 = @SAddress2, SCity = @SCity, SState = @SState, SZipCode = @SZipCode, SCountry = @SCountry where OrderID = @OrderID
End

select @OrderID as OrdrIN

   End
End 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[18] 2[4] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -288
      End
      Begin Tables = 
         Begin Table = "stblPatient"
            Begin Extent = 
               Top = 0
               Left = 672
               Bottom = 177
               Right = 842
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblSampleTestTrack"
            Begin Extent = 
               Top = 4
               Left = 946
               Bottom = 300
               Right = 1198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblOrder"
            Begin Extent = 
               Top = 6
               Left = 351
               Bottom = 310
               Right = 549
            End
            DisplayFlags = 280
            TopColumn = 22
         End
         Begin Table = "stblCustomer"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 243
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 40
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3150
         Alias = 1260
         Table = 1860
         Output = 765
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'
GO
