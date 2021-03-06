USE [kosoft_canary]
GO
/****** Object:  View [dbo].[svwAssaySpecimens]    Script Date: 2018-02-06 06:44:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER VIEW [dbo].[svwAssaySpecimens]
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
                         dbo.svwSpecimenInfo.SampleReceivedDate, dbo.svwSpecimenInfo.TenantID AS Expr2, dbo.svwSpecimenInfo.CreatedBy, dbo.stblAssayGroup.AssayName
FROM            dbo.stblAssaySpecimens INNER JOIN
                         dbo.stblAssayGroup ON dbo.stblAssaySpecimens.AssayID = dbo.stblAssayGroup.AssayID INNER JOIN
                         dbo.svwSpecimenInfo ON dbo.stblAssaySpecimens.SpecimenID = dbo.svwSpecimenInfo.SpecimenID

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[17] 2[22] 3) )"
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
            TopColumn = 6
         End
         Begin Table = "stblAssayGroup"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "svwSpecimenInfo"
            Begin Extent = 
               Top = 6
               Left = 264
               Bottom = 136
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 16
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
