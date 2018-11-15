USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspGetSpecimenAssayInfo]    Script Date: 2018-01-23 03:54:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sspGetSpecimenAssayInfo] @SpecimenID int, @TenantID int
as
begin

	if (select count(*) from svwAssaySpecimenPatients where SpecimenID = @SpecimenID and TenantID = @TenantID) > 0
		select * from svwAssaySpecimenPatients where SpecimenID = @SpecimenID
	else
		select * from svwAssaySpecimenPatients where TenantID  = @TenantID and SpecimenID = @SpecimenID and AssayStatus = 'Current'

end

