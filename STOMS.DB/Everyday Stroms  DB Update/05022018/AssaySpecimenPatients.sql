USE [kosoft_canary]
GO
/****** Object:  View [dbo].[svwAssaySpecimenPatients]    Script Date: 2018-02-05 06:01:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER VIEW [dbo].[svwAssaySpecimenPatients]
AS
SELECT        dbo.stblAssaySpecimens.AssaySpecimenID, dbo.stblAssaySpecimens.AssayID, dbo.stblAssaySpecimens.SpecimenID, dbo.stblAssaySpecimens.IsRepeat, dbo.stblAssaySpecimens.OutofBINDate, 
                         dbo.stblAssaySpecimens.AssignedToBINOn, dbo.stblAssayGroup.AssayBIN, dbo.stblAssayGroup.AssayDesc, dbo.stblAssayGroup.CreatedOn, dbo.stblAssayGroup.SampleMaxDate, dbo.stblAssayGroup.AssayLoadDateTime, 
                         dbo.stblAssayGroup.AssayCompleteDateTime, dbo.stblAssayGroup.SampleCount, dbo.stblAssayGroup.AssayStatus, dbo.stblAssayGroup.TenantID, dbo.svwSpecimenInfo.SpecimenNumber, dbo.svwSpecimenInfo.PatientID, 
                         dbo.stblPatient.Gender, dbo.stblPatient.DOB, dbo.stblPatient.FirstName + ' ' + COALESCE (dbo.stblPatient.LastName, '') AS PatientName, dbo.stblPatient.FirstName, dbo.stblPatient.LastName, 
                         dbo.svwSpecimenInfo.CreatedOn AS SpecimenCreatedOn, dbo.svwSpecimenInfo.SpecimenStatus, dbo.svwSpecimenInfo.CreatedByName, dbo.svwSpecimenInfo.DateDrawn, dbo.svwSpecimenInfo.CustomerID, 
                         dbo.stblAssayGroup.AssayType, dbo.stblAssayGroup.AssayName
FROM            dbo.stblAssaySpecimens INNER JOIN
                         dbo.stblAssayGroup ON dbo.stblAssaySpecimens.AssayID = dbo.stblAssayGroup.AssayID INNER JOIN
                         dbo.svwSpecimenInfo ON dbo.stblAssaySpecimens.SpecimenID = dbo.svwSpecimenInfo.SpecimenID INNER JOIN
                         dbo.stblPatient ON dbo.svwSpecimenInfo.PatientID = dbo.stblPatient.PatientID

GO

