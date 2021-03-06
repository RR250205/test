USE [kosoft_canary]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimens'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimens'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimens'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimenPatients'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimenPatients'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimenPatients'

GO
/****** Object:  StoredProcedure [dbo].[sspviewAssayInfo]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP PROCEDURE [dbo].[sspviewAssayInfo]
GO
/****** Object:  StoredProcedure [dbo].[sspUpdateAssayGroup]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP PROCEDURE [dbo].[sspUpdateAssayGroup]
GO
/****** Object:  StoredProcedure [dbo].[sspGetAssayInfo]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP PROCEDURE [dbo].[sspGetAssayInfo]
GO
/****** Object:  StoredProcedure [dbo].[sspAddSpecimenToAssay]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP PROCEDURE [dbo].[sspAddSpecimenToAssay]
GO
/****** Object:  View [dbo].[svwAssaySpecimens]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP VIEW [dbo].[svwAssaySpecimens]
GO
/****** Object:  View [dbo].[svwAssaySpecimenPatients]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP VIEW [dbo].[svwAssaySpecimenPatients]
GO
/****** Object:  Table [dbo].[stblAssaySpecimens]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP TABLE [dbo].[stblAssaySpecimens]
GO
/****** Object:  Table [dbo].[stblAssayGroup]    Script Date: 11-01-2018 03:32:25 PM ******/
DROP TABLE [dbo].[stblAssayGroup]
GO
/****** Object:  Table [dbo].[stblAssayGroup]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblAssayGroup](
	[AssayID] [int] IDENTITY(1,1) NOT NULL,
	[AssayBIN] [varchar](30) NULL,
	[AssayDesc] [varchar](255) NULL,
	[CreatedOn] [datetime] NULL,
	[SampleMaxDate] [datetime] NULL,
	[AssayLoadDateTime] [datetime] NULL,
	[AssayCompleteDateTime] [datetime] NULL,
	[SampleCount] [int] NULL,
	[AssayStatus] [varchar](30) NULL,
	[TenantID] [int] NULL,
	[AssayType] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stblAssaySpecimens]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stblAssaySpecimens](
	[AssaySpecimenID] [int] IDENTITY(1,1) NOT NULL,
	[AssayID] [int] NOT NULL,
	[SpecimenID] [int] NOT NULL,
	[IsRepeat] [bit] NULL,
	[OutofBINDate] [datetime] NULL,
	[AssignedToBINOn] [datetime] NULL,
	[SpecimenType] [varchar](30) NULL,
	[SubSpecimenType] [varchar](30) NULL,
	[RemainVol] [varchar](20) NULL,
	[BindValue] [decimal](5, 2) NULL,
	[BindValComment] [varchar](30) NULL,
	[BlockValue] [decimal](5, 2) NULL,
	[BlockValComment] [varchar](30) NULL,
	[ResultFileName] [varchar](50) NULL,
	[ResultSentDate] [datetime] NULL,
	[TestStatus] [varchar](30) NULL,
	[ResultDocID] [int] NULL,
	[RepeatCount] [smallint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[svwAssaySpecimenPatients]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[svwAssaySpecimenPatients]
AS
SELECT        dbo.stblAssaySpecimens.AssaySpecimenID, dbo.stblAssaySpecimens.AssayID, dbo.stblAssaySpecimens.SpecimenID, dbo.stblAssaySpecimens.IsRepeat, dbo.stblAssaySpecimens.OutofBINDate, 
                         dbo.stblAssaySpecimens.AssignedToBINOn, dbo.stblAssayGroup.AssayBIN, dbo.stblAssayGroup.AssayDesc, dbo.stblAssayGroup.CreatedOn, dbo.stblAssayGroup.SampleMaxDate, dbo.stblAssayGroup.AssayLoadDateTime, 
                         dbo.stblAssayGroup.AssayCompleteDateTime, dbo.stblAssayGroup.SampleCount, dbo.stblAssayGroup.AssayStatus, dbo.stblAssayGroup.TenantID, dbo.svwSpecimenInfo.SpecimenNumber, dbo.svwSpecimenInfo.PatientID, 
                         dbo.stblPatient.Gender, dbo.stblPatient.DOB, dbo.stblPatient.FirstName + ' ' + COALESCE (dbo.stblPatient.LastName, '') AS PatientName, dbo.stblPatient.FirstName, dbo.stblPatient.LastName, 
                         dbo.svwSpecimenInfo.CreatedOn AS SpecimenCreatedOn, dbo.svwSpecimenInfo.SpecimenStatus, dbo.svwSpecimenInfo.CreatedByName, dbo.svwSpecimenInfo.DateDrawn, dbo.svwSpecimenInfo.CustomerID, 
                         dbo.stblAssayGroup.AssayType
FROM            dbo.stblAssaySpecimens INNER JOIN
                         dbo.stblAssayGroup ON dbo.stblAssaySpecimens.AssayID = dbo.stblAssayGroup.AssayID INNER JOIN
                         dbo.svwSpecimenInfo ON dbo.stblAssaySpecimens.SpecimenID = dbo.svwSpecimenInfo.SpecimenID INNER JOIN
                         dbo.stblPatient ON dbo.svwSpecimenInfo.PatientID = dbo.stblPatient.PatientID

GO
/****** Object:  View [dbo].[svwAssaySpecimens]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[svwAssaySpecimens]
AS
SELECT        dbo.stblAssaySpecimens.AssaySpecimenID, dbo.stblAssaySpecimens.AssayID, dbo.stblAssaySpecimens.SpecimenID, dbo.stblAssaySpecimens.IsRepeat, dbo.stblAssaySpecimens.OutofBINDate, 
                         dbo.stblAssaySpecimens.AssignedToBINOn, dbo.stblAssayGroup.AssayBIN, dbo.stblAssayGroup.AssayDesc, dbo.stblAssayGroup.CreatedOn, dbo.stblAssayGroup.SampleMaxDate, dbo.stblAssayGroup.AssayLoadDateTime, 
                         dbo.stblAssayGroup.AssayCompleteDateTime, dbo.stblAssayGroup.SampleCount, dbo.stblAssayGroup.AssayStatus, dbo.stblAssayGroup.TenantID, dbo.svwSpecimenInfo.SpecimenNumber, dbo.svwSpecimenInfo.PatientID, 
                         dbo.svwSpecimenInfo.CreatedOn AS SpecimenRecCreatedOn, dbo.svwSpecimenInfo.SpecimenStatus, dbo.svwSpecimenInfo.CreatedByName, dbo.svwSpecimenInfo.DateDrawn, dbo.svwSpecimenInfo.CustomerID, 
                         dbo.stblAssaySpecimens.SpecimenType, dbo.stblAssaySpecimens.SubSpecimenType, dbo.stblAssaySpecimens.RemainVol, dbo.stblAssaySpecimens.BindValue, dbo.stblAssaySpecimens.BindValComment, 
                         dbo.stblAssaySpecimens.BlockValue, dbo.stblAssaySpecimens.BlockValComment, dbo.stblAssaySpecimens.ResultFileName, dbo.stblAssaySpecimens.ResultSentDate, dbo.stblAssaySpecimens.ResultDocID, 
                         dbo.stblAssaySpecimens.TestStatus, dbo.stblAssaySpecimens.RepeatCount, dbo.svwSpecimenInfo.InterSubstance, dbo.svwSpecimenInfo.IsConsent, dbo.svwSpecimenInfo.IsSpecimenAccept, 
                         dbo.svwSpecimenInfo.RejectReasons, dbo.svwSpecimenInfo.TimeDrawn, dbo.svwSpecimenInfo.TransitTemperature, dbo.svwSpecimenInfo.TransitTime, dbo.svwSpecimenInfo.VolumeReceived, 
                         dbo.svwSpecimenInfo.ReqFormCopyID, dbo.svwSpecimenInfo.TestType, dbo.svwSpecimenInfo.PaymentMode, dbo.svwSpecimenInfo.SpecimenType AS Expr1, dbo.svwSpecimenInfo.BloodType, 
                         dbo.svwSpecimenInfo.PendingReason, dbo.svwSpecimenInfo.ReactivateReason, dbo.svwSpecimenInfo.IsRejection, dbo.stblAssayGroup.AssayType, dbo.svwSpecimenInfo.ReceivedTime, 
                         dbo.svwSpecimenInfo.SampleReceivedDate, dbo.svwSpecimenInfo.TenantID AS Expr2, dbo.svwSpecimenInfo.CreatedBy
FROM            dbo.stblAssaySpecimens INNER JOIN
                         dbo.stblAssayGroup ON dbo.stblAssaySpecimens.AssayID = dbo.stblAssayGroup.AssayID INNER JOIN
                         dbo.svwSpecimenInfo ON dbo.stblAssaySpecimens.SpecimenID = dbo.svwSpecimenInfo.SpecimenID

GO
/****** Object:  StoredProcedure [dbo].[sspAddSpecimenToAssay]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sspAddSpecimenToAssay] @SpecimenID int, @TenantID int,@AssayType varchar(50)
as
begin
	Declare @MaxSpecimenCount int
	Declare @ReturnNum AS VARCHAR(20)
	Declare @AssayID int

	if (Select count(*) from stblAssayGroup where AssayStatus='Current' and TenantID = @TenantID and AssayType = @AssayType) = 0
	Begin
		exec sspGetNextSeqNumber 2,'Assay',0, @ReturnNum output
		
		Insert into stblAssayGroup(AssayBIN, AssayType, AssayDesc, AssayStatus, TenantID)
			values(@ReturnNum, @AssayType, 'Assay name ' + @ReturnNum,'Current',@TenantID)
		
		set @AssayID = @@identity

		If (select count(*) from stblAssaySpecimens where AssayID=@AssayID and SpecimenID=@SpecimenID) = 0
		Begin
			Insert into stblAssaySpecimens(AssayID,SpecimenID,AssignedToBINOn)
			Values(@AssayID,@SpecimenID,getdate())

			Update stblSpecimenInfo set SpecimenStatus='Assigned to Assay' where SpecimenID = @SpecimenID

			Update stblAssayGroup set SampleCount = 1 where AssayID = @AssayID
		End
	End
	else
	Begin
		Declare @SampleCount int

		select @MaxSpecimenCount = Convert(int,ConfigValue) from stblConfigSettings 
			where TenantID=@TenantID and ConfigType='Assay' and ConfigName='Max'
		
		Select @AssayID = AssayID, @SampleCount = SampleCount from stblAssayGroup where AssayStatus='Current' and TenantID = @TenantID and AssayType = @AssayType
		
		If (@SampleCount >= @MaxSpecimenCount)
		Begin
			
			Update stblAssayGroup set AssayStatus='Ready for Testing' where AssayID = @AssayID
			
			exec sspGetNextSeqNumber @TenantID,'Assay',0, @ReturnNum output

			Insert into stblAssayGroup(AssayBIN, AssayType, AssayDesc, AssayStatus, TenantID)
				values(@ReturnNum, @AssayType,'Assay name ' + @ReturnNum,'Current', @TenantID)
		
			set @AssayID = @@identity

			If (select count(*) from stblAssaySpecimens where AssayID=@AssayID and SpecimenID=@SpecimenID) = 0
			Begin
				Insert into stblAssaySpecimens(AssayID, SpecimenID, AssignedToBINOn)
				Values(@AssayID, @SpecimenID, getdate())
				
				Update stblSpecimenInfo set SpecimenStatus='Assigned to Assay' where SpecimenID = @SpecimenID

				Update stblAssayGroup set SampleCount = 1 where AssayID = @AssayID
			End
		End
		Else
		Begin
			If (select count(*) from stblAssaySpecimens where AssayID=@AssayID and SpecimenID=@SpecimenID) = 0
			Begin
				Insert into stblAssaySpecimens(AssayID,SpecimenID,AssignedToBINOn)
				Values(@AssayID,@SpecimenID,getdate())

				Update stblSpecimenInfo set SpecimenStatus='Assigned to Assay' where SpecimenID = @SpecimenID

				Update stblAssayGroup set SampleCount = SampleCount + 1 where AssayID = @AssayID
			End
		End
	End

	Select @AssayID as AssayID
End

GO
/****** Object:  StoredProcedure [dbo].[sspGetAssayInfo]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sspGetAssayInfo] @TenantID int, @AssayStatus varchar(50), @AssayID int = 0, @IsAssayOnly bit = 0
as
begin
	if (@IsAssayOnly = 0)
	Begin
		If (@AssayID != 0)
			select * from svwAssaySpecimens where AssayID = @AssayID
		else
		Begin
			select * into #t1 from svwAssaySpecimens where TenantID=@TenantID
			If @AssayStatus <> ''
				Select * from #t1 where AssayStatus = @AssayStatus
			else
				Select * from #t1 where AssayStatus <> 'Completed'
		End
	End
	Else
	Begin
		If (@AssayID != 0)
			select * from stblAssayGroup where AssayID = @AssayID
		else
		Begin
			select * into #t2 from stblAssayGroup where TenantID=@TenantID
			If @AssayStatus <> ''
				Select * from #t2 where AssayStatus = @AssayStatus
			else
				Select * from #t2 where AssayStatus <> 'Completed'
		End
	End
end


GO
/****** Object:  StoredProcedure [dbo].[sspUpdateAssayGroup]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sspUpdateAssayGroup] @AssayID int, @ActDate datetime, @AssayStatus varchar(30)
AS
Begin
	If Upper(@AssayStatus) = 'IN TESTING'
	Begin
		Update stblAssayGroup set AssayStatus = @AssayStatus, AssayLoadDateTime = @ActDate where AssayID = @AssayID
		Update stblAssaySpecimens set TestStatus = @AssayStatus where AssayID = @AssayID
	End
	Else if Upper(@AssayStatus) = 'TEST COMPLETED'
	Begin
		Update stblAssayGroup set AssayStatus = @AssayStatus, AssayCompleteDateTime = @ActDate where AssayID = @AssayID
		Update stblAssaySpecimens set TestStatus = @AssayStatus, OutofBINDate = @ActDate where AssayID = @AssayID
	End
End


GO
/****** Object:  StoredProcedure [dbo].[sspviewAssayInfo]    Script Date: 11-01-2018 03:32:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sspviewAssayInfo] @TenantID int, @AssayStatus varchar(50),@AssayBIN varchar(30), @IsAssayOnly bit = 0  
as  
begin  
 if (@IsAssayOnly = 0)  
 Begin  
  If (@AssayBIN != '')  
   select * from svwAssaySpecimens where AssayBIN= @AssayBIN  
  else  
  Begin  
   select * into #t1 from svwAssaySpecimens where TenantID=@TenantID  
   If @AssayStatus <> ''  
    Select * from #t1 where AssayStatus = @AssayStatus  
   else  
    Select * from #t1 where AssayStatus <> 'Completed'  
  End  
 End  
 Else  
 Begin  
  If (@AssayBIN != '')  
   select * from svwAssaySpecimens where AssayBIN =@AssayBIN  
     
  else  
  Begin  
   select * into #t2 from stblAssayGroup where TenantID=@TenantID  
   If @AssayStatus <> ''  
    Select * from #t2 where AssayStatus = @AssayStatus  
   else  
    Select * from #t2 where AssayStatus <> 'Completed'  
  End  
 End  
end


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
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "stblAssaySpecimens"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 226
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblAssayGroup"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "svwSpecimenInfo"
            Begin Extent = 
               Top = 6
               Left = 264
               Bottom = 136
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "stblPatient"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 243
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
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimenPatients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'       Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimenPatients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimenPatients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[6] 2[36] 3) )"
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
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "stblAssaySpecimens"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 226
            End
            DisplayFlags = 280
            TopColumn = 14
         End
         Begin Table = "stblAssayGroup"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 345
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "svwSpecimenInfo"
            Begin Extent = 
               Top = 6
               Left = 264
               Bottom = 283
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 19
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 44
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
     ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimens'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'    Width = 1500
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimens'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'svwAssaySpecimens'
GO
