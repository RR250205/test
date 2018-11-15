USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspGetSpecimenDetail]    Script Date: 11-01-2018 05:27:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sspGetSpecimenDetail] @SpecimenID int,@TenantID int, @AssayID int = 0
as 
begin 
 
if @AssayID = 0 
 
select Coalesce(dbo.stblPatient.PatientName, dbo.stblPatient.FirstName + ' ' + Coalesce(dbo.stblPatient.LastName,'')) as PatientName,  
    dbo.stblPatient.FirstName, dbo.stblPatient.LastName, dbo.stblPatient.Gender, dbo.stblPatient.DOB, 
dbo.stblPatient.Street,
    dbo.stblPatient.City,dbo.stblPatient.State, dbo.stblPatient.Zip,dbo.stblPatient.Country,GuardianFName,GuardianLName,GuardianStreet,GuardianCity,
	GuardianState,GuardianCountry,GuardianZip,isPatientAddressSame, GuardianRelationship,PatientEmailID,PatientContactNo,GuardianEmailID,GuardianContactNo,
	PendingReason,ReactivateReason,svwSpecimenInfo.* 
   from dbo.stblPatient RIGHT OUTER JOIN svwSpecimenInfo ON dbo.stblPatient.PatientID = svwSpecimenInfo.PatientID
Where SpecimenID = @SpecimenID
else 
select Coalesce(dbo.stblPatient.PatientName, dbo.stblPatient.FirstName + ' ' + Coalesce(dbo.stblPatient.LastName,'')) as PatientName,  
    dbo.stblPatient.FirstName, dbo.stblPatient.LastName, dbo.stblPatient.Gender, dbo.stblPatient.DOB,dbo.stblPatient.Street,
    dbo.stblPatient.City,dbo.stblPatient.State, dbo.stblPatient.Zip,dbo.stblPatient.Country,GuardianFName,GuardianLName,GuardianStreet,GuardianCity,GuardianState,GuardianCountry,GuardianZip,isPatientAddressSame, dbo.stblPatient.PatientID,GuardianRelationship,PatientEmailID,PatientContactNo,GuardianEmailID,GuardianContactNo,PendingReason,ReactivateReason,svwAssaySpecimens.* 
   from dbo.stblPatient RIGHT OUTER JOIN svwAssaySpecimens ON dbo.stblPatient.PatientID = svwAssaySpecimens.PatientID 
Where SpecimenID = @SpecimenID and AssayID = @AssayID
 
end
