USE [STOMS2]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwSampleTestTrack'

GO
/****** Object:  StoredProcedure [dbo].[sspSaveOrder]    Script Date: 8/12/2016 6:34:56 PM ******/
DROP PROCEDURE [dbo].[sspSaveOrder]
GO
/****** Object:  StoredProcedure [dbo].[sspGetGroup]    Script Date: 8/12/2016 6:34:56 PM ******/
DROP PROCEDURE [dbo].[sspGetGroup]
GO
/****** Object:  View [dbo].[svwSampleTestTrack]    Script Date: 8/12/2016 6:34:56 PM ******/
DROP VIEW [dbo].[svwSampleTestTrack]
GO
/****** Object:  Table [dbo].[stblSettings]    Script Date: 8/12/2016 6:34:56 PM ******/
DROP TABLE [dbo].[stblSettings]
GO
/****** Object:  Table [dbo].[stblOrder]    Script Date: 8/12/2016 6:34:56 PM ******/
DROP TABLE [dbo].[stblOrder]
GO
/****** Object:  Table [dbo].[stblAssayGroup]    Script Date: 8/12/2016 6:34:56 PM ******/
DROP TABLE [dbo].[stblAssayGroup]
GO
/****** Object:  Table [dbo].[stblAssayGroup]    Script Date: 8/12/2016 6:34:56 PM ******/
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
/****** Object:  Table [dbo].[stblOrder]    Script Date: 8/12/2016 6:34:56 PM ******/
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
/****** Object:  Table [dbo].[stblSettings]    Script Date: 8/12/2016 6:34:56 PM ******/
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
/****** Object:  View [dbo].[svwSampleTestTrack]    Script Date: 8/12/2016 6:34:56 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sspGetGroup]    Script Date: 8/12/2016 6:34:56 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sspSaveOrder]    Script Date: 8/12/2016 6:34:56 PM ******/
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
