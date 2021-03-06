USE [kosoft_Stg_Storms]
GO
/****** Object:  Table [dbo].[stblPreAuthorizationInsurance]    Script Date: 04-27-2018 12:30:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stblPreAuthorizationInsurance](
	[InsuranceID] [int] IDENTITY(1,1) NOT NULL,
	[Insurance_TenantID] [int] NULL,
	[Labuser_TenantID] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[PrimaryCompayID] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[stblPreAuthorizationInsurance] ON 

INSERT [dbo].[stblPreAuthorizationInsurance] ([InsuranceID], [Insurance_TenantID], [Labuser_TenantID], [Status], [PrimaryCompayID]) VALUES (1, 8, 2, N'Active', 2)
INSERT [dbo].[stblPreAuthorizationInsurance] ([InsuranceID], [Insurance_TenantID], [Labuser_TenantID], [Status], [PrimaryCompayID]) VALUES (2, 8, 4, N'Active', 2)
SET IDENTITY_INSERT [dbo].[stblPreAuthorizationInsurance] OFF
