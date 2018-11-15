Alter table stblAssayGroup
add AssayName varchar(100) NULL

ALTER procedure [dbo].[sspAddSpecimenToAssay] @SpecimenID int, @TenantID int,@AssayType varchar(50),@AssayName varchar(100)
as
begin
	Declare @MaxSpecimenCount int
	Declare @ReturnNum AS VARCHAR(20)
	Declare @AssayID int

	if (Select count(*) from stblAssayGroup where AssayStatus='Current' and TenantID = @TenantID and AssayType = @AssayType) = 0
	Begin
		exec sspGetNextSeqNumber @TenantID,'Assay',0, @ReturnNum output
		
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

ALTER PROCEDURE [dbo].[sspGetNextSeqNumber] @TenantID AS INT,@ConfigName AS VARCHAR(30), @GeneratedBy int =0, @ReturnNum AS VARCHAR(20) output
AS
BEGIN
	--DECLARE @ReturnNum AS VARCHAR(20)
	Declare @Prefix varchar(10), @Counter int
	Declare @Yr int

	  if (Select PrefixYear from stblConfigSettings where ConfigType='Prefix' and TenantID = @TenantID and ConfigName=@ConfigName) <> year(getdate())
	  Begin
			Update stblConfigSettings set PrefixYear = year(getdate()) where TenantID = @TenantID
	  End
	  Select @Prefix = ConfigValue, @Counter = SrNumber, @Yr = PrefixYear from stblConfigSettings where ConfigType='Prefix' and TenantID = @TenantID and ConfigName=@ConfigName
	  
	  Update stblConfigSettings set SrNumber = SrNumber + 1 where ConfigType='Prefix' and TenantID = @TenantID and ConfigName = @ConfigName

	  if (@Yr > 0)
		Set @ReturnNum  = convert(varchar(5),@yr) + @prefix + substring('00000' + CONVERT(varchar(6),@counter),len(@counter)+1, LEN('00000' + CONVERT(varchar(6),@counter)) - len(@counter))
	  else
	    Set @ReturnNum  = @prefix + substring('00000' + CONVERT(varchar(6),@counter),len(@counter)+1, LEN('00000' + CONVERT(varchar(6),@counter)) - len(@counter))
	  
	  if (@GeneratedBy > 0 and @ConfigName = 'Specimen')
	  begin
		Insert into stblSpecimenID([SpecimenNum],[Status],[CreatedBy],[TenantID],[CreatedON])
			values(@ReturnNum, 'Active',@GeneratedBy, @TenantID, getdate())
	  end
    Select @ReturnNum 
END