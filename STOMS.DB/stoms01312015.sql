USE [STOMS]
GO
/****** Object:  StoredProcedure [dbo].[sspGetTenantDashboardStats]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sspGetTenantDashboardStats] @OrgID int, @ActivityMonth int, @ActivityYear int
AS
BEGIN

Declare @TotalOrders int
Declare @SampleTesting int
Declare @ClientCount int
Declare @OutStandingInv int

	select ClientID into #tbl1 from stblClient where OrgID = @OrgID

	select @TotalOrders = count(*) from stblOrder where ClientID in (select ClientID from #tbl1) and
		month(OrderDate) = @ActivityMonth and year(OrderDate) = @ActivityYear
	
	select @SampleTesting = count(*) from stblSampleTestTrack where ClientID in (select ClientID from #tbl1)

	select @ClientCount = count(*) from #tbl1

	select @OutStandingInv = count(*) from stblInvoice where OrderID in (select OrderID from stblOrder where ClientID in (select ClientID from #tbl1))
		and invStatus = 'Open'

	Select @TotalOrders as TotalOrders, @SampleTesting as TestInProgress, @ClientCount as ClientCount, @OutStandingInv as OutStandingInv
END

GO
/****** Object:  StoredProcedure [dbo].[sspUserLogin]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sspUserLogin] @UserEmail varchar(255), @password varchar(255)
AS
BEGIN
	SET NOCOUNT ON;
    IF (SELECT count(*) FROM svwUser WHERE UserEmail=@UserEmail and password=@password) > 0
		BEGIN
			Update svwUser set lastLogin=getdate() WHERE UserEmail=@UserEmail and password=@password

			SELECT * FROM svwUser WHERE UserEmail=@UserEmail and password=@password
		END
	ELSE
		BEGIN
			RETURN 0;
		END
END


GO
/****** Object:  Table [dbo].[stblClient]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblClient](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[OrgID] [int] NULL,
	[ClientName] [varchar](50) NULL,
	[Phone] [varchar](30) NULL,
	[Fax] [varchar](30) NULL,
	[Email] [varchar](30) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblEntFunctions]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblEntFunctions](
	[entFuncID] [int] IDENTITY(1,1) NOT NULL,
	[funcName] [varchar](100) NULL,
	[funcOrder] [int] NULL,
	[menuLink] [varchar](100) NULL,
	[parentFuncID] [int] NULL,
	[displayIndicator] [varchar](100) NULL,
	[isChild] [bit] NULL,
	[menuIcon] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblInvoice]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblInvoice](
	[invoiceID] [int] IDENTITY(1,1) NOT NULL,
	[invNumber] [varchar](20) NULL,
	[invDate] [smalldatetime] NULL,
	[invAmount] [decimal](12, 2) NULL,
	[discountAmt] [decimal](12, 2) NULL,
	[discountType] [varchar](20) NULL,
	[OrderID] [int] NULL,
	[invNotes] [varchar](max) NULL,
	[invStatus] [varchar](20) NULL,
	[isReceipt] [bit] NULL,
 CONSTRAINT [PK_stblInvoice] PRIMARY KEY CLUSTERED 
(
	[invoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblInvoiceDetail]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stblInvoiceDetail](
	[invDetailID] [int] IDENTITY(1,1) NOT NULL,
	[invID] [int] NULL,
	[itemID] [int] NULL,
	[quantity] [int] NULL,
	[unitCost] [decimal](12, 2) NULL,
	[totalCost] [decimal](12, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stblInvoicePayment]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stblInvoicePayment](
	[payID] [int] IDENTITY(1,1) NOT NULL,
	[invID] [int] NULL,
	[payTypeID] [tinyint] NULL,
	[payAmount] [decimal](12, 2) NULL,
	[payDate] [datetime] NULL,
 CONSTRAINT [PK_stblInvoicePayment] PRIMARY KEY CLUSTERED 
(
	[payID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stblOrder]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblOrder](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNum] [varchar](20) NULL,
	[OrderDate] [date] NULL,
	[OrderStatus] [varchar](30) NULL,
	[ClientID] [int] NULL,
	[Discount] [decimal](8, 2) NULL,
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
	[OrderCompletedOn] [datetime] NULL,
	[CompletedBy] [int] NULL,
	[GenOrderCustPhone] [varchar](30) NULL,
	[GenOrderCustName] [varchar](50) NULL,
	[ClientDiscountTier] [smallint] NULL,
 CONSTRAINT [PK_tblOrder] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblOrderDetail]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblOrderDetail](
	[OrderItemID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [varchar](30) NULL,
	[ItemSNo] [smallint] NULL,
	[ItemID] [int] NULL,
	[OrderItem] [varchar](255) NULL,
	[OrderQty] [int] NULL,
	[UnitCost] [decimal](8, 2) NULL,
	[TotalCost] [decimal](8, 2) NULL,
	[ItemDiscountType] [varchar](30) NULL,
	[ItemDiscountValue] [decimal](8, 2) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblOrderPayment]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblOrderPayment](
	[OrderPayID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[PayTypeID] [int] NULL,
	[PayAmount] [decimal](8, 2) NULL,
	[CardNumber] [varchar](20) NULL,
	[CardType] [varchar](20) NULL,
	[CardExp] [varchar](10) NULL,
	[CVV] [varchar](5) NULL,
	[BankName] [varchar](50) NULL,
	[CheckNumber] [varchar](20) NULL,
	[CheckDate] [varchar](30) NULL,
	[AccountNumber] [varchar](30) NULL,
	[RoutingNumber] [varchar](30) NULL,
	[PayReference] [varchar](50) NULL,
	[TransCode] [varchar](30) NULL,
	[PayDate] [datetime] NULL,
	[CreditAllowComments] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblOrganization]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblOrganization](
	[OrgID] [int] IDENTITY(1,1) NOT NULL,
	[OrgName] [varchar](50) NULL,
	[TaxID] [varchar](30) NULL,
	[IncorporatedAt] [varchar](30) NULL,
	[OrgGroupID] [int] NULL,
	[OrgAdminID] [int] NULL,
	[OrgStatus] [varchar](30) NULL,
 CONSTRAINT [PK_stblCompany] PRIMARY KEY CLUSTERED 
(
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblOrgGroup]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblOrgGroup](
	[OrgGroupID] [int] IDENTITY(1,1) NOT NULL,
	[OrgGroupName] [varchar](255) NULL,
	[GroupTaxID] [varchar](30) NULL,
	[IncorporateAt] [varchar](50) NULL,
	[IsMultiTaxFirm] [bit] NULL,
	[GroupAdminID] [int] NULL,
	[OrgGroupType] [varchar](30) NULL,
	[OrgGroupStatus] [varchar](30) NULL,
 CONSTRAINT [PK_stblOrgGroup] PRIMARY KEY CLUSTERED 
(
	[OrgGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblOrgLocation]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblOrgLocation](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[OrgID] [int] NULL,
	[OrgLocType] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Zip] [varchar](15) NULL,
	[LocationStatus] [varchar](30) NULL,
 CONSTRAINT [PK_stblOrgLocation] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblPaymentType]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblPaymentType](
	[payTypeID] [tinyint] NULL,
	[payType] [varchar](30) NULL,
	[typeStatus] [varchar](30) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblSampleTestTrack]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblSampleTestTrack](
	[TTrackID] [int] IDENTITY(1,1) NOT NULL,
	[OrgID] [int] NULL,
	[ClientID] [int] NULL,
	[SampleBarCode] [varchar](50) NULL,
	[DateAssayBIN] [datetime] NULL,
	[DateAssayBLO] [datetime] NULL,
	[ResultBIN] [varchar](20) NULL,
	[ResultBL] [varchar](20) NULL,
	[ResultSentDate] [datetime] NULL,
	[SampleStatus] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblUser]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserEmail] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[UserMobileNumber] [varchar](30) NULL,
	[UserFirstName] [varchar](50) NULL,
	[UserLastName] [varchar](50) NULL,
	[UserTypeID] [int] NULL,
	[UserStatus] [varchar](30) NULL,
	[LastLogin] [datetime] NULL,
	[PriOrgID] [int] NULL,
	[PriLocationID] [int] NULL,
 CONSTRAINT [PK_stblUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblUserCompanyAssociation]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblUserCompanyAssociation](
	[UserCompID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CompanyID] [int] NOT NULL,
	[associateStatus] [varchar](30) NULL,
 CONSTRAINT [PK_stblUserCompanyAssociation] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblUserType]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblUserType](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [varchar](50) NULL,
	[UserGroup] [varchar](50) NULL,
 CONSTRAINT [PK_stblUserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblUserTypeFunc]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stblUserTypeFunc](
	[typfuncID] [int] IDENTITY(1,1) NOT NULL,
	[usrTypeID] [int] NULL,
	[funcID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[svwOrder]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[svwOrder]
AS
SELECT        dbo.stblOrder.*, dbo.stblClient.OrgID, dbo.stblClient.ClientName, dbo.stblClient.Phone, dbo.stblClient.Fax, dbo.stblClient.Email
FROM            dbo.stblClient INNER JOIN
                         dbo.stblOrder ON dbo.stblClient.ClientID = dbo.stblOrder.ClientID

GO
/****** Object:  View [dbo].[svwUser]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[svwUser]
AS
SELECT        dbo.stblUser.UserID, dbo.stblUser.UserEmail, dbo.stblUser.Password, dbo.stblUser.UserMobileNumber, dbo.stblUser.UserFirstName, dbo.stblUser.UserLastName, dbo.stblUser.UserTypeID, 
                         dbo.stblUser.UserStatus, dbo.stblUser.LastLogin, dbo.stblUser.PriOrgID, dbo.stblUser.PriLocationID, dbo.stblUserType.UserTypeName, dbo.stblUserType.UserGroup, dbo.stblOrganization.OrgName
FROM            dbo.stblOrganization INNER JOIN
                         dbo.stblUser ON dbo.stblOrganization.OrgID = dbo.stblUser.PriOrgID INNER JOIN
                         dbo.stblUserType ON dbo.stblUser.UserTypeID = dbo.stblUserType.UserTypeID

GO
/****** Object:  View [dbo].[svwUserTypeFunctions]    Script Date: 1/31/2015 10:37:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[svwUserTypeFunctions]
AS
SELECT        dbo.stblEntFunctions.entFuncID, dbo.stblEntFunctions.funcName, dbo.stblEntFunctions.funcOrder, dbo.stblEntFunctions.menuLink, dbo.stblEntFunctions.parentFuncID, dbo.stblEntFunctions.displayIndicator, 
                         dbo.stblEntFunctions.isChild, dbo.stblEntFunctions.menuIcon, dbo.stblUserTypeFunc.usrTypeID
FROM            dbo.stblUserTypeFunc INNER JOIN
                         dbo.stblEntFunctions ON dbo.stblUserTypeFunc.funcID = dbo.stblEntFunctions.entFuncID


GO
SET IDENTITY_INSERT [dbo].[stblEntFunctions] ON 

INSERT [dbo].[stblEntFunctions] ([entFuncID], [funcName], [funcOrder], [menuLink], [parentFuncID], [displayIndicator], [isChild], [menuIcon]) VALUES (1, N'Dashboard', 1, N'pages/dashboard', 0, N'fa fa-dashboard', 0, NULL)
INSERT [dbo].[stblEntFunctions] ([entFuncID], [funcName], [funcOrder], [menuLink], [parentFuncID], [displayIndicator], [isChild], [menuIcon]) VALUES (2, N'Order', 2, N'pages/order', 0, N'fa fa-stethoscope', 0, NULL)
INSERT [dbo].[stblEntFunctions] ([entFuncID], [funcName], [funcOrder], [menuLink], [parentFuncID], [displayIndicator], [isChild], [menuIcon]) VALUES (3, N'Invoice', 3, N'pages/invoice', 0, N'fa fa-table', 0, NULL)
INSERT [dbo].[stblEntFunctions] ([entFuncID], [funcName], [funcOrder], [menuLink], [parentFuncID], [displayIndicator], [isChild], [menuIcon]) VALUES (4, N'Reports', 4, N'pages/reports', 0, N'fa fa-bar-chart-o', 0, NULL)
INSERT [dbo].[stblEntFunctions] ([entFuncID], [funcName], [funcOrder], [menuLink], [parentFuncID], [displayIndicator], [isChild], [menuIcon]) VALUES (5, N'Administration', 7, N'pages/admin', 0, N'fa fa-support', 0, NULL)
SET IDENTITY_INSERT [dbo].[stblEntFunctions] OFF
SET IDENTITY_INSERT [dbo].[stblOrganization] ON 

INSERT [dbo].[stblOrganization] ([OrgID], [OrgName], [TaxID], [IncorporatedAt], [OrgGroupID], [OrgAdminID], [OrgStatus]) VALUES (1, N'Iliad Neurosciences Inc.', N'', N'PA', 1, NULL, N'Active')
SET IDENTITY_INSERT [dbo].[stblOrganization] OFF
SET IDENTITY_INSERT [dbo].[stblOrgGroup] ON 

INSERT [dbo].[stblOrgGroup] ([OrgGroupID], [OrgGroupName], [GroupTaxID], [IncorporateAt], [IsMultiTaxFirm], [GroupAdminID], [OrgGroupType], [OrgGroupStatus]) VALUES (1, N'Iliad Neurosciences Inc.', N'', N'PA', 0, NULL, NULL, N'Active')
SET IDENTITY_INSERT [dbo].[stblOrgGroup] OFF
SET IDENTITY_INSERT [dbo].[stblOrgLocation] ON 

INSERT [dbo].[stblOrgLocation] ([LocationID], [OrgID], [OrgLocType], [Address1], [Address2], [City], [State], [Zip], [LocationStatus]) VALUES (1, 1, N'Primary', N'5110 Campus Drive, Suite #190', NULL, N'Plymouth Meeting', N'PA', N'19462', N'Active')
SET IDENTITY_INSERT [dbo].[stblOrgLocation] OFF
SET IDENTITY_INSERT [dbo].[stblUser] ON 

INSERT [dbo].[stblUser] ([UserID], [UserEmail], [Password], [UserMobileNumber], [UserFirstName], [UserLastName], [UserTypeID], [UserStatus], [LastLogin], [PriOrgID], [PriLocationID]) VALUES (1, N'stsetsekos@iliadneuro.com', N'pass', N'', N'Steve', N'Tsetsekos', 3, N'Active', NULL, 1, 1)
INSERT [dbo].[stblUser] ([UserID], [UserEmail], [Password], [UserMobileNumber], [UserFirstName], [UserLastName], [UserTypeID], [UserStatus], [LastLogin], [PriOrgID], [PriLocationID]) VALUES (2, N'bgonen@iliadneuro.com', N'pass', NULL, N'Boas', N'Gonen', 2, N'Active', NULL, 1, 1)
INSERT [dbo].[stblUser] ([UserID], [UserEmail], [Password], [UserMobileNumber], [UserFirstName], [UserLastName], [UserTypeID], [UserStatus], [LastLogin], [PriOrgID], [PriLocationID]) VALUES (3, N'sadelman@iliadneuro.com', N'pass', NULL, N'Steven', N'Adelman', 4, N'Active', NULL, 1, 1)
INSERT [dbo].[stblUser] ([UserID], [UserEmail], [Password], [UserMobileNumber], [UserFirstName], [UserLastName], [UserTypeID], [UserStatus], [LastLogin], [PriOrgID], [PriLocationID]) VALUES (4, N'selva@simhasoft.com', N'pass', NULL, N'Selva', N'Ardhanareesan', 1, N'Active', NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[stblUser] OFF
SET IDENTITY_INSERT [dbo].[stblUserType] ON 

INSERT [dbo].[stblUserType] ([UserTypeID], [UserTypeName], [UserGroup]) VALUES (1, N'Platform Admin', N'Platform')
INSERT [dbo].[stblUserType] ([UserTypeID], [UserTypeName], [UserGroup]) VALUES (2, N'Administrator', N'Tenant')
INSERT [dbo].[stblUserType] ([UserTypeID], [UserTypeName], [UserGroup]) VALUES (3, N'Fin User', N'Tenant')
INSERT [dbo].[stblUserType] ([UserTypeID], [UserTypeName], [UserGroup]) VALUES (4, N'User', N'Tenant')
SET IDENTITY_INSERT [dbo].[stblUserType] OFF
SET IDENTITY_INSERT [dbo].[stblUserTypeFunc] ON 

INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (1, 1, 1)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (2, 1, 5)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (3, 2, 1)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (4, 2, 2)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (5, 2, 3)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (6, 2, 4)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (7, 2, 5)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (8, 3, 1)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (9, 3, 2)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (10, 3, 3)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (11, 3, 4)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (12, 4, 1)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (13, 4, 2)
INSERT [dbo].[stblUserTypeFunc] ([typfuncID], [usrTypeID], [funcID]) VALUES (14, 4, 4)
SET IDENTITY_INSERT [dbo].[stblUserTypeFunc] OFF
ALTER TABLE [dbo].[stblOrder] ADD  CONSTRAINT [DF_stblOrder_discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[stblInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_stblInvoiceDetail_stblInvoice] FOREIGN KEY([invID])
REFERENCES [dbo].[stblInvoice] ([invoiceID])
GO
ALTER TABLE [dbo].[stblInvoiceDetail] CHECK CONSTRAINT [FK_stblInvoiceDetail_stblInvoice]
GO
ALTER TABLE [dbo].[stblInvoicePayment]  WITH CHECK ADD  CONSTRAINT [FK_stblInvoicePayment_stblInvoice] FOREIGN KEY([invID])
REFERENCES [dbo].[stblInvoice] ([invoiceID])
GO
ALTER TABLE [dbo].[stblInvoicePayment] CHECK CONSTRAINT [FK_stblInvoicePayment_stblInvoice]
GO
ALTER TABLE [dbo].[stblOrganization]  WITH CHECK ADD  CONSTRAINT [FK_stblOrganization_stblOrgGroup] FOREIGN KEY([OrgGroupID])
REFERENCES [dbo].[stblOrgGroup] ([OrgGroupID])
GO
ALTER TABLE [dbo].[stblOrganization] CHECK CONSTRAINT [FK_stblOrganization_stblOrgGroup]
GO
ALTER TABLE [dbo].[stblOrgLocation]  WITH CHECK ADD  CONSTRAINT [FK_stblOrgLocation_stblOrganization] FOREIGN KEY([OrgID])
REFERENCES [dbo].[stblOrganization] ([OrgID])
GO
ALTER TABLE [dbo].[stblOrgLocation] CHECK CONSTRAINT [FK_stblOrgLocation_stblOrganization]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Left = 0
      End
      Begin Tables = 
         Begin Table = "stblClient"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 196
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblOrder"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 284
               Right = 444
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
      Begin ColumnWidths = 9
         Width = 284
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
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Left = 0
      End
      Begin Tables = 
         Begin Table = "stblOrganization"
            Begin Extent = 
               Top = 5
               Left = 531
               Bottom = 195
               Right = 701
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblUser"
            Begin Extent = 
               Top = 6
               Left = 267
               Bottom = 283
               Right = 460
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblUserType"
            Begin Extent = 
               Top = 12
               Left = 26
               Bottom = 287
               Right = 217
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
      Begin ColumnWidths = 9
         Width = 284
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
         Column = 2730
         Alias = 900
         Table = 1170
         Output = 720
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Left = 0
      End
      Begin Tables = 
         Begin Table = "stblUserTypeFunc"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblEntFunctions"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 220
               Right = 419
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
      Begin ColumnWidths = 9
         Width = 284
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
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwUserTypeFunctions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwUserTypeFunctions'
GO
