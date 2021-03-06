USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspSaveResults]    Script Date: 2018-02-28 08:46:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sspSaveResults] 

@TenantID int, @ResultID int, @SpecimenID int,@AssayID  int, @TotalBuccalProteinyield varchar(30),@CitrateSynthase varchar(30),
@RC_IV varchar(30), @RC_I varchar(30), @analysisReveals text, @Interpretation text,@Notes text, @PerformedBy varchar(30), @ResultDocID int, @IsReleased bit		
AS
begin
	if(@ResultID = 0)
	begin
		Insert stblTestResults(SpecimenID,AssayID,TotalBuccalProteinyield,CitrateSynthase, RC_IV, RC_I,analysisReveals,Interpretation,Notes,TenantID,
		PerformedBy,ResultDocID,IsReleased)
		Values(@SpecimenID, @AssayID, @TotalBuccalProteinyield,@CitrateSynthase,@RC_IV, @RC_I, @analysisReveals, @Interpretation,@Notes,@TenantID,@PerformedBy,@ResultDocID,@IsReleased)	
		set  @ResultID = @@IDENTITY 
	end
	else
	begin
		Update stblTestResults set TotalBuccalProteinyield = @TotalBuccalProteinyield,CitrateSynthase = @CitrateSynthase, RC_IV = @RC_IV,RC_I = @RC_I,
		analysisReveals = @analysisReveals,Interpretation = @Interpretation,Notes = @Notes, PerformedBy=@PerformedBy ,
		ResultDocID=@ResultDocID,IsReleased = @IsReleased
		where ResultID = @ResultID and TenantID = @TenantID
	end
	select @ResultID
end
