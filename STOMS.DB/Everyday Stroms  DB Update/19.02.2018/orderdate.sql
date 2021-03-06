USE [kosoft_canary]
GO
/****** Object:  StoredProcedure [dbo].[sspGenerateOrder]    Script Date: 02/19/2018 14:43:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sspGenerateOrder]
  @TenantID int, 
  @PatientID int,
  @SpecimenID int
  
as
begin
	
	Declare @ReturnNum AS VARCHAR(20)
    Declare @OrderID int
	
	exec sspGetNextSeqNumber  @TenantID ,'Order',2, @ReturnNum output

	INSERT INTO stblOrder(OrderNum,PatientID,SpecimenID,TenantID,OrderDate)
		VALUES(@ReturnNum,@PatientID,@SpecimenID, @TenantID,getdate())

	Select @OrderID=@@IDENTITY
	
End
GO
