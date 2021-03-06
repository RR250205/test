USE [kosoft_Stg_Storms]
GO
/****** Object:  Table [dbo].[stblInsurancePreauthorization]    Script Date: 04-25-2018 12:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stblInsurancePreauthorization](
	[PreInsuranceNo] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[Dataofbirth] [nvarchar](50) NULL,
	[MobileNumber] [nvarchar](50) NULL,
	[PrimaryInsName] [nvarchar](50) NULL,
	[InsuranceCard_IDno] [nvarchar](50) NULL,
	[PolicyNumber] [nvarchar](50) NULL,
	[PolicyName] [nvarchar](50) NULL,
	[OtherInsurance] [bit] NULL,
	[TenantID] [int] NULL,
	[Createon] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
	[Comments] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[stblInsurancePreauthorization] ON 

INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (1, N'suganya', N'Female', N'10-04-1991', N'5555', N'4444', N'cd14225', N'dfgh414', N'gdfg', 1, 2, CAST(N'2018-04-24 13:13:24.220' AS DateTime), N'Submitted', N'normal')
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (2, N'mani', N'Male', N'10-04-1991', N'8888888888888', N'sdathish', N'415524', N'852456871', N'sathish', 1, 4, CAST(N'2018-04-24 12:44:09.823' AS DateTime), N'Submitted', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (3, N'sathish', N'Male', N'10-4-1991', N'998899889988', N'lic', N'123654', N'1254', N'sathish', 1, 2, CAST(N'2018-04-20 17:45:41.877' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (4, N'sathishkumar', N'Other', N'10-4-1991', N'8855447755', N'ICICI', N'123654', N'989495', N'santhosh', 1, 2, CAST(N'2018-04-23 21:05:54.633' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (5, N'vinotha', N'Male', N'10-4-1991', N'998899889988', N'lic', N'123654', N'1254', N'sathish', 1, 4, CAST(N'2018-04-24 12:38:18.320' AS DateTime), N'Submitted', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (6, N'anji', N'Male', N'10-4-1991', N'998899889988', N'lic', N'123654', N'1254', N'sathish', 1, 4, CAST(N'2018-04-24 12:41:31.853' AS DateTime), N'Submitted', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (7, N'jai', N'other', N'10-04-1991', N'5555555', N'zfv', N'555', N'ads555', N'dvs', 0, 2, CAST(N'2018-04-21 16:58:07.700' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (8, N'jai', N'other', N'10-04-1991', N'5555555', N'zfv', N'555', N'ads555', N'dvs', 0, 2, CAST(N'2018-04-21 17:01:48.430' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (10, N'sureshkumar', N'Male', N'10-4-1991', N'998899889988', N'lic', N'123654', N'1254', N'sathish', NULL, 4, CAST(N'2018-04-23 21:12:17.380' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (11, N'rasampal', N'female', N'10-04-1935', N'5555555', N'mani', N'555', N'ads555', N'dvs', NULL, 2, CAST(N'2018-04-23 21:15:01.150' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (12, N'Ragu', N'female', N'10-04-1991', N'4524180788', N'ilc', N'dsfh', N'ads555', N'dvs', NULL, 2, CAST(N'2018-04-24 09:31:44.340' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (13, N'Santha', N'female', N'10-05-2018', N'8870814254', N'LIC', N'fxbf', N'dfyfdgf', N'dtrfu', 0, 2, CAST(N'2018-04-24 09:31:44.340' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (14, N'vanitha', N'female', N'10-04-1989', N'654654456456', N'zfv', N'sfd848274', N'ads555', N'dvs', NULL, 2, CAST(N'2018-04-24 13:17:54.473' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (15, N'Thiru', N'male', N'10-04-1989', N'654654456456', N'ilc', N'555', N'ads555', N'dvs', NULL, 2, CAST(N'2018-04-25 10:29:07.363' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (16, N'sathish', N'other', N'10-04-1991', N'5555555', N'zfv', N'555', N'ads555', N'dvs', NULL, 2, CAST(N'2018-04-25 11:49:01.850' AS DateTime), N'Received', NULL)
INSERT [dbo].[stblInsurancePreauthorization] ([PreInsuranceNo], [PatientName], [Gender], [Dataofbirth], [MobileNumber], [PrimaryInsName], [InsuranceCard_IDno], [PolicyNumber], [PolicyName], [OtherInsurance], [TenantID], [Createon], [Status], [Comments]) VALUES (9, N'perumalraja', N'other', N'10-04-1991', N'5555555', N'zfv', N'555', N'ads555', N'dvs', 0, 2, CAST(N'2018-04-21 17:23:21.850' AS DateTime), N'Received', NULL)
SET IDENTITY_INSERT [dbo].[stblInsurancePreauthorization] OFF
