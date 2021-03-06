USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspAddSpecimenToAssay]    Script Date: 2018-02-06 06:09:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sspAddSpecimenToAssay] @SpecimenID int, @TenantID int,@AssayType varchar(50),@AssayName varchar(100) = ''
as
begin
	Declare @MaxSpecimenCount int
	Declare @ReturnNum AS VARCHAR(20)
	Declare @AssayID int

	if (Select count(*) from stblAssayGroup where AssayStatus='Current' and TenantID = @TenantID 
	    and AssayType = @AssayType and AssayName = @AssayName) = 0
	Begin
		exec sspGetNextSeqNumber @TenantID,'Assay',0, @ReturnNum output
		
		Insert into stblAssayGroup(AssayBIN, AssayType, AssayName, AssayDesc, AssayStatus, TenantID)
			values(@ReturnNum, @AssayType, @AssayName, 'Assay name ' + @ReturnNum, 'Current', @TenantID)
		
		set @AssayID = @@identity

		If (select count(*) from stblAssaySpecimens where AssayID=@AssayID and SpecimenID=@SpecimenID) = 0
		Begin
			Insert into stblAssaySpecimens(AssayID, SpecimenID, AssignedToBINOn)
			Values(@AssayID, @SpecimenID, getdate())

			Update stblSpecimenInfo set SpecimenStatus='Assigned to Assay' where SpecimenID = @SpecimenID

			Update stblAssayGroup set SampleCount = 1 where AssayID = @AssayID
		End
	End
	else
	Begin
		Declare @SampleCount int

		select @MaxSpecimenCount = Convert(int,ConfigValue) from stblConfigSettings 
			where TenantID=@TenantID and ConfigType='Assay' and ConfigName='Max'
		
		Select @AssayID = AssayID, @SampleCount = SampleCount from stblAssayGroup where AssayStatus='Current' and TenantID = @TenantID and AssayType = @AssayType and AssayName = @AssayName
		
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
